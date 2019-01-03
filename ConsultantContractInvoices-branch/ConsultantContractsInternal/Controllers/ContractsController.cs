using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Diagnostics;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.ViewModels;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Newtonsoft.Json;
using ConsultantContractsInternal.Utilities;
using System.IO;
using Microsoft.SharePoint;
using SP = Microsoft.SharePoint.Client;
using Elmah;
using System.Data.Entity.SqlServer;
//using UserAuthorization.Data.Dto.Response;
//using ConsultantContracts.Infrastructure.Service;
using ArDOT_UserProv.Client.API;
using ConsultantContractsInternal.Security.Attributes;
using System.Web.Caching;
using System.Configuration;

namespace ConsultantContractsInternal.Controllers
{
    public class ContractsController : SecuredController
    {
        private IList<int> _AvailableDivs;

        public ContractsController()
        {
            using (var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = UserProvHelpers.AvailableDivisions(context, CurrentUser.UserName);
            }
        }
        // Variable that holds the file
        byte[] fileuploaded;
        HttpPostedFileBase file3;
        // GET: /Contracts/

        public ActionResult Index()
        {
            if (Roles.CanEnterContracts.Split(',').Contains(CurrentUser.Role))
                ViewBag.WriteAccess = true;
            else
                ViewBag.WriteAccess = false;

            return View();
        }

        public ActionResult Recent20(string q)
        {
            //UserAuthorizationServiceClient client = new UserAuthorizationServiceClient();
            //LegacySecurityDto[] result = client.GetLegacySecurityInfo(CurrentUser.UserName, "conscontr");

            var results = Filter(q);
            return Json(results.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult AllContracts(int consultantId = 0)
        {
            using (var context = new ConsultantContractsEntities())
            {
                //_AvailableDivs = UserProvHelpers.AvailableDivisions(context, CurrentUser.UserName);
                var contracts = context.Contracts.AsNoTracking()
                              .Where(p => _AvailableDivs.Contains( p.ResponsibleDivisionId ))
                              .Include(c => c.Consultant)
                              .Include(c => c.ContractSubConsultants)
                              .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultant))
                              .Where(c => (c.ConsultantId == consultantId
                                            || c.ContractSubConsultants.Any(sc => sc.SubConsultantId == consultantId)
                                          ))
                              .OrderByDescending(c => c.ContractStatus).ThenByDescending(c => c.ContractCode)
                              .ToList();

                var consultant = context.Consultants.SingleOrDefault(c => c.ConsultantId == consultantId);

                ViewBag.ConsultantName = consultant == null ? "" : consultant.Name; 
                ViewBag.ConsultantId = consultantId;

                return View(contracts);
            }
        }

        public ActionResult TopContractsByConsultant(int id)
        {
            List<Contract> list;

            ViewBag.Id = id;

            using (var context = new ConsultantContractsEntities())
            {
                //_AvailableDivs = UserProvHelpers.AvailableDivisions(context, CurrentUser.UserName);
                list = context.Contracts
                              .Where(p => _AvailableDivs.Contains(p.ResponsibleDivisionId)).Include(c => c.Consultant)
                              .Include(c => c.ContractSubConsultants)
                              .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultant))
                              .Where(c => (c.ConsultantId == id
                                            || c.ContractSubConsultants.Any(sc => sc.SubConsultantId == id)
                                          )
                                          && new[] { "A", "I" }.Contains(c.ContractStatus))
                              .OrderByDescending(c => c.ContractStatus).ThenByDescending(c => c.ContractCode)
                              .Take(12)
                              .ToList();

            }

