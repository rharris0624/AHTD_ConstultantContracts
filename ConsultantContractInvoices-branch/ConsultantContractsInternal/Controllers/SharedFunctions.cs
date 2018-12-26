using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Controllers
{
    public class SharedFunctions
    {
        public static int PerPage = 50;

        //public static List<Models.County> PopulateCounties (bool AddDefault)
        //{
        //    List<Models.County> counties;
        //    using (var db = new Models.CitationsEntities())
        //{
        //    counties = new List<Models.County>();
        //    //var query = from c in db.Counties select c;
        //    var query = db.Counties;
        //    if (AddDefault)
        //    {
        //        Models.County county = new Models.County();
        //        county.County_Code = "";
        //        counties.Add(county);
        //    }
        //    foreach (Models.County temp in query)
        //    {
        //        counties.Add(temp);
        //    }
        //}
        //    return counties;
        //}

        public static string[][] PagerArray(int page, int perpage, int count, Controller cont, string action, string controller)
        {
            int NumberOnEachSide = 5;
            int NumOfPages = (count + perpage - 1) / perpage;
            int LowPage = (page > NumberOnEachSide ? page - NumberOnEachSide : 1);
            int HighPage = ((page + NumberOnEachSide) < NumOfPages ? page + NumberOnEachSide : NumOfPages);
            bool PreviousPage = page - 1 > 0;
            bool NextPage = page < NumOfPages;
            int NumberOfPagesOnPagers = (HighPage - LowPage + 1) + (PreviousPage ? 2 : 0) + (NextPage ? 2 : 0);
            string[][] pagerlist = new string[NumberOfPagesOnPagers][];
            for (int i = 0; i < pagerlist.Count(); i++)
            {
                pagerlist[i] = new string[2];
                if (i == 0 && PreviousPage)
                {
                    pagerlist[i][0] = cont.Url.Action(action, controller, new { page = 1 });
                    pagerlist[i][1] = "<< First Page";
                }
                else if (i == 1 && PreviousPage)
                {
                    pagerlist[i][0] = cont.Url.Action(action, controller, new { page = page - 1 });
                    pagerlist[i][1] = "< Previous Page";
                }
                else if (i == (pagerlist.Length - 2) && NextPage)
                {
                    pagerlist[i][0] = cont.Url.Action(action, controller, new { page = page + 1 });
                    pagerlist[i][1] = "Next Page >";
                }
                else if (i == (pagerlist.Length - 1) && NextPage)
                {
                    pagerlist[i][0] = cont.Url.Action(action, controller, new { page = NumOfPages });
                    pagerlist[i][1] = "Last Page >>";
                }
                else
                {
                    int pageNumber = (i) + LowPage;
                    if (PreviousPage)
                    {
                        pageNumber = (i - 2) + LowPage;
                    }
                    pagerlist[i][0] = cont.Url.Action(action, controller, new { page = pageNumber });
                    pagerlist[i][1] = pageNumber + "";
                }
            }
            return pagerlist;
        }
    }
}