using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ConsultantContractsInternal.Helpers
{
    public class CommonHelpers : GlobalCommonHelper
    {
        public int pageSize = 25;

    }

    public static class RazorViewToString
    {
        public static string RenderViewToString(this Controller controller, string viewName, object model)
        {
            using (var writer = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                controller.ViewData.Model = model;
                var viewCxt = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }
    }

    public static class SequenceFinder
    {
        public static int[] FindSequences(int[] input, int length)
        {
            int[] result = null;
            if (length > 0)
            {
                var inputMax = input.Length > 0 ? input.Max() + length : length;

                var g = Enumerable.Range(1, inputMax).GroupJoin(input, r => r, i => i, (c, d) => new { val = c, d }).Where(e => e.d.Count() == 0).Take(length);

                result = g.Select(a => a.val).ToArray();
            }

            return result;
        }
    }


    //Some Common functions. Only project-specific.
    public class ReturnArgs
    {
        public ReturnArgs()
        {
        }

        public int Status { get; set; }
        public string ViewString { get; set; }
    }
}