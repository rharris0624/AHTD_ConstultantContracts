<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AHTD.Security.Web._Default" %>
<%@ OutputCache Location="None" %>

<%@ Register assembly="Microsoft.IdentityModel,  Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="Microsoft.IdentityModel.Web.Controls" tagprefix="idfx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>AHTD Security Token Service</title>
	<meta name="author" content="Sean A. Hanley" />
	<meta name="generator" content="Microsoft Visual Studio 2010 SP1" />
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta charset="UTF-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
	<link rel="stylesheet" href="styles/sts-screen.css" type="text/css" media="screen" />
	<script type="text/javascript" src="scripts/html5.js"></script>
</head>
<body>
	<header>
		<h1>AHTD Security Token Service</h1>
	</header>
	<section id="content">
		<h2>Hi there!</h2>
		<p>This site provides basic <abbr title="Security Token Service">STS</abbr> support
		via <abbr title="Windows Identity Foundation">WIF</abbr> for
		<abbr title="Arkansas Highway & Transportation Department">AHTD</abbr> applications.</p>
		<h3>How do I use this system?</h3>
		<p>For web apps, use the "Add STS Reference" menu option and choose an existing STS.
		For the URL of the existing STS, enter
		<script type="text/javascript">document.write(location.href)</script>
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
		<h3>Helper assemblies</h3>
		<p>Several helper assemblies for both Windows and Web projects are available for use.</p>
		<table>
			<thead>
				<tr>
					<th>Assembly</th>
					<th>Name</th>
					<th>Link</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>AHTD.Security.Web.dll</td>
					<td>Web Helper</td>
					<td><a href="http://tfs2010srv:8080/tfs/web/UI/Pages/Scc/Explorer.aspx?pguid=742e313e-182f-4aa2-ae90-557230a1e112#path=%24%2FApplicationDevelopment%2FAssemblies%2FAHTD%2F2.0">.NET 2.0</a> &#124; <a href="http://tfs2010srv:8080/tfs/web/UI/Pages/Scc/Explorer.aspx?pguid=742e313e-182f-4aa2-ae90-557230a1e112#path=%24%2FApplicationDevelopment%2FAssemblies%2FAHTD%2F4.0">.NET 4.0</a></td>
				</tr>
				<tr>
					<td>AHTD.Security.Client.dll</td>
					<td>Client Helper</td>
					<td><a href="http://tfs2010srv:8080/tfs/web/UI/Pages/Scc/Explorer.aspx?pguid=742e313e-182f-4aa2-ae90-557230a1e112#path=%24%2FApplicationDevelopment%2FAssemblies%2FAHTD%2F3.5">.NET 3.5</a> &#124; <a href="http://tfs2010srv:8080/tfs/web/UI/Pages/Scc/Explorer.aspx?pguid=742e313e-182f-4aa2-ae90-557230a1e112#path=%24%2FApplicationDevelopment%2FAssemblies%2FAHTD%2F4.0">.NET 4.0</a></td>
				</tr>
			</tbody>
		</table>
	</section>
	<footer>
		<p>&copy; 2012 AHTD. All rights reserved.</p>
	</footer>
	<script type="text/javascript">
		function showXMLCode()
		{
			window.open('help/appconfig.htm', '', 'height=600,width=800,location=no,menubar=no,status=no,toolbar=no,scrollbars=yes');
		}
	</script>
</body>
</html>
