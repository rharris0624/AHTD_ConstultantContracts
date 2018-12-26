using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Models
{
    public class ReportInfo
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ReportUrl { get; set; }
        public string Folder { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ReportSummary { get; set; }
        public Dictionary<string, object> Parameters;
    }
}