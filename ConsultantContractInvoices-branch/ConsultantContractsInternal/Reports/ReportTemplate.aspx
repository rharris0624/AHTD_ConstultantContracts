<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Reports/ReportTemplate.aspx.cs" Inherits="ConsultantContractsInternal.Reports.ReportTemplate" EnableViewState="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <rsweb:ReportViewer ID="rvSiteMapping" runat="server" ShowPrintButton="false" Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false" >
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
