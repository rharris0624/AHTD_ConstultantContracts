<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AHTD.Security.Web.m._Default" %>
<%@ OutputCache Location="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>AHTD Security Token Service</title>
	<meta name="author" content="Sean A. Hanley" />
	<meta name="generator" content="Microsoft Visual Studio 2010 SP1" />
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta charset="UTF-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
	<link rel="stylesheet" href="../styles/sts-mobile.css" type="text/css" media="screen, handheld" />
	<script type="text/javascript" src="../scripts/html5.js"></script>
</head>
<body>
	<header>
		<h1>AHTD</h1>
		<h2>Security Token Service</h2>
	</header>
	<section id="content">
		<h3>Hi there!</h3>
		<p>This site provides basic <abbr title="Security Token Service">STS</abbr> support
		via <abbr title="Windows Identity Foundation">WIF</abbr> for
		<abbr title="Arkansas Highway & Transportation Department">AHTD</abbr> applications.</p>
		<h3>How do I use this system?</h3>
		<p>For web apps, use the "Add STS Reference" menu option and choose an existing STS.
		For the URL of the existing STS, enter
		<script type="text/javascript">			document.write(location.href)</script>
		and uncheck HTTPS secure sockets layer. Don't see this option when you right-click on
		your web project in Visual Studio? Be sure to download and install 
		<a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=EB9C345F-E830-40B8-A5FE-AE7A864C4D76" target="_blank">WIF</a>
		and the <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=c148b2df-c7af-46bb-9162-2c9422208504&displaylang=en" target="_blank">SDK</a>!</p>
		<p>For Windows apps, you will need to utilize the WCF
		<a href="Services/ClaimsService.svc">Claims Service</a>. You can add a reference to this
		service manually in your application project, or you may use the provided AHTD Security Client Helper
		assembly (see below). In either case, you will need to make sure to add the
		following to your App.config file: <small><a href="javascript:showXMLCode()">Show XML Code</a></small></p>
		<p>Be sure  to change the value for the ClaimsService_AppName appSettings key to the
		name of your application as defined in the AHTD Security database.</p>
	</section>
	<footer>
		<p>&copy; 2012 AHTD. All rights reserved.</p>
	</footer>
	<script type="text/javascript">
		function showXMLCode()
		{
			window.open('../help/appconfig.htm', '', 'height=600,width=800,location=no,menubar=no,status=no,toolbar=no,scrollbars=yes');
		}
	</script>
</body>
</html>
