using System;
using System.Collections.Generic;
//using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;
using Microsoft.Ajax.Utilities;
using ConsultantContractsInternal.Utilities;

namespace ConsultantContractsInternal.Controllers
{
	public class AllotmentsLookupController : Controller
	{
		//
		// GET: /AllotmentsLookup/

		public ActionResult JobSearch(String term, String jobNo)
		{
			if (term.Length < 2)
			{
				return null;
			}

			using (var context = new ProgCon_AllotmentsEntities())
			{
				var results = context.vw_ActiveJobAllotments
					.Where(a => a.ConstructionJobNumber.StartsWith(term))
					.Select(a => new {JobNo = a.ConstructionJobNumber, a.JobName})
					.Distinct()
					.Take(20)
					.ToList()
					.Select(j => new
					{
						j.JobNo,
						j.JobName,
						label = String.Format("{0}-{1}", j.JobNo, j.JobName)
					})
					.ToList();

				return Json(results, JsonRequestBehavior.AllowGet);
			}

		}

		public ActionResult AllotmentsList(String jobNo, String[] workTypes)
		{
			using (var context = new ProgCon_AllotmentsEntities())
			{

				string[] invalidFuncions = {"3610",
											"3611",
											"3612",
											"3710",
											"3711",
											"3712",
											"3810",
											"3811",
											"3812"};

				var t1q = context.vw_ActiveJobAllotments
					.Where(a => a.ConstructionJobNumber == jobNo && a.LastAmount > 0)
					.Where(a => (a.FunctionNumber.CompareTo("3010") >= 0 && a.FunctionNumber.CompareTo("3200") < 0) ||
						(a.FunctionNumber.CompareTo("3400") >= 0 && a.FunctionNumber.CompareTo("3499") <= 0) ||
						(a.FunctionNumber.CompareTo("3900") >= 0 && a.FunctionNumber.CompareTo("3990") < 0) ||
				   invalidFuncions.Contains(a.FunctionNumber)).ToList();

				var t2q = context.vw_ActiveJobAllotments
					.Where(a => a.ConstructionJobNumber == jobNo && a.LastAmount > 0)
					.Where(a => (a.FunctionNumber.CompareTo("3202") >= 0 && a.FunctionNumber.CompareTo("3400") < 0)
								|| (a.FunctionNumber.CompareTo("3499") > 0 && a.FunctionNumber.CompareTo("3900") < 0)
								&& (!invalidFuncions.Contains(a.FunctionNumber))).ToList();

				if (workTypes != null)
				{
					t1q = WorkTypeFilter(t1q, workTypes);
					if (t1q != null && t1q.Count() > 1)
					{ 
						//t1q = t1q.GroupBy(p => p.FederalAidProjectNumber + p.FunctionNumber).Select( group => group.First()).ToList(); 
						t1q = ListUtil.Distinct(t1q, (p, p1) => p.FederalAidProjectNumber == p1.FederalAidProjectNumber && p.FunctionNumber == p1.FunctionNumber).ToList();
					}
					t2q = WorkTypeFilter(t2q, workTypes);
					if (t2q != null && t2q.Count() > 1)
					{
						//t2q = t2q.GroupBy(p => p.FederalAidProjectNumber + p.FunctionNumber).Select(group => group.First()).ToList();
						t2q = ListUtil.Distinct(t2q, (p, p1) => p.FederalAidProjectNumber.Equals(p1.FederalAidProjectNumber) && p.FunctionNumber.Equals(p1.FunctionNumber)).ToList();
					}
				}
				
				return Json(new {t1 = t1q.ToList(), t2 = t2q.ToList()}, JsonRequestBehavior.AllowGet);
			}
		}

		private List<vw_ActiveJobAllotments> WorkTypeFilter(List<vw_ActiveJobAllotments> q, string[] workTypes)
		{
			var temp = new List<vw_ActiveJobAllotments>();

			if (workTypes.Any(t => new[] {"Design", "Environmental", "Planning"}.Contains(t)))
			{
				temp.AddRange(q.Where(a => a.FunctionNumber.StartsWith("301") || a.FunctionNumber.StartsWith("321")));
			}

			if (workTypes.Any(t => t == "Construction"))
			{
				temp.AddRange(q.Where(a => a.FunctionNumber.StartsWith("321") || a.FunctionNumber.StartsWith("322") || a.FunctionNumber.StartsWith("323")));
			}
			if (workTypes.Any(t => t == "Survey"))
			{
				temp.AddRange(q.Where(a => a.FunctionNumber.StartsWith("361") || a.FunctionNumber.StartsWith("371") || a.FunctionNumber.StartsWith("381")));
			}
			if (workTypes.Any(t => t == "Other"))
			{
				temp.AddRange(q.Where(a => a.FunctionNumber.StartsWith("340")));
			}
			if (workTypes.Any(t => t == "ROW"))
			{
				temp.AddRange(q.Where(a => a.FunctionNumber.StartsWith("316")));
			}
			return temp;
		}

		public ActionResult GetJobName(string jobNo)
		{
			using (var context = new ProgCon_AllotmentsEntities())
			{
				var job = context.Allotments.SingleOrDefault(j => j.ConstructionJobNumber == jobNo);

				if(job == null) return new HttpNotFoundResult(String.Format("Job No {0} not found", jobNo));

				return Json(job.JobName, JsonRequestBehavior.AllowGet);
			}
		}
	}
}
