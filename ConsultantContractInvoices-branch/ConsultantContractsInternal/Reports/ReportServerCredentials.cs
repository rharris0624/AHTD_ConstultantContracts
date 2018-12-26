using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Configuration;

[Serializable]
public sealed class ReportServerCredentials : IReportServerCredentials
{

    #region Private Properties
    private string _username;
    private string _password;
    private string _domain;
    #endregion Private Properties

    #region Public Properties
    public System.Security.Principal.WindowsIdentity ImpersonationUser
    {
        get { return null; }
    }

    public System.Net.ICredentials NetworkCredentials
    {
        get { return new NetworkCredential(_username, _password, _domain); }
    }
    #endregion Public Properties

    #region Constructor
    public ReportServerCredentials(string userName, string password, string domain)
    {
        _username = userName;
        _password = password;
        _domain = domain;
    }
    public ReportServerCredentials()
    {
        var appSetting = WebConfigurationManager.AppSettings;
        _username = appSetting["ReportServerUser"];
        _password = appSetting["ReportServerPassword"];
        _domain = appSetting["ReportServerDomain"];
    }
    #endregion Constructor

    #region Public Method
    public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
    {
        authCookie = null;
        userName = null;
        password = null;
        authority = null;
        return false;
    }
    #endregion Public Method


    public IEnumerable<Cookie> Cookies { get { return null; } }


    public IEnumerable<string> Headers { get { return null; } }

    public Uri ReportServerUrl { get { return new Uri(WebConfigurationManager.AppSettings["ReportServerUrl"]); } }


    public int Timeout { get { return 60000; } }
}