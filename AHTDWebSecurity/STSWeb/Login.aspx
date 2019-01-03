<%@ Page Language="C#" AutoEventWireup="true" Inherits="AHTD.Security.Web.Login" Codebehind="Login.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>AHTD Security Token Service Login</title>
	<meta name="author" content="Sean A. Hanley" />
	<meta name="generator" content="Microsoft Visual Studio 2010 SP1" />
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta charset="UTF-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
	<link rel="stylesheet" href="styles/sts-screen.css" type="text/css" media="screen" />
	<script type="text/javascript" src="scripts/html5.js"></script>
	<style type="text/css">
		table { MARGIN: 0; }
		td { TEXT-ALIGN: left; }
	</style>
</head>
<body>
	<header>
		<h1>AHTD Security Token Service</h1>
	</header>
	<section id="content">
		<h2>Login</h2>
		<form id="form1" runat="server">
			<table>
				<tr>
					<td>
						<asp:Label ID="Label1" AssociatedControlID="txtUserName" runat="server" Text="User name"></asp:Label>
					</td>
					<td>
						<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Label2" AssociatedControlID="txtPassword" runat="server" Text="Password"></asp:Label>
					</td>
					<td>
						<asp:TextBox ID="txtPassword" runat="server" TextMode="Password">password</asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Button ID="btnSubmit" runat="server" Text="Login" style="FLOAT: right;" />
						<a href="#" style="FONT-SIZE: 0.75em;">Forgot your password?</a>
					</td>
				</tr>
			</table>
		</form>
		<p>Note: This is an example login page so no authentication actually occurs
		(i.e. typing any user name will work). The user name is required but the
		password is optional.</p>
	</section>
	<footer>
		<p>&copy; 2012 AHTD. All rights reserved.</p>
	</footer>
</body>
</html>
