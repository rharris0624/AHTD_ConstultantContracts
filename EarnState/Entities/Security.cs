using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Text;

using DAL;
using Microsoft.Practices.EnterpriseLibrary.Security;

namespace AHTD.Entities
{
	[Serializable]
	public class UserSecurity
	{
		string con = CommonFunctions.GetConnectionString;

		#region Member Variables
		string _userId;
		string _priority;
		DateTime _payPeriodStartDate;
		List<Entities.BudgetItem> _nonEngineeringBudgetIds = new List<BudgetItem>( );
		List<Entities.BudgetItem> _engineerBudgetIds = new List<BudgetItem>( );
		bool _viewAll;
		bool _grantAccess;
		#endregion

		#region Public Property
		public string UserId
		{
			get
			{
				return _userId;
			}
			set
			{
				_userId = value;
			}
		}

		public string Priority
		{
			get
			{
				return _priority;
			}
			set
			{
				_priority = value;
			}
		}

		public DateTime PayPeriodStartDate
		{
			get
			{
				return _payPeriodStartDate;
			}
		}

		public List<Entities.BudgetItem> NonEngineeringBudgetIds
		{
			get
			{
				return _nonEngineeringBudgetIds;
			}
		}

		public List<Entities.BudgetItem> EngineeringBudgetIds
		{
			get
			{
				return _engineerBudgetIds;
			}
		}

		public bool ViewAll
		{
			get
			{
				return _viewAll;
			}
		}

		public bool GrantAccess
		{
			get
			{
				return _grantAccess;
			}
		}
		#endregion

		#region Constructors
		public UserSecurity( )
		{

		}

		public UserSecurity( string userId )
		{
			
			_userId = userId;
			CheckUserAccess( );
			if ( _grantAccess )
			{
				GetPriorityLevel( );
				SetStartDate( );
			}
		}

		public UserSecurity( IPrincipal userId )
		{
			bool authorized = false;
			_userId = SplitUserName( userId.Identity.Name);
			authorized = GetDefaultRuleProvider().Authorize( userId,Convert.ToBoolean( System.Configuration.ConfigurationManager.AppSettings["ProdMode"]) ? "IsAuthorized_Prod": "IsAuthorized_Test");
			if ( authorized )
			{
				CheckUserAccess( );
				if ( _grantAccess )
				{
					GetPriorityLevel( );
					SetStartDate( );
				}

			}
			else
			{
				_grantAccess = false;
			}
		}

		private string SplitUserName( string userId )
		{
			//The domain is stripped out in order to match up with the security tables
			string[ ] userInfo = userId.Split( new char[ ] { '\\' } );
			string user = userInfo[ 1 ];
			return user;
		}


		#endregion

		#region Get Priority Level
		
		/// <summary>
		/// Sets priority level based on security table
		/// </summary>
		public void GetPriorityLevel( )
		{
			_engineerBudgetIds.Clear( );
			_nonEngineeringBudgetIds.Clear( );

			using ( DAL.DAL DbAccess = new DAL.DAL( con ) )
			{
				int tries = 5;

				while ( tries > 0 )
				{
					DataTable dt;
					try
					{
						ArrayList parms = new ArrayList( );

						parms.Add( _userId );
						dt = DbAccess.getTable( "ERS.GetPriorityLevel", parms );

						if ( dt.Rows.Count > 0 )
						{
							_priority = dt.Rows[ 0 ][ "Priority" ].ToString( );

							foreach ( DataRow row in dt.Rows )
							{
								if ( row[ "Budget" ].ToString( ) == "ALL" )
								{
									_viewAll = true;
								}
								if ( row[ "Budget" ].ToString( ) == "490" )
								{
									BudgetItem bi = new BudgetItem( row[ "Budget" ].ToString( ), row[ "Functional_Area" ].ToString( ) );
									_engineerBudgetIds.Add( bi );
								}
								else
								{
									BudgetItem bi = new BudgetItem( row[ "Budget" ].ToString( ), null );
									_nonEngineeringBudgetIds.Add( bi );
								}
							}
						}
						tries = -1;
					}
					catch ( SqlException ex )
					{
						tries--;
						if ( tries == 0 )
						{
							throw;
						}
					}
					catch ( Exception ex )
					{
						tries--;
						if ( tries == 0 )
						{
							throw;
						}
					}
				}
			}
		}
		#endregion

		#region Set Start Date
		
		/// <summary>
		/// The user's priority determines the date range that can be viewed.
		/// A user can view either 6 months worth of records or all the records
		/// </summary>
		public void SetStartDate( )
		{
			if ( !string.IsNullOrEmpty( _priority ) )
			{
				switch ( _priority )
				{
					case ( "CA" ):
					case ( "IA" ):
					case ( "PA" ):
					case ( "PS" ):
						_payPeriodStartDate = new DateTime( 1900, 1, 1 );
						break;
					default:
						_payPeriodStartDate = DateTime.Today.AddMonths( -6 );
						break;
				}
			}
			else
			{
				_payPeriodStartDate = new DateTime( 1900, 1, 1 );
			}
		}
		#endregion

		/// <summary>
		/// Checks to see if the user id has access.
		/// </summary>
		public void CheckUserAccess()
		{
			using ( DAL.DAL dbAcess = new DAL.DAL( CommonFunctions.GetConnectionString ) )
			{
				int tries = 5;
				while ( tries > 0 )
				{
					try
					{
						ArrayList parms = new ArrayList( );
						parms.Add( _userId );
						int count = (int) dbAcess.GetSingleValue( "ERS.CheckUserAccess", parms );
						if ( count > 0 )
						{
							_grantAccess = true;
						}
						else
						{
							_grantAccess = false;
						}
						tries = -1;
					}
					catch ( SqlException ex )
					{
						tries--;
						if ( tries == 0 )
						{
							throw;
						}
					}
					catch ( Exception ex )
					{
						tries--;
						if ( tries == 0 )
						{
							throw;
						}
					}
				}
			}
		}

		private static IAuthorizationProvider GetDefaultRuleProvider( )
		{
			return AuthorizationFactory.GetAuthorizationProvider( );

		}

	}
}
