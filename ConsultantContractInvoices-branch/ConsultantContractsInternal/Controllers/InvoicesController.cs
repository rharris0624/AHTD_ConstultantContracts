using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using System.Data.Entity.SqlServer;
using SP = Microsoft.SharePoint.Client;
using ArDOT_UserProv.Client.API;
using ConsultantContractsInternal.Domain;
using Elmah;
using ConsultantContractsInternal.Security.Attributes;
using System.Web;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace ConsultantContractsInternal.Controllers
{
    public class InvoicesController : SecuredController
    {
        private IList<int> _AvailableDivs;
        private IList<ContractPermissionBudget> _ContractPermissions;

        public InvoicesController()
        {
            using(var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = UserProvHelpers.AvailableDivisions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""));
                _ContractPermissions = ConsultantContractsInternal.Utilities.UserProvHelpers.GetContractPermissions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""), _AvailableDivs);
            }
        }

        public ActionResult Index()
        {
            if (Roles.CanCreateInvoice.Split(',').Contains(CurrentUser.Role))
                ViewBag.WriteAccess = true;
            else
                ViewBag.WriteAccess = false;

            return View();

        }

        public enum InvoiceState
        {
            All,
            Created,  //JL40985  2/27/2017  The "Created" invoice status is no longer used ("Review" is now used instead).
            Review,
            Submitted,
            Recommended,
            Approved,
            waitingtobePaid,
            Paid,
            Audited,
            Rejected
        }

        public IQueryable<Invoice> StateFilter(IQueryable<Invoice> q, InvoiceState state)
        {

            
                //var InvoicesToReviewList = invoice.Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceCreated && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).ToList();
            switch (state)
            {

                case InvoiceState.Recommended:
                   // return
                         //q.Where(i => i.InvoiceApproval.RecommendDate.HasValue && !i.InvoiceApproval.ApprovedDate.HasValue);
                     //   q.Where(i => i.InvoiceApproval.Where(x=>x.InvoiceId ==i.InvoiceId && x.Status==InvoiceStatus.InvoiceRecommended  && x.ActiveStatus)).Select(s=>s);
                //case InvoiceState.Submitted:
                //    return
                //        q.Where(i => i.SubmittedDate.HasValue && !i.InvoiceApproval.RecommendDate.HasValue);
                //        q.Where(i => i.InvoiceApproval.ApprovedDate.HasValue && !i.InvoiceApproval.VoucherDate.HasValue);
                //case InvoiceState.Paid:
                //    return
                //        q.Where(i => i.InvoiceApproval.VoucherDate.HasValue);
                //case InvoiceState.All:
                //    return q;
                default:
                    return q;
            }
        }

        public ActionResult Search(string term, InvoiceState state = InvoiceState.All)
        {
            var terms = term.Split(' ');
            using (var context = new ConsultantContractsEntities())
            {
                IQueryable<Invoice> q = null;
                if (String.IsNullOrWhiteSpace(term))
                {
                    q = context.Invoices.Include(i => i.InvoiceApproval)
                                        .Include(i => i.Contract)
                                        .Include(i => i.Contract.Consultant);

                    q = StateFilter(q, state);
                    q = q.RevisionFilter();
                    q = q.OrderByDescending(i => i.InvoiceDate).Take(20);
                }
                else
                {
                    if (terms.Length == 1)
                    {
                        term = terms[0];

                        var byJobNo = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => i.Contract.JobNo.Contains(term))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);
                        var byContractCode = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => SqlFunctions.StringConvert((decimal)i.Contract.ContractCode).Contains(term))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);
                        var byConsultantName = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => i.Contract.Consultant.Name.Contains(term))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);
                        var byInvoiceNo = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => SqlFunctions.StringConvert((decimal)i.InvoiceNo).Contains(term))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);
                        var byConsultantInvoiceNo = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => i.ConsultantInvoiceNo.Contains(term))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);

                        q = byJobNo.Union(byContractCode)
                           .Union(byConsultantName)
                           .Union(byInvoiceNo)
                           .Union(byConsultantInvoiceNo);
                    }
                    else if (terms.Length > 1)
                    {
                        var byJobNo = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => i.Contract.JobNo.Contains(terms[0]))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);

                        var byContractCode = context.Invoices
                            .Include(i => i.InvoiceApproval)
                            .Include(i => i.Contract)
                            .Include(i => i.Contract.Consultant)
                            .Where(i => SqlFunctions.StringConvert((decimal)i.Contract.ContractCode).Contains(terms[0]))
                            .RevisionFilter()
                            .OrderBy(i => i.LastUpdateDate);

                        var byJobNoByInvoiceNo =
                            byJobNo.Where(i => SqlFunctions.StringConvert((decimal)i.InvoiceNo).Contains(terms[1]));
                        var byContractCodeByInvoiceNo =
                            byContractCode.Where(i => SqlFunctions.StringConvert((decimal)i.InvoiceNo).Contains(terms[1]));

                        var byJobNoByConsultantInvoiceNo =
                            byJobNo.Where(i => i.ConsultantInvoiceNo.Contains(terms[1]));
                        var byContractCodeByConsultantInvoiceNo =
                            byContractCode.Where(i => i.ConsultantInvoiceNo.Contains(terms[1]));

                        q =
                            byJobNoByInvoiceNo.Union(byContractCodeByInvoiceNo)
                                .Union(byJobNoByConsultantInvoiceNo)
                                .Union(byContractCodeByConsultantInvoiceNo);
                    }
                    if (q != null)
                    {
                        q = StateFilter(q, state);
                        q = q.RevisionFilter();
                        q = q.OrderByDescending(i => i.InvoiceDate).Take(20);
                    }
                    else
                    {
                        throw new InvalidOperationException("Call Jesus, how did this even happen");
                    }
                }
                var r = q.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId)).ToList().Select(i => new
                {
                    i.InvoiceId,
                    i.Contract.JobNo,
                    ConsultantName = i.Contract.Consultant.Name,
                    i.Contract.ConsultantId,
                    i.ContractCode,
                    i.InvoiceNo,
                    i.InvoiceDate,
                    i.ConsultantInvoiceNo,
                    i.InvoiceApproval,
                    Total = (i.T1Svcs ?? 0 + i.T2Svcs ?? 0).ToString("C2")
                }).ToList();

                var t = r.Join(_ContractPermissions, a => a.ContractCode, b => b.ContractId, (a, b) => new { a, b }).Select(i => new
                {
                    i.a.InvoiceId,
                    i.a.JobNo,
                    i.a.ConsultantName,
                    i.a.ConsultantId,
                    i.a.ContractCode,
                    i.a.InvoiceNo,
                    i.a.InvoiceDate,
                    i.a.ConsultantInvoiceNo,
                    i.a.InvoiceApproval,
                    i.a.Total,
                    i.b.PermissionId,
                });

                return Json(t.Select(i => new { label = string.Format("{3} {0}-{1}: {2}", i.JobNo.Trim(), i.ContractCode, i.InvoiceNo, i.ConsultantName), data = i }), JsonRequestBehavior.AllowGet);
                //return Json(r.Select(i => new { label = string.Format("{3} {0}-{1}: {2}", i.JobNo.Trim(), i.ContractCode, i.InvoiceNo, i.ConsultantName), data = i }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RecentInvoices(int count = 5, bool simpleView = false, int? contractCode = null, int? consultantId = null)
        {
            using (var context = new ConsultantContractsEntities())
            {
                ViewBag.simple = simpleView;
                var invoice = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                    .Include(i => i.InvoiceApproval)
                    .Include(i => i.Contract)
                    .Include(i => i.SubConsultant)
                    .Include(i => i.Contract.Consultant);

                if (contractCode.HasValue)
                    invoice = invoice.Where(i => i.ContractCode == contractCode);

                if (consultantId.HasValue)
                    invoice = invoice.Where(i => i.SubConsultantId == consultantId || i.Contract.ConsultantId == consultantId);

                var invoiceList = invoice.OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).ToList();
                var invlist = invoiceList.Distinct().Take(count).ToList();
                if (invlist.Count == 0)
                    return Content("No Invoices Found");

                return PartialView(invlist);
            }
        }

        #region AdminHome Page

        /// InvoicesToRecomend is changed to InvoicesToReview
        [OutputCache(Duration = 01)]
        [AHTDAuthorize(Roles = Roles.ReviewerRoles)]
        public ActionResult InvoicesToReview()
        {
            using (var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = ConsultantContractsInternal.Utilities.UserProvHelpers.AvailableDivisions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""));
                var availableContractCodes = context.Contracts.Where(p => _AvailableDivs.Contains(p.ResponsibleDivisionId)).Select(i => i.ContractCode);
                var invoice = context.Invoices.Where(p => availableContractCodes.Contains(p.ContractCode)).AsNoTracking()
                        .Include(i => i.Contract).AsNoTracking()
                        .Include(i => i.SubConsultant).AsNoTracking()
                        .Include(i => i.Contract.Consultant).AsNoTracking()
                        .Include(i => i.InvoiceApproval);
                var InvoicesToReviewList = invoice.Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceCreated && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).ToList();
                var InvToReViewLst = InvoicesToReviewList.Distinct().Take(10).ToList();
                if (InvToReViewLst.Count == 0)
                    return Content("No Invoices Found");
                
                return PartialView("InvoicesToReview", InvToReViewLst);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult GetInvoicesToRecommend()
        {
            using (var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = ConsultantContractsInternal.Utilities.UserProvHelpers.AvailableDivisions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""));
                var RecommendedInvoices = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                 .Include(i => i.InvoicePayments).AsNoTracking()
                 .Include(i => i.Contract).AsNoTracking()
                 .Include(i => i.Contract.Consultant).AsNoTracking()
                 .Include(i => i.InvoiceApproval).AsNoTracking();

                var _recommendedInvoicesListByInvoiceApproval = RecommendedInvoices.AsNoTracking().Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceSubmitted && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(10).ToList();

                var recommendedInvoices = _recommendedInvoicesListByInvoiceApproval.ToList().Select(i => new
                {
                    i.Contract.JobNo,
                    i.Contract.Consultant.Name,
                    i.Contract.ConsultantId,
                    i.Contract.ContractCode,
                    i.InvoiceNo,
                    i.InvoiceId,
                    Allotments = i.InvoicePayments.Select(ip => new
                    {
                        ip.Func,
                        ip.FAP,
                        ip.Amount
                    }),
                    InvoiceDate = i.InvoiceDate.ToShortDateString()
                });

                return Json(recommendedInvoices.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [OutputCache(Duration = 01)]
        //[AHTDAuthorize(Roles = Roles.ApproverRoles)]
        public ActionResult GetInvoicesToApprove()
        {
            using (var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = ConsultantContractsInternal.Utilities.UserProvHelpers.AvailableDivisions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""));
                var RecommendedInvoices = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                 .Include(i => i.InvoicePayments).AsNoTracking()
                 .Include(i => i.Contract).AsNoTracking()
                 .Include(i => i.Contract.Consultant).AsNoTracking()
                 .Include(i => i.InvoiceApproval).AsNoTracking();

                //var _recomendedInvoicesListByInvoiceApproval = RecomendedInvoices.AsNoTracking().Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceApproval && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(10).ToList();
                var _recommendedInvoicesListByInvoiceApproval = RecommendedInvoices.AsNoTracking().Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceRecommended && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(10).ToList();


                var recommendedInvoices = _recommendedInvoicesListByInvoiceApproval.ToList().Select(i => new
                {
                    i.Contract.JobNo,
                    i.Contract.Consultant.Name,
                    i.Contract.ConsultantId,
                    i.Contract.ContractCode,
                    i.InvoiceNo,
                    i.InvoiceId,
                    Allotments = i.InvoicePayments.Select(ip => new
                    {
                        ip.Func,
                        ip.FAP,
                        ip.Amount
                    }),
                    InvoiceDate = i.InvoiceDate.ToShortDateString()
                });

                return Json(recommendedInvoices.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        /// ----Waiting To be Paid
        [HttpGet]
        [OutputCache(Duration = 01)]
        //[AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult GetWaitingToBePaid()
        {
            using (var context = new ConsultantContractsEntities())
            {
                _AvailableDivs = ConsultantContractsInternal.Utilities.UserProvHelpers.AvailableDivisions(context, Regex.Replace(Thread.CurrentPrincipal.Identity.Name, @".*\\", ""));
                var WaitingToBePaid = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                    .Include(i => i.InvoicePayments).AsNoTracking()
                    .Include(i => i.Contract).AsNoTracking()
                    .Include(i => i.Contract.Consultant).AsNoTracking()
                    .Include(i => i.InvoiceApproval).AsNoTracking();

                //var _waitingToBePaidListByInvoiceApproval = WaitingToBePaid.AsNoTracking().Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceWaiting && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(10).ToList();
                var _waitingToBePaidListByInvoiceApproval = WaitingToBePaid.AsNoTracking().Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoiceApproval && i.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(10).ToList();

                var WaitingToBePaidList = _waitingToBePaidListByInvoiceApproval.ToList().Select(i => new
                {
                    i.Contract.JobNo,
                    i.Contract.Consultant.Name,
                    i.Contract.ConsultantId,
                    i.Contract.ContractCode,
                    i.InvoiceNo,
                    i.InvoiceId,
                    Allotments = i.InvoicePayments.Select(ip => new
                    {
                        ip.Func,
                        ip.FAP,
                        ip.Amount
                    }),
                    InvoiceDate = i.InvoiceDate.ToShortDateString(),
                });

                return Json(WaitingToBePaidList, JsonRequestBehavior.AllowGet);
            }
        }

        //---------Recently Paid   
        [HttpGet]
        [OutputCache(Duration = 01)]
        //[AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult GetPaidList(int count = 10, bool simpleView = false, int? contractCode = null, int? consultantId = null)
        {
            using (var context = new ConsultantContractsEntities())
            {
                ViewBag.simple = simpleView;

                var PaidList = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                    .Include(i => i.InvoicePayments).AsNoTracking()
                    .Include(i => i.Contract).AsNoTracking()
                    .Include(i => i.Contract.Consultant).AsNoTracking()
                    .Include(i => i.InvoiceApproval).AsNoTracking();

                var _paidListByInvoiceApproval = PaidList.Where(p => p.InvoiceId == p.InvoiceApproval.Where(i => i.Status == InvoiceStatus.InvoicePaid).Select(s => s.InvoiceId).FirstOrDefault()).OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.InvoiceNo).Take(count).ToList();

                return PartialView("RecentInvoices", _paidListByInvoiceApproval);
            }
        }

        /// -------Rejected Invoices.        
        [HttpGet]
        [OutputCache(Duration = 01)]
        [AHTDAuthorize(Roles = Roles.RejectRoles)]
        public ActionResult GetRejectedInvoices()
        {
            using (var context = new ConsultantContractsEntities())
            {
                var RejectedInvoices = context.Invoices.Where(p => _AvailableDivs.Contains(p.Contract.ResponsibleDivisionId))
                    .Include(i => i.InvoiceApproval).AsNoTracking()
                    .Include(i => i.InvoicePayments).AsNoTracking()
                    .Include(i => i.Contract).AsNoTracking()
                    .Include(i => i.Contract.Consultant).AsNoTracking();

                RejectedInvoices = RejectedInvoices.AsNoTracking().Where(i => i.InvoiceId == i.InvoiceApproval.Where(p => p.Status == InvoiceStatus.InvoiceRejected && p.ActiveStatus == true).Select(s => s.InvoiceId).FirstOrDefault());

                var rejectedInvoices = RejectedInvoices.ToList().Select(i => new
                {
                    i.Contract.JobNo,
                    i.Contract.Consultant.Name,
                    i.Contract.ConsultantId,
                    i.Contract.ContractCode,
                    i.RevisionNo,
                    i.InvoiceNo,
                    i.InvoiceId,
                    Allotments = i.InvoicePayments.Select(ip => new
                    {
                        ip.Func,
                        ip.FAP,
                        ip.Amount
                    }),
                    InvoiceDate = i.InvoiceDate.ToShortDateString(),
                    RejectedReason = context.InvoiceApproval.Where(p => p.Status == InvoiceStatus.InvoiceRejected && p.InvoiceId == i.InvoiceId).OrderByDescending(s => s.StatusDate).Select(s => s.RejectedReason).FirstOrDefault(),
                    RejectedDate = context.InvoiceApproval.Where(p => p.Status == InvoiceStatus.InvoiceRejected && p.InvoiceId == i.InvoiceId).OrderByDescending(s => s.StatusDate).Select(s => s.StatusDate).FirstOrDefault().ToShortDateString()
                });
                return Json(rejectedInvoices.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [OutputCache(Duration = 01)]
        [AHTDAuthorize(Roles = Roles.InvoiceEntryRoles)]
        public JsonResult GetConsultantJobNoList(string term, string jobNo, int contractCode = 0)
        {
            IEnumerable<String> consultantJobNoList;
            using (var context = new ConsultantContractsEntities())
            {
                var consultantIdList = from contract in context.Contracts
                                       join consultant in context.Consultants on contract.ConsultantId equals consultant.ConsultantId
                                       where contract.ContractCode == contractCode && contract.JobNo == jobNo.TrimEnd()
                                       select consultant.ConsultantId;
                if (consultantIdList.Count() > 0)
                {
                    int key = Convert.ToInt32(consultantIdList.FirstOrDefault());
                    
                    consultantJobNoList = from invoice in context.Invoices
                                          join contract in context.Contracts on invoice.ContractCode equals contract.ContractCode
                                          join consultant in context.Consultants on contract.ConsultantId equals consultant.ConsultantId
                                          where contract.ContractCode == contractCode && 
                                                consultant.ConsultantId == key && 
                                                invoice.ConsultantJobNo != null && 
                                                invoice.ConsultantJobNo.Contains(term.TrimEnd())
                                          select invoice.ConsultantJobNo;
                    if (consultantJobNoList.Count() > 0)
                    {
                        var availableItems = ListUtil.Distinct(consultantJobNoList, (p, p1) => p == p1);
                        return Json(availableItems.ToList(), JsonRequestBehavior.AllowGet);
                    }
                }
                return null;
            }
        }

        //[AHTDAuthorize(Roles = Roles.ApproverRoles)]
        public ActionResult WaitingTobePaidForHomePage()
        {
            return PartialView("~/Views/Invoices/ApproveInvoices.cshtml");
        }

        [AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult RecommendInvoicesForHomePage()
        {
            if (Roles.CanRecommend.Split(',').Contains(CurrentUser.Role))
                ViewBag.WriteAccess = true;
            else
                ViewBag.WriteAccess = false;

            return PartialView("~/Views/Invoices/RecommendInvoices.cshtml");
        }

        [AHTDAuthorize(Roles = Roles.RejectRoles)]
        public ActionResult RejectedInvoicesForHomePage()
        {
            return PartialView("~/Views/Invoices/RejectedInvoices.cshtml");
        }

        [AHTDAuthorize(Roles = Roles.ApproverRoles)]
        public ActionResult ApprovalInvoicesForHomePage()
        {
            if (Roles.CanApprove.Split(',').Contains(CurrentUser.Role))
                ViewBag.WriteAccess = true;
            else
                ViewBag.WriteAccess = false;

            return PartialView("~/Views/Invoices/ApprovalInvoice.cshtml");
        }

        #endregion

        #region invoice Status Update
        [HttpPost]
        [AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult SubmitInvoice(int id, string status)
        {
            using (var context = new ConsultantContractsEntities())
            {
                if (id > 0)
                {
                    var invoice = context.Invoices.Include(i => i.InvoiceApproval).SingleOrDefault(i => i.InvoiceId == id);
                    if (invoice == null)
                        return Json(new { Status = "ERROR", Msg = "Not Found" });

                    var _changestatusInvoiceApproval = context.InvoiceApproval.Where(p => p.InvoiceId == id && p.Status == InvoiceStatus.InvoiceCreated && p.ActiveStatus == true).FirstOrDefault();
                    if (_changestatusInvoiceApproval != null)
                    {
                        _changestatusInvoiceApproval.ActiveStatus = false;
                    }
                    else
                    {
                        return Json(new { Status = "ERROR", Msg = "Not Found" });
                    }
                    InvoiceApproval _invoiceApproval = new InvoiceApproval();
                    _invoiceApproval.Status = InvoiceStatus.InvoiceSubmitted;
                    _invoiceApproval.StatusDate = DateTime.Now;
                    _invoiceApproval.ActiveStatus = true;
                    _invoiceApproval.UserID = Regex.Replace(@"ahtd\something", @".*\\", "");
                    _invoiceApproval.InvoiceId = id;

                    context.InvoiceApproval.Add(_invoiceApproval);
                    context.SaveChanges();
                    return Json(new { Status = "OK" });
                }
            }
            return Json(new { Status = "OK" });
        }

        [AHTDAuthorize(Roles = Roles.RecommendRoles)]
        public ActionResult RecommendInvoice(int id, string status)
        {
            using (var context = new ConsultantContractsEntities())
            {
                if (id > 0)
                {
                    var invoice = context.Invoices.Include(i => i.InvoiceApproval).SingleOrDefault(i => i.InvoiceId == id);
                    if (invoice == null)
                        return Json(new { Status = "ERROR", Msg = "Not Found" });

                    //var _changestatusInvoiceApproval = context.InvoiceApproval.Where(p => p.InvoiceId == id && p.Status == InvoiceStatus.InvoiceRecommended && p.ActiveStatus == true).FirstOrDefault();
                    var _changestatusInvoiceApproval = context.InvoiceApproval.Where(p => p.InvoiceId == id && p.Status == InvoiceStatus.InvoiceSubmitted && p.ActiveStatus == true).FirstOrDefault();
                    if (_changestatusInvoiceApproval != null)
                    {
                        _changestatusInvoiceApproval.ActiveStatus = false;
                    }
                    else
                    {
                        return Json(new { Status = "ERROR", Msg = "something went wrong" });
                    }

                    InvoiceApproval _invoiceApproval = new InvoiceApproval();
                    _invoiceApproval.Status = InvoiceStatus.InvoiceRecommended;
                    _invoiceApproval.StatusDate = DateTime.Now;
                    _invoiceApproval.ActiveStatus = true;
                    _invoiceApproval.UserID = Regex.Replace(@"ahtd\something", @".*\\", "");
                    _invoiceApproval.InvoiceId = id;

                    context.InvoiceApproval.Add(_invoiceApproval);
                    context.SaveChanges();
                }
            }
            return Json(new { Status = "OK" });
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ApproverRoles)]
        public ActionResult ApproveInvoice(int id, string status)
        {
            using (var context = new ConsultantContractsEntities())
            {
                if (id > 0)
                {
                    ViewBag.Role = CurrentUser.Role;
                    var invoice = context.Invoices.Include(i => i.InvoiceApproval).SingleOrDefault(i => i.InvoiceId == id);
                    if (invoice == null)
                        return Json(new { Status = "ERROR", Msg = "Not Found" });

                    var _changestatusInvoiceApproval = context.InvoiceApproval.Where(p => p.InvoiceId == id && p.Status == InvoiceStatus.InvoiceRecommended && p.ActiveStatus == true).FirstOrDefault();
                    if (_changestatusInvoiceApproval != null)
                    {
                        _changestatusInvoiceApproval.ActiveStatus = false;
                    } 
                    else
                    {
                        return Json(new { Status = "ERROR", Msg = "something went wrong" });
                    }

                        InvoiceApproval _invoiceApproval = new InvoiceApproval();
                        _invoiceApproval.Status = InvoiceStatus.InvoiceApproval;
                        _invoiceApproval.StatusDate = DateTime.Now;
                        _invoiceApproval.ActiveStatus = true;
                        _invoiceApproval.UserID = Regex.Replace(@"ahtd\something", @".*\\", "");
                        _invoiceApproval.InvoiceId = id;

                        context.InvoiceApproval.Add(_invoiceApproval);
                        context.SaveChanges();
                }
            }
            return Json(new { Status = "OK" });
        }

        [HttpPost]
        public ActionResult RejectInvoice(int id, string source, string reason)
        {

            using (var context = new ConsultantContractsEntities())
            {
                if (id > 0)
                {
                    var invoice = context.Invoices.Include(i => i.InvoiceApproval).SingleOrDefault(i => i.InvoiceId == id);
                    if (invoice == null)
                        return Json(new { Status = "ERROR", Msg = "Not Found" });


                    var _rejectInvoice = context.InvoiceApproval.Where(p => p.InvoiceId == id && p.Status != InvoiceStatus.InvoicePaid).ToList();
                    if (_rejectInvoice != null)
                    {
                        foreach (var li in _rejectInvoice)
                        {
                            li.ActiveStatus = false;
                        }
                        InvoiceApproval _invoiceApproval = new InvoiceApproval();
                        _invoiceApproval.Status = InvoiceStatus.InvoiceRejected;
                        _invoiceApproval.StatusDate = DateTime.Now;
                        _invoiceApproval.RejectedReason = reason;
                        _invoiceApproval.ActiveStatus = true;
                        _invoiceApproval.UserID = Regex.Replace(@"ahtd\something", @".*\\", "");
                        _invoiceApproval.InvoiceId = id;
                        context.InvoiceApproval.Add(_invoiceApproval);
                        context.SaveChanges();
                    }
                    else
                    {
                        return Json(new { Status = "ERROR", Msg = "something went wrong" });
                    }
                }
            }
            return Json(new { Status = "OK" });
        }

        #endregion

        #region Add && Modify Invoice

        [AHTDAuthorize(Roles = Roles.InvoiceEntryRoles)]
        public ActionResult AddSimpleInvoice(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.contractCode = id;
            }
            else
            {
                ViewBag.contractCode = null;
            }

            return View();
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.InvoiceEntryRoles)]
        public ActionResult AddSimpleInvoice(SimpleInvoiceVM model)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var now = DateTime.Now;
                var contract = context.Contracts.Include(c => c.Consultant).Single(c => c.ContractCode == model.ContractCode);
                var invoices = context.Invoices.Where(c => c.ContractCode == model.ContractCode).Select(i => i.InvoiceNo);
                var newInvoiceNo = 1;
                if (invoices.Any())
                {
                    newInvoiceNo = invoices.Max() + 1;
                }
                var newInvoice = model.PopulateInvoice(context.Invoices.Create(), contract);
                newInvoice.InvoiceNo = newInvoiceNo;
                newInvoice.ConsultantInvoiceNo = model.ConsultantInvoiceNo;
                newInvoice.ConsultantJobNo = model.ConsultantJobNo;
                newInvoice.LastUpdateDate = now;
                newInvoice.LastUpdateUser = Regex.Replace(@"ahtd\something", @".*\\", "");

                if (model.T1Allotments != null)
                {
                    model.T1Allotments.ForEach(avm =>
                    {
                        if (avm.AmountToPay == null) return;

                        var a = avm.ToInvoicePayment();

                        newInvoice.InvoicePayments.Add(a);
                    });
                }

                if (model.T2Allotments != null)
                {
                    model.T2Allotments.ForEach(avm =>
                    {
                        if (avm.AmountToPay == null) return;

                        var a = avm.ToInvoicePayment();

                        newInvoice.InvoicePayments.Add(a);
                    });
                }

                newInvoice.IsSimple = true;

                newInvoice.SubmittedDate = now;

                newInvoice.PhysicalAddress = contract.Consultant.PhysicalAddress;
                newInvoice.State = contract.Consultant.State;
                newInvoice.City = contract.Consultant.City;
                newInvoice.CountryCode = contract.Consultant.CountryCode;
                newInvoice.PostalCode = contract.Consultant.PostalCode;
                newInvoice.EmailAddress = contract.Consultant.EmailAddress;

                newInvoice.InvoiceApproval.Add(new InvoiceApproval
                {
                    ActiveStatus = true,
                    Status = InvoiceStatus.InvoiceCreated,
                    UserID = Regex.Replace(@"ahtd\something", @".*\\", ""),
                    StatusDate = DateTime.Now
                });

                contract.Invoices.Add(newInvoice);
                context.SaveChanges();

                return Json(new { Status = "OK", id = newInvoice.InvoiceId });
            }
        }

        [AHTDAuthorize(Roles = Roles.InvoiceEntryRoles)]
        public ActionResult ReviseSimpleInvoice(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var invoice = context.Invoices.Include(i => i.InvoiceApproval)
                    .Include(i => i.InvoicePayments)
                    .Include(i => i.Contract)
                    .Include(i => i.Contract.Consultant)
                    .SingleOrDefault(i => i.InvoiceId == id);

                if (invoice == null) return new HttpNotFoundResult("invoice not found");

                var vm = new SimpleInvoiceVM(invoice);

                return View(vm);
            }
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.InvoiceEntryRoles)]
        public ActionResult ReviseSimpleInvoice(SimpleInvoiceVM model)
        {
            using (var context = new ConsultantContractsEntities())
            {
                try
                {
                    var now = DateTime.Now;

                    var invoice = context.Invoices.Include(i => i.InvoicePayments).SingleOrDefault(i => i.InvoiceId == model.InvoiceId.Value);

                    if (invoice == null)
                        return new HttpNotFoundResult("Invoice Not Found");

                    invoice.BeginDate = model.BeginDate;
                    invoice.EndDate = model.EndDate;
                    invoice.SubmittedDate = now;
                    invoice.ConsultantInvoiceNo = model.ConsultantInvoiceNo;
                    invoice.ConsultantJobNo = model.ConsultantJobNo;
                    invoice.T1FixedFee = model.T1FixedFee;
                    invoice.T2FixedFee = model.T2FixedFee;
                    invoice.LastUpdateDate = now;
                    invoice.LastUpdateUser = Regex.Replace(@"ahtd\something", @".*\\", "");
                    invoice.HomeOfficeOverheadRateMax = model.HomeOfficeOverheadRateMax;
                    invoice.FieldServiceOverheadRateMax = model.FieldServiceOverheadRateMax;

                    if (model.T1Allotments != null)
                    {
                        decimal totalT1Svcs = 0;

                        model.T1Allotments.ForEach(avm =>
                        {
                            if (!avm.AmountToPay.HasValue) return;
                            if (invoice.InvoicePayments.Count > 0)
                            {
                                var invoicePayment = invoice.InvoicePayments.FirstOrDefault(ip => ip.Func == avm.Func && ip.FAP == avm.FAP);
                                if (invoicePayment != null)
                                    invoicePayment.Amount = avm.AmountToPay.Value;
                                totalT1Svcs += avm.AmountToPay.Value;
                            }
                        });

                        invoice.T1Svcs = totalT1Svcs;
                    }

                    if (model.T2Allotments != null)
                    {
                        decimal totalT2Svcs = 0;

                        model.T2Allotments.ForEach(avm =>
                        {
                            if (!avm.AmountToPay.HasValue) return;
                            if (invoice.InvoicePayments.Count > 0)
                            {
                                var invoicePayment = invoice.InvoicePayments.FirstOrDefault(ip => ip.Func == avm.Func && ip.FAP == avm.FAP);
                                if (invoicePayment != null)
                                    invoicePayment.Amount = avm.AmountToPay.Value;
                                totalT2Svcs += avm.AmountToPay.Value;
                            }
                        });

                        invoice.T2Svcs = totalT2Svcs;
                    }

                    var _rejectInvoice = context.InvoiceApproval.Where(p => p.InvoiceId == invoice.InvoiceId && p.Status == InvoiceStatus.InvoiceRejected && p.ActiveStatus == true).FirstOrDefault();
                    if (_rejectInvoice != null)
                        _rejectInvoice.ActiveStatus = false;

                    InvoiceApproval _invoiceApproval = new InvoiceApproval();
                    _invoiceApproval.Status = model.status;
                    _invoiceApproval.StatusDate = DateTime.Now;
                    _invoiceApproval.ActiveStatus = true;
                    _invoiceApproval.UserID = Regex.Replace(@"ahtd\something", @".*\\", "");
                    _invoiceApproval.InvoiceId = invoice.InvoiceId;
                    context.InvoiceApproval.Add(_invoiceApproval);

                    context.SaveChanges();
                    return Json(new { Status = "OK", id = invoice.InvoiceId });
                }
                catch (Exception ex)
                {
                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
        }

        public class SimpleInvoiceVM
        {
            public int ContractCode { get; set; }
            public int? InvoiceNo { get; set; }
            public string ConsultantInvoiceNo { get; set; }
            public string ConsultantJobNo { get; set; }
            public int? InvoiceId { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal? T1FixedFee { get; set; }
            public decimal? T2FixedFee { get; set; }
            public List<InvoiceAllotmentVM> T1Allotments { get; set; }
            public List<InvoiceAllotmentVM> T2Allotments { get; set; }
            public string RejectReason { get; set; }
            public string RejectDate { get; set; }
            public string status { get; set; }
            public decimal? HomeOfficeOverheadRateMax { get; set; }
            public decimal? FieldServiceOverheadRateMax { get; set; }

            public SimpleInvoiceVM() { }

            public SimpleInvoiceVM(Invoice invoice)
            {
                ContractCode = invoice.ContractCode;
                InvoiceNo = invoice.InvoiceNo;
                ConsultantInvoiceNo = invoice.ConsultantInvoiceNo;
                ConsultantJobNo = invoice.ConsultantJobNo;
                InvoiceId = invoice.InvoiceId;
                BeginDate = invoice.BeginDate.AddDays(1);  //JL40985 3/7/2017  Add 1 day so correct date is displayed on Revise Simple Invoice screen (not sure why this is necessary).
                EndDate = invoice.EndDate.AddDays(1);  //JL40985 3/7/2017  Add 1 day so correct date is displayed on Revise Simple Invoice screen (not sure why this is necessary).
                T1FixedFee = invoice.T1FixedFee;
                T2FixedFee = invoice.T2FixedFee;
                HomeOfficeOverheadRateMax = invoice.Contract.HomeOfficeOverheadRateMax;
                FieldServiceOverheadRateMax = invoice.Contract.FieldServiceOverheadRateMax;
                var tempRejectedInvoice = invoice.InvoiceApproval.Where(p => p.Status == InvoiceStatus.InvoiceRejected && p.ActiveStatus == true && p.InvoiceId == invoice.InvoiceId).OrderByDescending(s => s.StatusDate).FirstOrDefault();
                if (tempRejectedInvoice != null)
                {
                    RejectReason = tempRejectedInvoice.RejectedReason;
                    RejectDate = tempRejectedInvoice.StatusDate.ToShortDateString();
                }

                T1Allotments = new List<InvoiceAllotmentVM>();

                if (invoice.InvoicePayments.Any(ip => ip.IsT1()))
                {
                    foreach (var a in invoice.InvoicePayments.Where(ip => ip.IsT1()))
                    {
                        T1Allotments.Add(new InvoiceAllotmentVM(a));
                    }
                }

                T2Allotments = new List<InvoiceAllotmentVM>();
                if (invoice.InvoicePayments.Any(ip => ip.IsT2()))
                {
                    foreach (var a in invoice.InvoicePayments.Where(ip => ip.IsT2()))
                    {
                        T2Allotments.Add(new InvoiceAllotmentVM(a));
                    }
                }


            }

            public class InvoiceAllotmentVM
            {
                public string Func { get; set; }
                public string FAP { get; set; }
                public int FundingPriority { get; set; }
                public double AllottedAmt { get; set; }
                public decimal? AmountToPay { get; set; }

                public InvoiceAllotmentVM() { }

                public InvoiceAllotmentVM(InvoicePayment ip)
                {
                    Func = ip.Func;
                    FAP = ip.FAP;
                    AmountToPay = ip.Amount;
                }

                public InvoicePayment ToInvoicePayment()
                {
                    if (AmountToPay == null)
                    {
                        throw new Exception("payment details must have amount to convert to model object");
                    }
                    var ip = new InvoicePayment()
                    {
                        Func = Func,
                        FAP = FAP,
                        Amount = AmountToPay.Value,
                        ObjectCode = "281"
                    };

                    return ip;
                }
            }

            public Invoice PopulateInvoice(Invoice i, Contract c)
            {
                i.ContractCode = ContractCode;
                i.InvoiceDate = DateTime.Now;
                i.BeginDate = BeginDate;
                i.EndDate = EndDate;
                i.PrimeInvoice = true;

                i.T1FixedFee = T1FixedFee;
                i.T2FixedFee = T2FixedFee;

                i.T1Svcs = T1Allotments == null ? 0 : T1Allotments.Select(a => a.AmountToPay ?? 0).Sum();
                i.T2Svcs = T2Allotments == null ? 0 : T2Allotments.Select(a => a.AmountToPay ?? 0).Sum();

                i.T1FixedFeeMax = c.T1FixedFeeMax;
                i.T2FixedFeeMax = c.T2FixedFeeMax;
                i.ContractCeiling = c.ContractCeiling;
                i.T1SvcsCeiling = c.T1SvcsCeiling;
                i.T2SvcsCeiling = c.T2SvcsCeiling;
                i.FCCM = c.FCCM;
                i.HomeOfficeOverheadRateMax = c.HomeOfficeOverheadRateMax;
                i.FieldServiceOverheadRateMax = c.FieldServiceOverheadRateMax;
                i.Multiplier = c.Multiplier;


                return i;
            }

        }

        public ActionResult Details(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                ViewBag.WriteAccess = false;
                ViewBag.Role = CurrentUser.Role;
                //TODO Add all includes for when invoices get done.
                var inv = context.Invoices
                    .Include(i => i.InvoicePayments)
                    .Include(i => i.InvoiceApproval)
                    .Include(i => i.Contract)
                    .Include(i => i.Contract.SalaryRates)
                    .Include(i => i.SubConsultant)
                    .Include(i => i.Contract.Consultant)

                    .SingleOrDefault(i => i.InvoiceId == id);

                if (inv == null)
                    return HttpNotFound();

                //var state = InvoiceState.Submitted;
                //var state = InvoiceState.Created;
                var state = InvoiceState.Review;

                var status = inv.InvoiceApproval.Where(p => p.InvoiceId == id && p.ActiveStatus == true).Select(s => s.Status).FirstOrDefault();

                //if (status == InvoiceStatus.InvoiceCreated) { state = InvoiceState.Submitted; }
                //if (status == InvoiceStatus.InvoiceCreated) { state = InvoiceState.Created; }
                if (status == InvoiceStatus.InvoiceCreated) 
                { 
                    state = InvoiceState.Review;
                    if (Roles.CanSubmit.Split(',').Contains(CurrentUser.Role))
                        ViewBag.WriteAccess = true;
                    else
                        ViewBag.WriteAccess = false;

                }

                if (status == InvoiceStatus.InvoiceSubmitted)
                { 
                    state = InvoiceState.Submitted;
                    if (Roles.CanRecommend.Split(',').Contains(CurrentUser.Role))
                        ViewBag.WriteAccess = true;
                    else
                        ViewBag.WriteAccess = false;

                }

                if (status == InvoiceStatus.InvoiceRecommended) 
                { 
                    state = InvoiceState.Recommended;
                    if (Roles.CanApprove.Split(',').Contains(CurrentUser.Role))
                        ViewBag.WriteAccess = true;
                    else
                        ViewBag.WriteAccess = false;

                }

                if (status == InvoiceStatus.InvoiceApproval) { state = InvoiceState.Approved; }

                if (status == InvoiceStatus.InvoiceWaiting) { state = InvoiceState.waitingtobePaid; }

                if (status == InvoiceStatus.InvoicePaid) { state = InvoiceState.Paid; }

                if (status == InvoiceStatus.InvoiceRejected) { state = InvoiceState.Rejected; }

                ViewBag.state = state;

                return View(inv);
            }
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

        public ActionResult uploadfile(int contractCode, string invoiceNo, string filen)
        {
            string JsonList2;
            string JsonList;
            using (var context = new ConsultantContractsEntities())
            {
                // http://devsp2010srv/divisions/ConsultantContracts/default.aspx
                var siteUrl = "http://devsp2010srv/divisions/ConsultantContracts";
                var webName = "http://devsp2010srv/divisions/ConsultantContracts";
                var libraryName = "Invoices";
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

                        if (filefinal != null)
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
                        item["invoiceNo"] = invoiceNo;
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
                var libraryName = "Invoices";
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


        #endregion
        
    }
}
