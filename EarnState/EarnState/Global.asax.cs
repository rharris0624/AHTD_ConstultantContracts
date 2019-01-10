using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AHTD.EarnState
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start( object sender, EventArgs e )
		{

		}

		protected void Application_End( object sender, EventArgs e )
		{

		}

		void Application_Error( object sender, EventArgs e )
		{
			Exception ex =
			HttpContext.Current.Server.GetLastError( );
			if ( ex != null )
			{
				AHTD.ErrorHandling.ExceptionHandler.DBConnection = Entities.CommonFunctions.GetConnectionString;
				AHTD.ErrorHandling.ExceptionHandler.Publish( ex.InnerException, "PAHREarnState" );
				
			}
		}


	}
}