            return PartialView(list);
        }
        public ActionResult Search(string term)
        {
            var r = Filter(term);
            return Json(r.Select(c => new { label = String.Format("{0} - {1} {2}", c.ContractCode, c.JobNo, c.Consultant.Name), value = c.ContractCode }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LookupContractCodeInSalaryRates(string term)
        {
            if( !string.IsNullOrEmpty(term))
            {
                using (var db = new ConsultantContractsEntities())
                {
                    var queryResult = db.Contracts.Join(db.SalaryRates, a => a.ContractCode, b => b.ConractCode, (a, b) => new { a, b }).Where(b => (b.a.ContractCode.ToString() + b.a.JobNo).Contains(term)).OrderBy(d => d.a.ContractCode).Select(c => new { data = c.b.ConractCode, label = c.a.ContractCode.ToString() + " - " + c.a.JobNo });
                    var resultList = queryResult.ToList();
                    return Json(resultList, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }

        public ActionResult ContractSummary(int id)
        {
            if (id != 0)
            {
                using (var context = new ConsultantContractsEntities())
                {
                    var cd = context.Contracts.Include(c => c.Consultant)
                                              .Include(c => c.ContractType)
                                              .Include(c => c.ContractAllotments)
                                              .Include(c => c.Invoices)
                                              .SingleOrDefault(c => c.ContractCode == id);
                    if (cd == null)
                    {
                        //error here better
                        throw new NotImplementedException();
                    }
                    var t1Allotments = cd.ContractAllotments
                        .Where(a => a.IsT1())
                        .Select(a => new
                                {
                                    a.Func,
                                    a.FAP,
                                    a.FundingPriority,
                                    a.AllottedAmt
                                });
                    var t2Allotments = cd.ContractAllotments
                        .Where(a => a.IsT1())
                        .Select(a => new
                                {
                                    a.Func,
                                    a.FAP,
                                    a.FundingPriority,
                                    a.AllottedAmt
                                });

                    var invoiceData = context.Invoices.Where(i => i.ContractCode == id)
                        .GroupBy(i => i.ContractCode,
                            (k, x) =>
                                new
                                {
                                    cc = k,
                                    iNo = x.Max(i => i.InvoiceNo),
                                    T1FFTotal = x.Sum(i => i.T1FixedFee),
                                    T2FFTotal = x.Sum(i => i.T2FixedFee)
                                }).SingleOrDefault();
                    var summary = new
                    {
                        contractCode = cd.ContractCode,
                        jobNo = cd.JobNo,
                        consultantId = cd.ConsultantId,
                        taxId = String.Format("{0}-{1}", cd.Consultant.TaxId, cd.Consultant.SeqNo),
                        consultantName = cd.Consultant.Name,
                        consultantEmail = cd.Consultant.EmailAddress,
                        primaryContact = String.Format("{0} {1}", cd.Consultant.PrimaryContactFirstName, cd.Consultant.PrimaryContactLastName),
                        totalAmount = cd.ContractCeiling,
                        cd.T1SvcsCeiling,
                        cd.T1FixedFeeMax,
                        cd.FCCM,
                        LastInvoiceDate = cd.Invoices.Count > 0 ? cd.Invoices.Select(i => i.EndDate).Max().AddDays(1).ToShortDateString() : null,
                        cd.HomeOfficeOverheadRateMax,
                        cd.T2SvcsCeiling,
                        cd.T2FixedFeeMax,
                        cd.FieldServiceOverheadRateMax,
                        cd.Multiplier,
                        t1Allotments,
                        t2Allotments,
                        cd.Remarks,
                        cd.TaskOrderNo,
                        NextInvoiceNo = invoiceData == null ? 1 : invoiceData.iNo + 1,
                        T1FixedFeeTotal = (invoiceData != null && invoiceData.T1FFTotal != null) ? invoiceData.T1FFTotal.Value : 0,
                        T2FixedFeeTotal = (invoiceData != null && invoiceData.T2FFTotal != null) ? invoiceData.T2FFTotal.Value : 0
                    };

                    return Json(summary, JsonRequestBehavior.AllowGet);
                }
            }
            return new HttpNotFoundResult();
        }

        public ActionResult Details(int id)
        {          
            using (var context = new ConsultantContractsEntities())
            {
                //var availableDivs = UserProvHelpers.AvailableDivisions(context,CurrentUser.UserName);
                //var contractPermissionBudget = UserProvHelpers.GetContractPermissions(context, CurrentUser.UserName, _AvailableDivs);
                //if (contractPermissionBudget.Select(p => p.PermissionId.Where(o => o.Equals("CONS_FULL"))) != null)
                if (Roles.ContractEntryRoles.Contains(CurrentUser.Role))
                {
                    ViewBag.Permission = "Write";
                }
                else
                {
                    ViewBag.Permission = "Read";
                }
                return View(context.Contracts.Include(c => c.Consultant)
                                             .Include(c => c.ContractSubConsultants)
                                             .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultant))
                                             .Include(c => c.ContractType)
                                             .Include(c => c.AgreementType)
                                             .Include(c => c.WorkTypes)
                                             .Include(c => c.ResponsibleDivision)
                                             .Include(c => c.RemitToCon)
                                             .Single(c => c.ContractCode == id));
            }
        }

        [HttpGet]
        public ActionResult GetCommentsByContractCode(string contractCode)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var commentsList = context.CommentsHistories.Where(p => p.CommentNo == contractCode).OrderByDescending(p=>p.DateEntered).ToList();
                return Json(new { commentsList }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SalaryList(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                return PartialView(context.SalaryRates.Where(s => s.ConractCode == id).OrderByDescending(s => s.RateMin).ToList());
            }
        }

        public ActionResult ServiceList(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                return PartialView(context.ServiceRates.Where(s => s.ContractCode == id).OrderByDescending(s => s.RateMin).ToList());
            }
        }

        public ActionResult SubConsultantList(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                if (Roles.ContractEntryRoles.Contains(CurrentUser.Role))
                {
                    ViewBag.Permission = "Write";
                }
                else
                {
                    ViewBag.Permission = "Read";
                }
                return PartialView(context.ContractSubConsultants
                                          .Include(csc => csc.SubConsultant)
                                          .Include(csc => csc.SubConsultantSalaryRates)
                                          .Include(csc => csc.SubConsultantServiceRates)
                                          .Where(csc => csc.ContractCode == id).ToList()
                                    );
            }
        }

        public ActionResult AllotmentList(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {

                var contractAllotments = context.ContractAllotments.Where(ca => ca.ContractCode == id)
                    .AsEnumerable();

                var invoicePayments =
                    context.InvoicePayments.Include(ip => ip.Invoice).Where(i => i.Invoice.ContractCode == id)
                        .GroupBy(i => new { i.Func, i.FAP })
                        .Select(g => new { g.Key.FAP, g.Key.Func, Total = g.Sum(a => a.Amount) }).AsEnumerable();


                IEnumerable<AllotmentBalanceVm> allotments = contractAllotments
                .GroupJoin(
                    invoicePayments,
                    a => new { a.Func, a.FAP },
                    i => new { i.Func, i.FAP },
                    (a, ip) => new { a, ip })
                .SelectMany(a => a.ip.DefaultIfEmpty(),
                            (a, ip)
                                => new AllotmentBalanceVm {
                                    FAP = a.a.FAP,
                                    Func = a.a.Func,
                                    FundingPriority = a.a.FundingPriority,
                                    AllottedAmt = a.a.AllottedAmt,
                                    Total = ip == null ? 0 : ip.Total
                                }
                            ).ToList();

                var allTotals = allotments.ToDictionary(g => new Tuple<string, string>(g.FAP, g.Func), g => g.Total);

                ViewBag.Totals = allTotals;

                if (Roles.Admin.Split(',').Contains(CurrentUser.Role))
                    ViewBag.WriteAccess = true;
                else
                    ViewBag.WriteAccess = false;

                return PartialView(contractAllotments.OrderBy(p => p.FundingPriority).ToList());
            }
        }

        public ActionResult AllotmentsForInvoicing(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var contractAllotments = context.ContractAllotments.Where(ca => ca.ContractCode == id)
                    .Where(a => a.FundingPriority.HasValue && a.FundingPriority > 0)
                    .AsEnumerable();

                var invoicePayments =
                    context.InvoicePayments.Include(ip => ip.Invoice).Include(ip => ip.Invoice.InvoiceApproval)
                    .Where(i => i.Invoice.ContractCode == id && i.Invoice.SubmittedDate.HasValue && i.Invoice.InvoiceApproval.Where(p=>p.Status == InvoiceStatus.InvoiceRejected && p.ActiveStatus ==false).Count() <1)
                        .GroupBy(i => new { i.Func, i.FAP })
                        .Select(g => new { g.Key.FAP, g.Key.Func, Total = g.Sum(a => a.Amount) }).AsEnumerable();
                
                var allotments = contractAllotments
                .GroupJoin(
                    invoicePayments,
                    a => new { a.Func, a.FAP },
                    i => new { i.Func, i.FAP },
                    (a, ip) => new { a, ip })
                .SelectMany(a => a.ip.DefaultIfEmpty(),
                            (a, ip)
                                => new
                                {
                                    a.a.FAP,
                                    a.a.Func,
                                    a.a.FundingPriority,
                                    IsT1 = a.a.IsT1(),
                                    IsT2 = a.a.IsT2(),
                                    a.a.AllottedAmt,
                                    Total = ip == null ? 0 : ip.Total
                                }
                            );
                if (Roles.Admin.Split(',').Contains(CurrentUser.Role))
                    ViewBag.WriteAccess = true;
                else
                    ViewBag.WriteAccess = false;

                return Json(allotments.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        //public HttpPostedFileBase _file { get; set; }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult SaveRemark(int contractCode, string text)
        {
            using (var context = new ConsultantContractsEntities())
            {
                //-----------------------BEFORE REAMRKS WAS SAVED IN CONTRACT TABLE, AND NOW IT IS SAVING IN COMMENTSHISTORY TABLE------------
                //var contract = context.Contracts.SingleOrDefault(c => c.ContractCode == contractCode);
                //if (contract == null) return new HttpNotFoundResult();
                //contract.Remarks = text;
                //contract.RemarkLastEditDate = DateTime.Now;
                //contract.RemarkLastEditUser = CurrentUser.UserName;

                var comments = context.CommentsHistories.Create();
                comments.CommentNo = Convert.ToString(contractCode);
                comments.Type = "Contract";
                comments.UserId = CurrentUser.UserName;
                comments.DateEntered = DateTime.Now;
                comments.Comment = text;

                context.CommentsHistories.Add(comments);
                context.SaveChanges();

                return Json(new { msg = "OK" });
            }
        }

        [HttpPost]
        public ActionResult UpdateContractAllotment(int contractCode, string fap, string func, int priority, decimal amt)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var allotment = context.ContractAllotments.Where(p => p.ContractCode.Equals(contractCode) && p.FAP.Equals(fap.TrimEnd()) && p.Func.Equals(func)).FirstOrDefault();
                if (allotment != null)
                {
                    allotment.FundingPriority = priority;
                    allotment.AllottedAmt = amt;
                    context.SaveChanges();
                }
                else
                    return Json(new { Status = "ERROR" });
                return Json(new { Status = "OK" });
            }
        }

        [HttpPost]
        public ActionResult UpdateSubconsultantRates(int contractCode, int subConId, decimal? fccm, decimal? fieldServiceRateMax, decimal? homeOfficeRateMax, decimal? multiplier)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var subCon = context.ContractSubConsultants.Where(p => p.ContractCode.Equals(contractCode) && p.SubConsultantId.Equals(subConId)).FirstOrDefault();
                if (subCon != null)
                {
                    subCon.FCCM = fccm != null ? fccm : subCon.FCCM;
                    subCon.FieldServiceOverheadRateMax = fieldServiceRateMax != null ? fieldServiceRateMax : subCon.FieldServiceOverheadRateMax;
                    subCon.HomeOfficeOverheadRateMax = homeOfficeRateMax != null ? homeOfficeRateMax : subCon.HomeOfficeOverheadRateMax;
                    subCon.Multiplier = multiplier != null ? multiplier : subCon.Multiplier;

                    context.SaveChanges();
                }
                else
                    return Json(new { Status = "ERROR" });
                return Json(new { Status = "OK" });
            }
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult Preload(HttpPostedFileBase file)
        {
            string name;
            name = file.FileName;
            file3 = file;
            string fname;
            //TODO: testing only
            ErrorLog.GetDefault(null).Log(new Error(new Exception("Preload file.filename - " + file.FileName)));
            string extensionName = System.IO.Path.GetExtension(file.FileName);
            // generate a random file name and appende extension name
            string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;
            fname = Path.Combine(Server.MapPath("~/Uploads/"), finalFileName);
            Session["updone"] = false;
            Session["fileup"] = finalFileName;
            Session["updone"] = true;

            file.SaveAs(fname);

            //byte[] image = new byte[file.File.ContentLength];
            //model.File.InputStream.Read(image, 0, image.Length); 

             //var array = new Byte[file.ContentLength];
             //var array2 = new Byte[file.ContentLength]; 

             //file.InputStream.Position = 0; 
             //file.InputStream.Read(array, 0, file.ContentLength);
             //array.CopyTo(array2,0);


             //fileuploaded= array; 

            //fileuploaded = file;
            return PartialView();

        }
        public ActionResult Preload2()  
        {  
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)  
            {  
                try  
                {  
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;  
                    for (int i = 0; i < files.Count; i++)  
                    {  
                        HttpPostedFileBase file = files[i];  
                        string fname;  

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")  
                        {  
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });  
                            fname = testfiles[testfiles.Length - 1];  
                        }  
                        else  
                        {  
                            fname = file.FileName;  
                        }

                        // Get the complete folder path and store the file inside it.  
                        string extensionName = System.IO.Path.GetExtension(file.FileName);
                        // generate a random file name and appende extension name
                        string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;
                        fname = Path.Combine(Server.MapPath("~/Upload/"), finalFileName);
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Upload/"));
                        if (!di.Exists)
                        {
                            ErrorSignal.FromCurrentContext().Raise(new Exception("Directory does not exit for " + fname));
                        }
                        try
                        {
                            file.SaveAs(fname);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error Saving fname - " + ex.Message + ";  fname = " + fname);
                        }
                
                        Session["updone"] = "false";
                        Session["fileup"] = fname;
                        //System.Threading.Thread.Sleep(30000);
                        Session["updone"] = "true";
                    }  
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");  
                }  
                catch (Exception ex)  
                {
                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                    return Json("Error occurred. Error details: " + ex.Message);  
                }  
            }  
            else  
            {  
                return Json("No files selected.");  
            }  
        }  

        public ActionResult uploadfile(int contractCode, string jobno, string filen)
        {
            string JsonList2;
            string JsonList;
            using (var context = new ConsultantContractsEntities())
            {
                // http://devsp2010srv/divisions/ConsultantContracts/default.aspx
                var siteUrl = "http://devsp2010srv/divisions/ConsultantContracts";
                var webName = "http://devsp2010srv/divisions/ConsultantContracts";
                var libraryName = "PreContracts";
                {
                    using (Microsoft.SharePoint.Client.ClientContext clientContext = new Microsoft.SharePoint.Client.ClientContext(siteUrl))
                    {

                       string filefinal = Session["fileup"] as string;
                       //string finfile = (string)(Session["fileup"]);
                       // string moretests = (string) HttpContext.Session["fileup"];
                       //string updone = Session["updone"] as string;
                       string updone = Session["updone"] as string;
                        Session["updone"] = null;
                        Session["fileup"] = null;

                        string filname = Path.GetFileName(filen);
                        string uploadLocation = contractCode + filname;
                        uploadLocation = string.Format("{0}/{1}/{2}", webName, libraryName, uploadLocation);
                        var list = clientContext.Web.Lists.GetByTitle(libraryName);
                        var fileCreationInformation = new Microsoft.SharePoint.Client.FileCreationInformation();

                        if(filefinal != null)
                        {
                            fileCreationInformation.Content = System.IO.File.ReadAllBytes(filefinal);
                        }
                        else
                        {
                            throw new Exception(string.Format("filefinal = {0}; filname = {1}; uploadLocation = {2}", filefinal, filname, uploadLocation));
                        }
                        //fileCreationInformation.Content = (array);
                        fileCreationInformation.Overwrite = true;
                        fileCreationInformation.Url = uploadLocation;
                        Microsoft.SharePoint.Client.File uploadFile = list.RootFolder.Files.Add(fileCreationInformation);
                        clientContext.ExecuteQuery();
                        Microsoft.SharePoint.Client.ListItem item = uploadFile.ListItemAllFields;
                        item["Title"] = contractCode;
                        item["contractCode"] = contractCode;
                        item["jobno"] = jobno;
                        item.Update();
                        clientContext.ExecuteQuery();

                        Microsoft.SharePoint.Client.CamlQuery query = new Microsoft.SharePoint.Client.CamlQuery();
                        query.ViewXml = "<View><Query><Where><Contains><FieldRef Name='Title'/><Value Type='Text'>" + contractCode + "</Value></Contains></Where></Query></View>";
                        Microsoft.SharePoint.Client.ListItemCollection collListItem = list.GetItems(query);

                        clientContext.Load(collListItem);
                        clientContext.ExecuteQuery();
                        // delete the file from web server
                        if (System.IO.File.Exists(filefinal))
                        {
                            // Delete our file
                            System.IO.File.Delete(filefinal);

                        } 
                        //
                        JsonList = ListColAsJson(collListItem);
                        JsonList2 = JsonList.Replace(",{]", "]");


                    }
                }
                return Content(JsonList2, "application/json");



            }
        }
        public ActionResult loadfile(int contractCode)
        {
            string JsonList2;
            string JsonList;



            using (var context = new ConsultantContractsEntities())
            {
                var siteUrl = "http://devsp2010srv/divisions/ConsultantContracts";
                var libraryName = "Contracts";
                {
                    using (Microsoft.SharePoint.Client.ClientContext clientContext = new Microsoft.SharePoint.Client.ClientContext(siteUrl))
                    {

                        try
                        {
                            var list = clientContext.Web.Lists.GetByTitle(libraryName);
                            Microsoft.SharePoint.Client.CamlQuery query = new Microsoft.SharePoint.Client.CamlQuery();
                            query.ViewXml = "<View><Query><Where><Contains><FieldRef Name='Title'/><Value Type='Text'>" + contractCode + "</Value></Contains></Where></Query></View>";
                            Microsoft.SharePoint.Client.ListItemCollection collListItem = list.GetItems(query);

                            clientContext.Load(collListItem);
                            clientContext.ExecuteQuery();
                            JsonList = ListColAsJson(collListItem);
                            if (collListItem.Count == 0)
                            {
                                JsonList2 = JsonList.Replace("[{]", "{}");

                            }
                            else
                            {
                                JsonList2 = JsonList.Replace(",{]", "]");
                            }
                            return Content(JsonList2, "application/json");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                        }
                    }
                }
                return null;
            }
        }


        public string ListColAsJson(SP.ListItemCollection IndListItem)
        {
            int rowcnt = IndListItem.Count;
            string fld_name = "";
            string fval = "";
            string json = "[{";
            int numfields = 0;
            int totnumfields = 2;
            foreach (SP.ListItem oListItem in IndListItem)
            {
                int fcount = oListItem.FieldValues.Keys.Count;
                for (int j = 0; j < fcount; j++)
                {
                    fld_name = oListItem.FieldValues.Keys.ElementAt(j);

                    //if (fld_name.Trim() == "FileRef" || (fld_name.Trim() == "Created_x0020_By") || fld_name.Trim() == "File_x0020_Size")
                    if (fld_name.Trim() == "FileRef" || fld_name.Trim() == "File_x0020_Size")
                    {
                        numfields = numfields + 1;
                        {
                            try
                            {
                                fval = HttpUtility.HtmlEncode(oListItem.FieldValues[fld_name].ToString());
                            }
                            catch
                            {
                                fval = "Missing or invalid Value";
                            }

                            if (j < fcount - 1)
                            {
                                if (numfields < totnumfields)
                                {

                                    json += "" + '"' + fld_name + '"' + ":" + '"' + fval + '"' + ",";
                                }
                                else
                                {
                                    json += "" + '"' + fld_name + '"' + ":" + '"' + fval + '"' + "},{";
                                    numfields = 0;
                                }

                            }
                            else
                            {

                                json += "" + '"' + fld_name + '"' + ":" + '"' + fval;

                            }

                        }
                    }


                }


            }
            json += "]";
            return json;
        }


        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult ChangeFundingPriority(int contractCode, int? priority, string func, string fap)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var con = context.Contracts.Include(c => c.ContractAllotments).SingleOrDefault(c => c.ContractCode == contractCode);

                if (con == null) return new HttpNotFoundResult(String.Format("Contract {0} not found", contractCode));

                var allotment = con.ContractAllotments.SingleOrDefault(a => a.Func == func && a.FAP == fap);

                if (allotment == null) return new HttpNotFoundResult(String.Format("Allotment Func: {0}, FAP: {1} for contract {2} not found", func, fap, contractCode));

                allotment.FundingPriority = priority;

                context.SaveChanges();

                return Json(new { status = "OK" });
            }
        }


        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult Create()
        {
            using (var context = new ConsultantContractsEntities())
            {
                //string[] divisions = context.ResponsibleDivisions.Where(p => _AvailableDivs.Contains(p.DivisionId)).Select(p => p.ShortName).ToArray();
                //ViewBag.divisions = divisions;
                var availableDivs = _AvailableDivs;
                ViewBag.divisions = 
                    context.ResponsibleDivisions.Where(p => availableDivs.Contains(p.DivisionId)).ToList().Select(p => new { value = p.DivisionId, label = p.ShortName });

                ViewBag.contractTypes =
                    context.ContractTypes.ToList().Select(ct => new { value = ct.TypeId, label = ct.TypeShortName });

                ViewBag.agreementTypes =
                    context.AgreementTypes.ToList().Select(at => new { value = at.AgreementTypeId, label = at.AgreementTypeName });

                ViewBag.workTypes = context.WorkTypes.ToList().Select(wt => new { wt.WorkTypeId, wt.WorkTypeName });

                ViewBag.consultants = context.Consultants.ToList().Select(co => new { value = co.ConsultantId, label = co.Name });

                return View();
            }
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult Create(NewContractVM model)
        {

            using (var context = new ConsultantContractsEntities())
            {
                var newContract = model.PopulateContract(context.Contracts.Create());
                newContract.LastUpdateDate = DateTime.Now;
                newContract.LastUpdateUser = CurrentUser.UserName;
                var workTypeIdList = model.WorkTypes.Select(w => w.WorkTypeId);

                var workTypes =
                    context.WorkTypes.Where(wt => workTypeIdList.Contains(wt.WorkTypeId));

                workTypes.ToList().ForEach(wt => newContract.WorkTypes.Add(wt));
                if (model.SubConsultants != null)
                {
                    model.SubConsultants.ForEach(sc =>
                        {
                            var s = sc.ToContractSubConsultant();
                            s.LastUpdateDate = DateTime.Now;
                            s.LastUpdateUser = CurrentUser.UserName;

                            if (sc.SalaryRates != null)
                            {
                                sc.SalaryRates.ForEach(srvm =>
                                {
                                    var sr = srvm.ToSubConSalaryRate();
                                    sr.SubConsultantId = s.SubConsultantId;
                                    sr.LastUpdateUser = CurrentUser.UserName;
                                    sr.LastUpdateDate = DateTime.Now;

                                    s.SubConsultantSalaryRates.Add(sr);
                                });
                            }
                            if (sc.ServiceRates != null)
                            {
                                sc.ServiceRates.ForEach(srvm =>
                                {
                                    var sr = srvm.ToSubConServiceRate();
                                    sr.SubConsultantId = s.SubConsultantId;
                                    sr.LastUpdateUser = CurrentUser.UserName;
                                    sr.LastUpdateDate = DateTime.Now;

                                    s.SubConsultantServiceRates.Add(sr);
                                });
                            }
                            newContract.ContractSubConsultants.Add(s);
                        });
                }
                if (model.SalaryRates != null)
                {
                    model.SalaryRates.ForEach(sr =>
                    {
                        var s = sr.ToSalaryRate();
                        s.LastUpdateDate = DateTime.Now;
                        s.LastUpdateUser = CurrentUser.UserName;

                        newContract.SalaryRates.Add(s);
                    });
                }

                if (model.ServiceRates != null)
                {
                    model.ServiceRates.ForEach(sr =>
                    {
                        var s = sr.ToServiceRate();
                        s.LastUpdateDate = DateTime.Now;
                        s.LastUpdateUser = CurrentUser.UserName;

                        newContract.ServiceRates.Add(s);
                    });
                }

                if (model.T1Allotments != null)
                {
                    model.T1Allotments.ForEach(avm =>
                    {
                        var a = avm.ToAllotment();
                        a.LastUpdateDate = DateTime.Now;
                        a.LastUpdateUser = CurrentUser.UserName;

                        newContract.ContractAllotments.Add(a);
                    });
                }

                if (model.T2Allotments != null)
                {
                    model.T2Allotments.ForEach(avm =>
                    {
                        var a = avm.ToAllotment();
                        a.LastUpdateDate = DateTime.Now;
                        a.LastUpdateUser = CurrentUser.UserName;

                        newContract.ContractAllotments.Add(a);
                    });
                }
                var _remarks = newContract.Remarks;
                newContract.Remarks = null;
                context.Contracts.Add(newContract);
                context.SaveChanges();

                if (model.Remarks != null && newContract.ContractCode > 0)
                {
                    var comments = context.CommentsHistories.Create();
                    comments.CommentNo = Convert.ToString(newContract.ContractCode);
                    comments.Type = "Contract";
                    comments.UserId = CurrentUser.UserName;
                    comments.DateEntered = DateTime.Now;
                    comments.Comment = _remarks;

                    context.CommentsHistories.Add(comments);
                    context.SaveChanges();
                }

                return Json(new { Status = "OK", newContractCode = newContract.ContractCode }, JsonRequestBehavior.DenyGet);
            }


            return Json(new { Status = "ERROR", msg = "WHAT IS HAPPENING" });

        }

        [HttpGet]
        public ActionResult GetRates(int contractCode)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var contract = context.Contracts.Include(c => c.SalaryRates).Include(c => c.ServiceRates).SingleOrDefault(c => c.ContractCode == contractCode);
                if (contract == null) return new HttpNotFoundResult();

                var salaryRates = contract.SalaryRates.Select(s => new
                {
                    name = s.JobTitle,
                    min = s.RateMin,
                    max = s.RateMax
                });
                var serviceRates = contract.ServiceRates.Select(s => new
                {
                    name = s.ServiceName,
                    min = s.RateMin,
                    max = s.RateMax
                });

                return Json(new { salaryRates, serviceRates }, JsonRequestBehavior.AllowGet);
            }
        }

        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult Edit(int id)
        {
            ContractEditVM vm;

            using (var context = new ConsultantContractsEntities())
            {
                vm = new ContractEditVM(context, id);

                vm.ConsultantName = context.Consultants.FirstOrDefault(at => at.ConsultantId.Equals(vm.ConsultantId)).Name;
                if (vm.RemitToId != null)
                {
                    var remitToConsultant = context.Consultants.FirstOrDefault(at => at.ConsultantId.Equals((int)vm.RemitToId));
                    if(remitToConsultant != null)
                        vm.RemitToName = remitToConsultant.Name;
                }
                ViewBag.contractTypes =
                    context.ContractTypes.ToList().Select(ct => new { value = ct.TypeId, label = ct.TypeShortName });

                ViewBag.agreementTypes =
                    context.AgreementTypes.ToList().Select(at => new { value = at.AgreementTypeId, label = at.AgreementTypeName });

                ViewBag.responsibleDivisions =
                    context.ResponsibleDivisions.ToList().Select(at => new { value = at.DivisionId, label = at.ShortName });
            }

            return View(vm);
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult Edit(ContractEditVM model)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var contract = context.Contracts.SingleOrDefault(c => c.ContractCode == model.ContractCode);

                if (contract == null)
                    return new HttpNotFoundResult("Contract doesn't exist");

                contract.ContractTypeId = model.ContractTypeId;
                contract.ContractStatus = model.ContractStatus;
                contract.TitlePhase = model.TitlePhase;
                contract.AgreementDate = model.AgreementDate;
                contract.NoticeProceedDate = model.NoticeProceedDate;
                contract.AgreementTypeId = model.AgreementTypeId;
                contract.TaskOrderNo = model.TaskOrderNo;
                contract.ResponsibleDivisionId = model.ResponsibleDivisionId;
                contract.ScheduledCompletionDate = model.ScheduledCompletionDate;
                contract.CompletionDate = model.CompletionDate;

                //JL40985  2/9/2017  Added these 6 parameters.
                contract.LastUpdateDate = DateTime.Now;
                contract.LastUpdateUser = CurrentUser.UserName;
                contract.HomeOfficeOverheadRateMax = model.HomeOfficeOverheadRateMax != null ? Math.Round((decimal)model.HomeOfficeOverheadRateMax, 3) : 0.00m;
                contract.FieldServiceOverheadRateMax = model.FieldServiceOverheadRateMax != null ? Math.Round((decimal)model.FieldServiceOverheadRateMax, 3) : 0.00m;
                contract.FCCM = model.FCCM != null ? Math.Round((decimal)model.FCCM, 3) : 0.00m;
                contract.Remarks = model.Remarks;

                //JL40985  3/28/2017  Added these 6 parameters.
                contract.Multiplier = model.Multiplier;
                contract.ContractCeiling = model.ContractCeiling;
                contract.T1SvcsCeiling = model.T1SvcsCeiling;
                contract.T1FixedFeeMax = model.T1FixedFeeMax;
                contract.T2SvcsCeiling = model.T2SvcsCeiling;
                contract.T2FixedFeeMax = model.T2FixedFeeMax;

                //rh41200 5/19/2017 
                
                contract.ConsultantId = model.ConsultantId;
                contract.RemitTo = model.RemitToId;
                contract.ConsultantContractNo = model.ConsultantContractNo;

                context.SuppAgreements.Where(p => model.ContractCode.Equals(p.ContractCode))
                    .ToList()
                    .ForEach(p =>
                    {
                        p.FCCM = contract.FCCM;
                        p.LastUpdateUser = contract.LastUpdateUser;
                        p.LastUpdateDate = contract.LastUpdateDate;
                        p.Multiplier = contract.Multiplier;
                        p.ContractCeiling = contract.ContractCeiling;
                        p.HomeOfficeOverheadRateMax = contract.HomeOfficeOverheadRateMax;
                        p.FieldServiceOverheadRateMax = contract.FieldServiceOverheadRateMax;
                        p.T1FixedFeeMax = contract.T1FixedFeeMax;
                        p.T1SvcsCeiling = contract.T1SvcsCeiling;
                        p.T2FixedFeeMax = contract.T2FixedFeeMax;
                        p.T2SvcsCeiling = contract.T2SvcsCeilingOrig;
                    });
                context.SaveChanges();

            }



            return Json(new { model.ContractCode, Status = "OK" });
        }

        //TODO fix according to invoice updates eventually probably
        private IEnumerable<Contract> Filter(string term, int count = 100)
        {
            using (var context = new ConsultantContractsEntities())
            {
                IEnumerable<int> availableDivs = UserProvHelpers.AvailableDivisions(context, CurrentUser.UserName);
                List<Contract> r = null;

                if (String.IsNullOrWhiteSpace(term))
                {
                    r = context.Contracts.Where(p => availableDivs.Contains(p.ResponsibleDivisionId)).AsNoTracking().Include(c => c.Consultant).AsQueryable().OrderByDescending(c => c.ContractCode).Take(count).ToList();
                }
                else
                {
                    var q = context.Contracts.AsNoTracking().Include(c => c.Consultant).Where(c => c.JobNo.Contains(term) )
                        .Union(
                            context.Contracts.AsNoTracking().Include(c => c.Consultant).Where(c => SqlFunctions.StringConvert((double)c.ContractCode).Contains(term))
                        ).Union(
                            context.Contracts.AsNoTracking().Include(c => c.Consultant).Where(c => c.Consultant.Name.Contains(term))
                        ); ;

                    q = q
                        .Where(p => _AvailableDivs.Contains(p.ResponsibleDivisionId))
                        .OrderByDescending(c => c.ContractCode).Take(count);

                    r = q.ToList();
                }
                return r;
            }
        }
    }
}