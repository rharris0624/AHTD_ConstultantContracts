using ConsultantContractsInternal.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsultantContractsInternal.Views.Report
{
    public partial class TestWebForm : ViewPage<ReportInfo>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    ReportInfo rptInfo = ViewBag.ReportInfo as ReportInfo;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    ReportViewer1.Height = Unit.Pixel(rptInfo.Height - 58);
                    ReportViewer1.Width = Unit.Percentage(rptInfo.Width);
                    ReportViewer1.ServerReport.ReportServerUrl = new Uri(rptInfo.ReportUrl); // Add the Reporting Server URL  
                    ReportViewer1.ServerReport.ReportPath = String.Format("/{0}/{1}", rptInfo.Folder, rptInfo.ReportName);
                    var parms = GetParameters(rptInfo.Parameters);
                    //ReportViewer1.AsyncRendering = false;
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

        private List<ReportParameter> GetParameters(Dictionary<string, object> parameters)
        {
            List<ReportParameter> rval = new List<ReportParameter>();
            try
            {
                foreach (var parameter in parameters)
                {
                    var itemValue = parameter.Value;
                    if (parameter.Value is int)
                        rval.Add(new ReportParameter(parameter.Key, ((int)parameter.Value).ToString()));
                    else if (parameter.Value is string)
                        rval.Add(new ReportParameter(parameter.Key, (string)parameter.Value));
                    else if (parameter.Value is DateTime)
                        rval.Add(new ReportParameter(parameter.Key, ((DateTime)parameter.Value).ToString(@"MM\dd\yyyy")));
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rval;
        }
    }
}