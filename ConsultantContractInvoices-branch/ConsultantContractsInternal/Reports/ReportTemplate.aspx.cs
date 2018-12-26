using ConsultantContractsInternal.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsultantContractsInternal.Reports
{
    public partial class ReportTemplate : ViewPage<ReportInfo>//System.Web.UI.Page//
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //ReportInfo rptInfo = ViewBag.ReportInfo as ReportInfo;
                    rvSiteMapping.ProcessingMode = ProcessingMode.Remote;
                    rvSiteMapping.Height = Unit.Pixel(600);//Unit.Pixel(rptInfo.Height - 58); //
                    rvSiteMapping.Width = Unit.Percentage(100);//Unit.Percentage(rptInfo.Width);//
                    rvSiteMapping.ServerReport.ReportServerUrl = new Uri(@"http://localhost/reportserver"); //new Uri(rptInfo.ReportUrl); //  Add the Reporting Server URL  
                    rvSiteMapping.ServerReport.ReportPath = String.Format("/{0}/{1}", Request["Folder"], Request["ReportName"]); //String.Format("/{0}/{1}", rptInfo.Folder, rptInfo.ReportName); //@"/AHTDConsultantContracts/UnpaidInvoices";//
                    var parms = GetParameters2();
                    //rvSiteMapping.AsyncRendering = false;
                    rvSiteMapping.EnableViewState = true;
                    Page.EnableViewState = true;
                    rvSiteMapping.ServerReport.SetParameters(parms);
                    rvSiteMapping.ServerReport.Refresh();
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
        private List<ReportParameter> GetParameters2()
        {
            List<ReportParameter> rval = new List<ReportParameter>();
            try
            {
                if (Request["Contract"] != null)
                {
                    rval.Add(new ReportParameter("ContractCode", Request["Contract"]));
                }
                if (Request["Consultant"] != null)
                {
                    rval.Add(new ReportParameter("ConsultantId", Request["Consultant"]));
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
