<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ConsultantContractsInternal.Views.Report.Report"  EnableViewState="true" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowPrintButton="false" Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false" >
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
