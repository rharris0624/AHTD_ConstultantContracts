<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Reports/ReportPage.aspx.cs" Inherits="ConsultantContractsInternal.Reports.WebForm1" EnableViewState="true" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>
            

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() != undefined) {
            args.set_errorHandled(true);
        }
    }
</script>
