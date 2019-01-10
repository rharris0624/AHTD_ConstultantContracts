<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AHTD.EarnState.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Earning Statements</title>
	<link href="App_Themes/MainSkin/Header.css" rel="stylesheet" type="text/css" />
	<link href="App_Themes/MainSkin/MainStyle.css" rel="stylesheet" type="text/css" />
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
			
				<div id="Error">
					<h2>We're sorry, but an error as occurred.</h2>
					<span>Please try again and if the error persists contact Computer Services.</span>
					<br /><asp:Button ID="btnReturn" runat="server" Text="Earning Statement Home" OnClick="btnReturn_Click" />
				</div>
			
    
    </div>
    </form>
</body>
</html>
