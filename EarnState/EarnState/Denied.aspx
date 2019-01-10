<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Denied.aspx.cs" Inherits="AHTD.EarnState.Denied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Earning Statements</title>
    <link href="App_Themes/MainSkin/MainStyle.css" rel="stylesheet" type="text/css" />
	<link href="App_Themes/MainSkin/Header.css" rel="stylesheet" type="text/css" />
	<link href="App_Themes/MainSkin/ViewStatement.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div id="MainDiv" style="text-align:center">
		<div class="header">
				<div class="Outer">
					<div class="Inner">
						<div class="Content">
							<h3>PAHR Employee Earnings Statement</h3>
						</div>
					</div>
				</div>
			</div>
		
		<div class="denied">
			 <p>You do not have permission to view this page!</p>
			 <span>You have attempted to access this site with the user id:</span>
			 <asp:Literal ID="error" runat="server"></asp:Literal> 
		</div>
    </div>
    </form>
</body>
</html>
