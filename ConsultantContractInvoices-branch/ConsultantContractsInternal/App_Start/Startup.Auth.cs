using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ConsultantContractsInternal
{
    public class ConsultantContractsAuthentication
    {
        public const string ApplicationCookie = "ConsultantContractsAuthenticationType";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = ConsultantContractsAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "ConsultantContractsAuthCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(24),
            });
        }
    }
}