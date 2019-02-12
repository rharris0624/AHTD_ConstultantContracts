using ConsultantContractsInternal.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsultantContractsInternal.Reports
{
    public partial class WebForm1 : ViewPage<ReportInfo>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    
                    //ReportInfo rptInfo = ViewBag.ReportInfo as ReportInfo;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    //ReportViewer1.Height = Unit.Pixel(rptInfo.Height - 58);
                    //ReportViewer1.Width = Unit.Percentage(rptInfo.Width);
                    //ReportViewer1.ServerReport.ReportServerUrl = new Uri(rptInfo.ReportUrl); // Add the Reporting Server URL  
                    //ReportViewer1.ServerReport.ReportPath = String.Format("/{0}/{1}", rptInfo.Folder, rptInfo.ReportName);
                    var parms = GetParameters();
                    var height = Request["Height"];
                    var userId2 = User.Identity.Name;
                    var userid = Regex.Replace(Thread.CurrentPrincipal.Identity.Name,@".*\\","");
                    ReportViewer1.Height = Unit.Pixel(Convert.ToInt32(height) - 100);
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerURL"]); // Add the Reporting Server URL  
                    ReportViewer1.ServerReport.ReportPath = String.Format("/{0}/{1}", Request["Folder"], Request["ReportName"]);
                    //var parms = GetParameters(rptInfo.Parameters);                    //ReportViewer1.AsyncRendering = false;
                    ReportViewer1.EnableViewState = true;
                    Page.EnableViewState = true;
                    ReportViewer1.ServerReport.SetParameters(parms);
                    ReportViewer1.ServerReport.Refresh();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Source + " - " + ex.Message);
                }
            }
        }

        private List<ReportParameter> GetParameters()
        {
            List<ReportParameter> rval = new List<ReportParameter>();
            if (Request["contractCode"] != null)
            {
                rval.Add(new ReportParameter("Contract", Request["contractCode"]));
            }
            if (Request["consultantId"] != null)
            {
                rval.Add(new ReportParameter("Consultant", Request["consultantId"]));
            }
            if (Request["fromDate"] != null)
            {
                rval.Add(new ReportParameter("StartDate", Request["fromDate"]));
            }
            if (Request["toDate"] != null)
            {
                rval.Add(new ReportParameter("EndDate", Request["toDate"]));
            }
            return rval;
        }
    }
}