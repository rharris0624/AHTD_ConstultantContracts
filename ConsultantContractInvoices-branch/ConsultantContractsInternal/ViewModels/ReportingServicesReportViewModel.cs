using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ConsultantContractsInternal.ViewModels
{
    public class ReportingServicesReportViewModel
    {
        #region Constructor
        public ReportingServicesReportViewModel(String reportPath, List<ReportParameter> Parameters)
        {
            ReportPath = reportPath;
            parameters = Parameters;
        }
        public ReportingServicesReportViewModel()
        {
        }
        #endregion Constructor

        #region Public Properties
        //public ReportServerCredentials ServerCredentials { get { return new ReportServerCredentials(); } }
        public String ReportPath { get; set; }
        public Uri ReportServerURL { get { return new Uri(WebConfigurationManager.AppSettings["ReportServerUrl"]); } }
        public List<ReportParameter> parameters { get; set; }
        private string UploadDirectory = HttpContext.Current.Server.MapPath("~/App_Data/UploadTemp/");
        private string TempDirectory = HttpContext.Current.Server.MapPath("~/tempFiles/");
        #endregion
    }
}