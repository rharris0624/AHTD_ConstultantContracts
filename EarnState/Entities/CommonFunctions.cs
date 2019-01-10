using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace AHTD.Entities
{
	public static class CommonFunctions
	{
		/// <summary>
		/// Gets the connection string from the web config file based on the server
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)" )]
		public static string GetConnectionString
		{
			get{
			string con = string.Empty;
			if( Convert.ToBoolean( System.Configuration.ConfigurationManager.AppSettings["ProdMode"].ToString() ) )
			{
				con = System.Configuration.ConfigurationManager.ConnectionStrings["Prod"].ToString();
			}
			else
			{			
				con = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ToString();
			}
			return con;
			}
		}

		/// <summary>
		/// Gets the security level the user has access to
		/// </summary>
		/// <param name="identity">the Windows login id</param>
		/// <returns></returns>
		public static UserSecurity GetUserSecurity( string identity )
		{
			if ( !string.IsNullOrEmpty( identity ) )
			{
				//The domain is stripped out in order to match up with the security tables
				string[ ] userInfo = identity.Split( new char[ ] { '\\' } );
				string user = userInfo[ 1 ];
				UserSecurity sec = new UserSecurity( user );
				return sec;
			}
			else
			{
				throw new ArgumentNullException( "identity" );
			}
		}

		public static UserSecurity GetUserSecurity( IPrincipal identity )
		{
			UserSecurity sec = new UserSecurity( identity );
			return sec;

		}

		/// <summary>
		/// Search Earning Statements by SSN
		/// </summary>
		/// <param name="userSec">User Security Object</param>
		/// <param name="ssn">Social Security Number to search for</param>
		/// <returns>Dataset</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static System.Data.DataSet SearchBySSN( UserSecurity userSec, string ssn )
		{
			int tries = 5;
			DataSet ds = null;


			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}

			parms.Add( ssn );
			ds = LoadDataset( "ERS.GetEarnStateBySSN", parms );

			return ds;
		}

		/// <summary>
		/// Searches for Earning statements by Employee Number
		/// </summary>
		/// <param name="userSec">User Security Object</param>
		/// <param name="empNo">Employee Number to search for</param>
		/// <returns>Dataset</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static System.Data.DataSet SearchByEmpNo( UserSecurity userSec, string empNo )
		{

			DataSet ds = null;


			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}
			parms.Add( empNo );
			ds = LoadDataset( "ERS.GetEarnStateByEmpId", parms );


			return ds;
		}

		/// <summary>
		/// Gets the number of unique employee record by Budget number
		/// </summary>
		/// <param name="userSec">User Security object</param>
		/// <param name="budget">Budget number to search for</param>
		/// <returns>Int32</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static Int32 GetHeaderBudgetSearchCount( UserSecurity userSec, string criteria )
		{
			int reccount = 0;


			DataSet ds = null;

			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}
			parms.Add( criteria );
			ds = LoadDataset( "ERS.GetEarnStateHeaderByBudget", parms );
			reccount = ds.Tables[ 0 ].Rows.Count;
		

			return reccount;
		}

	
		/// <summary>
		/// Gets the unique employeees by budget number to be 
		/// used as header records
		/// </summary>
		/// <param name="userSec">User Security Object</param>
		/// <param name="budget">Budget number to search for</param>
		/// <returns>Dataset</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static System.Data.DataSet GetHeaderByBudgetSearch( UserSecurity userSec, string criteria )
		{

			DataSet ds = null;


			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}
			parms.Add( criteria );
			ds = LoadDataset( "ERS.GetEarnStateHeaderByBudget", parms );


			return ds;
		}

		/// <summary>
		/// Gets the detail records for each header record based on Employee number
		/// </summary>
		/// <param name="userSec">User Security object</param>
		/// <param name="ssn"></param>
		/// <returns>Dataset</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static System.Data.DataSet GetMultiSearchDetail( UserSecurity userSec, string criteria )
		{

			DataSet ds = null;


			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}
			if ( !String.IsNullOrEmpty( criteria ) )
			{
				parms.Add( criteria );
			}
			else
			{
				parms.Add( DBNull.Value );
			}
			ds = LoadDataset( "ERS.GetEarnStateByEmpId", parms );
			
			return ds;
		}

		/// <summary>
		/// Gets Unique employees by Engineering number to be 
		/// used as header record.
		/// </summary>
		/// <param name="userSec"></param>
		/// <param name="budget"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex" )]
		public static System.Data.DataSet GetHeaderByEngineerSearch( UserSecurity userSec, string criteria )
		{
			DataSet ds = null;

			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}

			parms.Add( criteria );
			ds = LoadDataset( "ERS.GetEarnStateHeaderByEngNo", parms );
			
			return ds;
		}

		public static DataSet GetHeaderByName( UserSecurity userSec, string criteria )
		{
			DataSet ds = null;
			
			ArrayList parms = new ArrayList( );
			parms.Add( userSec.UserId );
			parms.Add( userSec.PayPeriodStartDate );
			if ( userSec.ViewAll )
			{
				parms.Add( "ALL" );
			}
			else
			{
				if ( userSec.NonEngineeringBudgetIds.Count > 0 )
				{
					parms.Add( ( ( BudgetItem ) userSec.NonEngineeringBudgetIds[ 0 ] ).Budget );
				}
				else
				{
					parms.Add( ( ( BudgetItem ) userSec.EngineeringBudgetIds[ 0 ] ).Budget );
				}
			}

			parms.Add( criteria );
			ds = LoadDataset( "ERS.GetEarnStateHeadersByName", parms );

			return ds;
		}

		private static DataSet LoadDataset( string procName, ArrayList parms )
		{
			DataSet ds = null;
			int tries = 5;
			
			using ( DAL.DAL dbAccess = new DAL.DAL( GetConnectionString ) )
			{
				while ( tries > 0 )
				{
					try
					{
						ds = dbAccess.getDataSet( procName, parms );
						tries = -1;
					}
					catch ( SqlException ex )
					{
						tries--;
						if ( tries == 0 )
						{
							ErrorHandling.ExceptionHandler.Publish( ex.InnerException, "PAHR Earning Statement" );
						}
					}
					catch ( Exception ex )
					{
						tries--;
						if ( tries == 0 )
						{
							ErrorHandling.ExceptionHandler.Publish( ex.InnerException, "PAHR Earning Statement" );
						}
					}
				}
			}

			return ds;
		}


		/// <summary>
		/// Checks to see if string contains only numbers
		/// </summary>
		/// <param name="number">string</param>
		/// <returns>bool</returns>
		public static bool IsNumeric( string number )
		{
			bool result;
			int output;
			result = Int32.TryParse( number, out output );
			return result;
		}


	}
}
