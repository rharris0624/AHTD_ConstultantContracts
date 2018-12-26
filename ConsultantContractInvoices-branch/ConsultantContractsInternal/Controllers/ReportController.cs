using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
//using AHTD.Web.Mvc;
using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.ViewModels;
using ConsultantContractsInternal.Controllers;
using System.Net;

namespace ConsultantContractsInternal.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Search(int page = 1)
        {
            var model = new InvoiceSearchVM();
            model.page = page;
            
            if (Session["InvoiceFromDate"] != null)
                model.InvoiceFromDate = (DateTime)Session["InvoiceFromDate"];
            if (Session["InvoiceToDate"] != null)
                model.InvoiceToDate = (DateTime)Session["InvoiceToDate"];
            if (Session["PaymentFromDate"] != null)
                model.PaymentFromDate = (DateTime)Session["PaymentFromDate"];
            if (Session["PaymentToDate"] != null)
                model.PaymentToDate = (DateTime)Session["PaymentToDate"];
            if (Session["ConsultantId"] != null)
                model.ConsultantId = (int)Session["ConsultantId"];
            if (Session["ContractCode"] != null)
                model.ContractCode = (int)Session["ContractCode"];

            if (Session["query"] != null)
            {
                var query = (List<UnpaidInvoice>)Session["query"];
                model.page = model.page <= 0 ? 1 : model.page;
                ViewBag.CurrentPage = model.page;
                ViewBag.PagingList = SharedFunctions.PagerArray(model.page, SharedFunctions.PerPage, query.Count(), this, "Search", "Report");
                var pagingList = query
                    .Skip((model.page - 1) * SharedFunctions.PerPage)
                    .Take(SharedFunctions.PerPage)
                    .ToList<UnpaidInvoice>();
                model.UnpaidInvoices = pagingList;
            }

            using (var db = new Models.ConsultantContractsEntities())
            {
                ViewBag.ConsultantList = db.Consultants.OrderBy(d => d.Name).ToList().Select(v => new { value = v.ConsultantId, label = v.Name });
                ViewBag.ContractList = db.Contracts.Join(db.Consultants, s => s.ConsultantId, r => r.ConsultantId, (s, r) => new { s, r }).OrderBy(v => v.r.Name).ToList().Select(v => new { value = v.s.ContractCode, label = v.s.JobNo + v.s.ContractCode + " - " + v.r.Name });
                ViewBag.OrderByList = InvoiceSearchVM.OrderByFields.ToList().Select(v => new { value = v.Key, label = v.Value });
                ViewBag.SortOrderList = InvoiceSearchVM.SortOrders.ToList().Select(v => new { value = v.Key, label = v.Value });
                
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Search(ViewModels.InvoiceSearchVM data)
        {
            using (var db = new Models.ConsultantContractsEntities())
            {
                ViewBag.ConsultantList = db.Consultants.OrderBy(d => d.Name).ToList().Select(v => new { value = v.ConsultantId, label = v.Name });
                ViewBag.ContractList = db.Contracts.Join(db.Consultants, s => s.ConsultantId, r => r.ConsultantId, (s, r) => new { s, r }).OrderBy(v => v.s.ContractCode).ToList().Select(v => new { value = v.s.ContractCode, label = v.s.JobNo + v.s.ContractCode + " - " + v.r.Name });
                ViewBag.OrderByList = InvoiceSearchVM.OrderByFields.ToList().Select(v => new { value = v.Key, label = v.Value });
                ViewBag.SortOrderList = InvoiceSearchVM.SortOrders.ToList().Select(v => new { value = v.Key, label = v.Value });
                if (data != null
                    && (data.PaymentFromDate == null || data.PaymentFromDate.Equals(DateTime.MinValue))
                    && (data.PaymentToDate == null || data.PaymentToDate.Equals(DateTime.MinValue))
                    && (data.InvoiceFromDate == null || data.InvoiceFromDate.Equals(DateTime.MinValue))
                    && (data.InvoiceToDate == null || data.InvoiceToDate.Equals(DateTime.MinValue))
                    && data.ConsultantId.Equals(0)
                    && data.ContractCode.Equals(0))
                {
                    if (Session["InvoiceFromDate"] != null)
                        data.InvoiceFromDate = (DateTime)Session["InvoiceFromDate"];
                    if (Session["InvoiceToDate"] != null)
                        data.InvoiceToDate = (DateTime)Session["InvoiceToDate"];
                    if (Session["PaymentFromDate"] != null)
                        data.PaymentFromDate = (DateTime)Session["PaymentFromDate"];
                    if (Session["PaymentToDate"] != null)
                        data.PaymentToDate = (DateTime)Session["PaymentToDate"];
                    if (Session["ConsultantId"] != null)
                        data.ConsultantId = (int)Session["ConsultantId"];
                    if (Session["ContractCode"] != null)
                        data.ContractCode = (int)Session["ContractCode"];

                    if (Session["query"] != null)
                    {
                        var query = (List<UnpaidInvoice>)Session["query"];
                        data.page = data.page <= 0 ? 1 : data.page;
                        ViewBag.CurrentPage = data.page;
                        ViewBag.PagingList = SharedFunctions.PagerArray(data.page, SharedFunctions.PerPage, query.Count(), this, "Search", "Report");
                        var pagingList = query
                            .Skip((data.page - 1) * SharedFunctions.PerPage)
                            .Take(SharedFunctions.PerPage)
                            .ToList<UnpaidInvoice>();
                        data.UnpaidInvoices = pagingList;
                    }
                }
                else
                {
                    #region join
                    var query = db.InvoiceApproval.Join(
                            db.Invoices, s => s.InvoiceId, r => r.InvoiceId, (s, r) => new { s, r })
                            .Join(db.Contracts, t => t.r.ContractCode, u => u.ContractCode, (u, v) => new { u, v })
                            .Join(db.Consultants, w => w.v.ConsultantId, x => x.ConsultantId, (w, x) => new { w, x }).Distinct()
                            .GroupJoin(db.InvoicePayments,  a => a.w.u.s.InvoiceId, b => b.InvoiceId, (a, b) => new {a, b})
                            .SelectMany(s => s.b.DefaultIfEmpty(), (a, b) => new UnpaidInvoice(){ 
                                ContractCode = a.a.w.v.ContractCode,
                                ConsultantId = a.a.x.ConsultantId,
                                ConsultantName = a.a.x.Name,
                                InvoiceNo = a.a.w.u.r.InvoiceNo,
                                FAP = b.FAP,
                                Amount = b.Amount,
                                InvoiceDate = a.a.w.u.r.InvoiceDate,
                                T1FixedFee = a.a.w.u.r.T1FixedFee,
                                Status = a.a.w.u.s.Status
                        });
                    #endregion

                    #region order by
                    //if (data.SortOrder == "DESCENDING")
                    //{
                    if (data.OrderBy.Equals("ContractCode"))
                        query = query.OrderByDescending(c => c.ContractCode);
                    else if (data.OrderBy.Equals("InvoiceDate"))
                        query = query.OrderByDescending(c => c.InvoiceDate);
                    else if (data.OrderBy.Equals("ConsultantId"))
                        query = query.OrderByDescending(c => c.ConsultantId);
                    else if (data.OrderBy.Equals("ConsultantName"))
                        query = query.OrderBy(c => c.ConsultantName);
                    //}
                    #endregion
                    if (data.ConsultantId > 0)
                        query = query.Where(c => c.ConsultantId.Equals(data.ConsultantId));
                    if (data.ContractCode > 0)
                        query = query.Where(c => c.ContractCode.Equals(data.ContractCode));
                    if (!data.InvoiceFromDate.Equals(null) && !data.InvoiceFromDate.Equals(DateTime.MinValue))
                        query = query.Where(c => c.InvoiceDate >= data.InvoiceFromDate);
                    if (!data.InvoiceToDate.Equals(null) && !data.InvoiceToDate.Equals(DateTime.MinValue))
                        query = query.Where(c => c.InvoiceDate <= data.InvoiceToDate);

                    data.page = data.page <= 0 ? 1 : data.page;
                    ViewBag.CurrentPage = data.page;
                    ViewBag.PagingList = SharedFunctions.PagerArray(data.page, SharedFunctions.PerPage, query.Count(), this, "Search", "Report");
                    Session["query"] = query.ToList<UnpaidInvoice>();
                    if (data.InvoiceFromDate != DateTime.MinValue)
                        Session["InvoiceFromDate"] = data.InvoiceFromDate;
                    if (data.InvoiceToDate != DateTime.MinValue)
                        Session["InvoiceToDate"] = data.InvoiceToDate;
                    if (data.PaymentFromDate != DateTime.MinValue)
                        Session["PaymentFromDate"] = data.PaymentFromDate;
                    if (data.PaymentToDate != DateTime.MinValue)
                        Session["PaymentToDate"] = data.PaymentToDate;
                    if (data.ContractCode != 0)
                        Session["ContractCode"] = data.ContractCode;
                    if (data.ConsultantId != 0)
                        Session["ConsultantId"] = data.ConsultantId;

                    query = query
                        .Skip((data.page - 1) * SharedFunctions.PerPage)
                        .Take(SharedFunctions.PerPage)
                        .Select(c => c);
                    if (data.UnpaidInvoices == null)
                        data.UnpaidInvoices = new List<UnpaidInvoice>();
                    foreach (Models.UnpaidInvoice temp in query)
                    {
                        data.UnpaidInvoices.Add(temp);
                    }
                }
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult UnpaidInvoicesResults(ViewModels.InvoiceSearchVM viewModel)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();

            if (viewModel.ConsultantId != 0)
            {
                parms.Add("Consultant", viewModel.ConsultantId.ToString());
            }
            if(viewModel.ContractCode  != 0)
            {
                parms.Add("Contract", viewModel.ContractCode.ToString());
            }
            if (viewModel.InvoiceFromDate != null)
            {
                parms.Add("StartDate", Convert.ToDateTime(viewModel.InvoiceFromDate).ToString("MM/dd/yyyy"));
            }
            if (viewModel.InvoiceToDate != null)
            {
                parms.Add("EndDate", Convert.ToDateTime(viewModel.InvoiceToDate).ToString("MM/dd/yyyy"));
            }

            return File(getUnpaidInvoicesReport(parms, "PDF"), @"application\pdf", "UnpaidInvoices.pdf");
        }

        [HttpGet]
        public ActionResult ReportTemplate(InvoiceSearchVM invoiceVm)//(string orderBy, string sortBy, int consultantId = 0, int contractCode = 0, string fromDate = "", string toDate = "")
        {
            var rptInfo = new ReportInfo{
                ReportName = "UnpaidInvoices",
                ReportDescription = "List of Unpaid Invoices",  
                Width = 100,  
                Height = 650,
                Folder = ConfigurationManager.AppSettings["ReportFolder"],
                ReportUrl = String.Format("../../Reports/ReportTemplate.aspx?ReportName={0}&Height={1}&Folder={2}", "UnpaidInvoices", 650, "AHTDConsultantContracts")//ConfigurationManager.AppSettings["ReportServerURL"]
            };
            Dictionary<string, object> parms = new Dictionary<string, object>();

            if (invoiceVm.ConsultantId != 0)
            {
                rptInfo.ReportUrl += "&Consultant=" + invoiceVm.ConsultantId;
            }
            if (invoiceVm.ContractCode != 0)
            {
                rptInfo.ReportUrl += "&Contract=" + invoiceVm.ContractCode;
            }
            if (!(invoiceVm.InvoiceFromDate == null))
            {
                rptInfo.ReportUrl += "&StartDate=" + invoiceVm.InvoiceFromDate;
            }
            if (!(invoiceVm.InvoiceToDate == null))
            {
                rptInfo.ReportUrl += "&EndDate=" + invoiceVm.InvoiceToDate;
            }
            //if (!string.IsNullOrEmpty(invoiceVm.OrderBy))
            //{
            //    parms.Add("OrderBy", invoiceVm.OrderBy);
            //}
            //if (!string.IsNullOrEmpty(invoiceVm.SortOrder))
            //{
            //    parms.Add("SortBy", invoiceVm.SortOrder);
            //}
            //rptInfo.Parameters = parms;

            ViewBag.ReportInfo = rptInfo;

            return View(rptInfo);
        }

        [HttpPost]
        public ActionResult ReportPage(InvoiceSearchVM invoiceVm)
        {
            //UrlHelper url = new UrlHelper(Request.RequestContext);
            //var siteHome = url.
            var rptInfo = new ReportInfo
            {
                ReportName = "UnpaidInvoices",
                ReportDescription = "List of Unpaid Invoices",
                Width = 100,
                Height = 1460,
                Folder = ConfigurationManager.AppSettings["ReportFolder"],
                ReportUrl = string.Format("../Reports/ReportPage.aspx?ReportName={0}&Height={1}&Folder={2}", "UnpaidInvoices", 1460, "AHTDConsultantContracts") // ConfigurationManager.AppSettings["ReportServerURL"]
            };
            //rptInfo.ReportUrl +=
            //Dictionary<string, object> parms = new Dictionary<string, object>();

            //if (invoiceVm.ConsultantId != 0)
            //{
            //    parms.Add("Consultant", invoiceVm.ConsultantId);
            //}
            //if (invoiceVm.ContractCode != 0)
            //{
            //    parms.Add("Contract", invoiceVm.ContractCode);
            //}
            //if (!(invoiceVm.InvoiceFromDate == null))
            //{
            //    parms.Add("StartDate", invoiceVm.InvoiceFromDate.Value);
            //}
            //if (!(invoiceVm.InvoiceToDate == null))
            //{
            //    parms.Add("EndDate", invoiceVm.InvoiceToDate.Value);
            //}
            //if (!string.IsNullOrEmpty(invoiceVm.OrderBy))
            //{
            //    parms.Add("OrderBy", invoiceVm.OrderBy);
            //}
            //if (!string.IsNullOrEmpty(invoiceVm.SortOrder))
            //{
            //    parms.Add("SortBy", invoiceVm.SortOrder);
            //}
            //rptInfo.Parameters = parms;
            rptInfo.ReportUrl += "&consultantId=" + invoiceVm.ConsultantId;
            rptInfo.ReportUrl += "&contractCode=" + invoiceVm.ContractCode;
            if (!(invoiceVm.InvoiceFromDate == null))
            {
                rptInfo.ReportUrl += "&fromDate=" + invoiceVm.InvoiceFromDate;
            }
            if (!(invoiceVm.InvoiceToDate == null))
            {
                rptInfo.ReportUrl += "&toDate=" + invoiceVm.InvoiceToDate;
            }
            try
            {

                ViewBag.ReportInfo = rptInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(rptInfo);
        }

        [HttpGet]
        public ActionResult ReportPage2(InvoiceSearchVM invoiceVm)
        {
            var rptInfo = new ReportInfo
            {
                ReportName = "UnpaidInvoices",
                ReportDescription = "List of Unpaid Invoices",
                Width = 100,
                Height = 650,
                Folder = ConfigurationManager.AppSettings["ReportFolder"],
                ReportUrl = String.Format("../../Reports/ReportPage.aspx?ReportName={0}&Height={1}", "UnpaidInvoices", 650)//ConfigurationManager.AppSettings["ReportServerURL"]
            };
            //Dictionary<string, object> parms = new Dictionary<string, object>();

            if (Request["consultantId"] != null)
            {
                rptInfo.ReportUrl += "&consultantId=" + Request["consultantId"];
            }
            if (Request["contractCode"] != null)
            {
                rptInfo.ReportUrl += "&contractCode=" + Request["contractCode"];
            }
            if (Request["fromDate"] != null)
            {
                rptInfo.ReportUrl += "&fromDate=" + Request["fromDate"];
            }
            if (Request["toDate"] != null)
            {
                rptInfo.ReportUrl += "&toDate=" + Request["toDate"];
            }
            return View(rptInfo);
        }

        //[HttpPost]
        //public ActionResult ReportPage()
        //{
        //    InvoiceSearchVM invoiceVm = new InvoiceSearchVM
        //    {
        //        ConsultantId = 1,
        //        ConsultantName = "Garver LLC"
        //    };
        //    var rptInfo = new ReportInfo
        //    {
        //        ReportName = "UnpaidInvoices",
        //        ReportDescription = "List of Unpaid Invoices",
        //        Width = 100,
        //        Height = 650,
        //        Folder = ConfigurationManager.AppSettings["ReportFolder"],
        //        ReportUrl = ConfigurationManager.AppSettings["ReportServerURL"]
        //    };
        //    Dictionary<string, object> parms = new Dictionary<string, object>();

        //    if (invoiceVm.ConsultantId != 0)
        //    {
        //        parms.Add("Consultant", invoiceVm.ConsultantId);
        //    }
        //    if (invoiceVm.ContractCode != 0)
        //    {
        //        parms.Add("Contract", invoiceVm.ContractCode);
        //    }
        //    if (!(invoiceVm.InvoiceFromDate == null))
        //    {
        //        parms.Add("StartDate", invoiceVm.InvoiceFromDate.Value);
        //    }
        //    if (!(invoiceVm.InvoiceToDate == null))
        //    {
        //        parms.Add("EndDate", invoiceVm.InvoiceToDate.Value);
        //    }
        //    //if (!string.IsNullOrEmpty(invoiceVm.OrderBy))
        //    //{
        //    //    parms.Add("OrderBy", invoiceVm.OrderBy);
        //    //}
        //    //if (!string.IsNullOrEmpty(invoiceVm.SortOrder))
        //    //{
        //    //    parms.Add("SortBy", invoiceVm.SortOrder);
        //    //}
        //    rptInfo.Parameters = parms;
        //    try
        //    {

        //        ViewBag.ReportInfo = rptInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return View();
        //}

        private byte[] getUnpaidInvoicesReport(Dictionary<string, string> reportArgs, string format, ParameterValue[] rparams = null)
        {
            using (var rs = new ReportExecutionService())
            {
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rs.Url = ConfigurationManager.AppSettings["ReportServerURL"];
                byte[] result = null;
                string reportPath = "/ConsultantContracts/UnpaidInvoices";
                string historyID = null;
                string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

                ParameterValue[] parameters = null;
                if (reportArgs == null)
                {
                    parameters = rparams;
                }
                else
                {
                    parameters = new ParameterValue[reportArgs.Count];
                    int i = 0;
                    foreach (var reportArg in reportArgs)
                    {
                        parameters[i] = new ParameterValue { Name = reportArg.Key, Value = reportArg.Value };
                        i++;
                    }
                }
                Microsoft.Reporting.WebForms.DataSourceCredentials[] credentials = null;
                string encoding;
                string mimeType;
                string extension;
                Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution.Warning[] warnings = null;
                string[] streamIDs = null;

                ExecutionInfo execInfo = new ExecutionInfo();
                ExecutionHeader execHeader = new ExecutionHeader();

                try
                {
                    rs.ExecutionHeaderValue = execHeader;
                    execInfo = rs.LoadReport(reportPath, historyID);

                    rs.SetExecutionParameters(parameters, "en-us");
                    string SessionId = rs.ExecutionHeaderValue.ExecutionID;
                    result = rs.Render(format, devInfo, out extension, out mimeType, out encoding, out warnings, out streamIDs);

                    execInfo = rs.GetExecutionInfo();

                    Console.WriteLine("Execution date and time: {0}", execInfo.ExecutionDateTime);
                }
                catch (System.Web.Services.Protocols.SoapException e)
                {
                    throw new HttpException(500, e.Message);
                }

                return result;
            }
        }
    }
}
