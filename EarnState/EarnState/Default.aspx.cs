using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AHTD.EarnState
{
	public partial class _Default : System.Web.UI.Page
	{
		Entities.UserSecurity UserSec;
		DataSet dsIndividualResult;

		EarnState.Controls.PagerControl MultiSearchPagerControl;
		EarnState.Controls.PagerControl NameSearchPagerControl;

		
		#region Page Load
		protected void Page_Load( object sender, EventArgs e )
		{

			if ( !IsPostBack )
			{
				if ( !string.IsNullOrEmpty( this.Page.User.Identity.Name.ToString( ) ) )
				{
					UserSec = GetUserSecurity( );
					if ( !UserSec.GrantAccess )
					{
						Response.Redirect( "Denied.aspx" );
					}
					Session[ "UserSec" ] = UserSec;
				}
				else
				{
					Response.Redirect( "Denied.aspx" );
				}

				System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly( );

				System.IO.FileInfo fi = new System.IO.FileInfo( assem.Location );
				DateTime dt = fi.LastWriteTime;
				lblUpdated.Text = string.Format( "{0:f}", dt );
				lblPath.Text = Page.Request.Path;

				txtEmpNo.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearchEmpNo.ID + ")" );
				txtSSN.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearchSSN.ID + ")" );
				txtBudget.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearchBudget.ID + ")" );
				txtConstr.Attributes.Add( "onKeyDOwn", "TrapKeyDown(" + btnSearchConstr.ID + ")" );
				txtName.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearchName.ID + ")" );
				TxtQuickNameSearch.Attributes.Add( "onKeyDown", "TrapKeyDown(" + BtnQuickNameSearch.ID + ")" );
				txtSearchCritBudget.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearchBudgetFilter.ID + ")" );
				txtSearchCriteria.Attributes.Add( "onKeyDown", "TrapKeyDown(" + btnSearch.ID + ")" );

			}
			else
			{

				if ( Session[ "UserSec" ] != null )
				{
					UserSec = ( Entities.UserSecurity ) Session[ "UserSec" ];
				}

				if ( ViewState[ "CurrentSearch" ] != null )
				{
					if ( ViewState[ "CurrentSearch" ].ToString( ) == "EmpNo" || ViewState[ "CurrentSearch" ].ToString( ) == "SSN" )
					{
						dsIndividualResult = ( DataSet ) ViewState[ "IndSearchResults" ];
						gridViewSingleResults.DataSource = dsIndividualResult;
					}
				}


			}

			GridViewRow MultiSearchRow = gridHeaderResults.BottomPagerRow;
			if ( MultiSearchRow != null )
			{
				MultiSearchPagerControl = ( EarnState.Controls.PagerControl ) MultiSearchRow.FindControl( "PagerControl1" );
				MultiSearchPagerControl.NextButtonClicked += new EventHandler<EventArgs>( MultiSearchPagerControl_NextButtonClicked );
				MultiSearchPagerControl.PrevButtonClicked += new EventHandler<EventArgs>( MultiSearchPagerControl_PrevButtonClicked );
				MultiSearchPagerControl.FirstButtonClicked += new EventHandler<EventArgs>( MultiSearchPagerControl_FirstButtonClicked );
				MultiSearchPagerControl.LastButtonClicked += new EventHandler<EventArgs>( MultiSearchPagerControl_LastButtonClicked );
				MultiSearchPagerControl.PageSizeChanged += new EventHandler<EventArgs>( MultiSearchPagerControl_PageSizeChanged );
			}

			GridViewRow NameSearchRow = GridNameHeader.BottomPagerRow;
			if( NameSearchRow != null)
			{
				NameSearchPagerControl = ( EarnState.Controls.PagerControl ) NameSearchRow.FindControl( "PagerControl2" );
				NameSearchPagerControl.FirstButtonClicked += new EventHandler<EventArgs>( NameSearchPagerControl_FirstButtonClicked );
				NameSearchPagerControl.PrevButtonClicked += new EventHandler<EventArgs>( NameSearchPagerControl_PrevButtonClicked );
				NameSearchPagerControl.NextButtonClicked += new EventHandler<EventArgs>( NameSearchPagerControl_NextButtonClicked );
				NameSearchPagerControl.LastButtonClicked += new EventHandler<EventArgs>( NameSearchPagerControl_LastButtonClicked );
				NameSearchPagerControl.PageSizeChanged += new EventHandler<EventArgs>( NameSearchPagerControl_PageSizeChanged );
			}
		}

		
		#endregion

		#region GridView Pager Control Events

		void NameSearchPagerControl_PageSizeChanged( object sender, EventArgs e )
		{
			int PageSize = 0;
			bool result = Int32.TryParse( ( string ) ( ( CommandEventArgs ) e ).CommandArgument, out PageSize );

			GridNameHeader.PageSize = PageSize;
			GridViewPageEventArgs args = new GridViewPageEventArgs( 0 );
			GridNameHeader_PageIndexChanging( ( GridView ) sender, args );
		}

		void NameSearchPagerControl_LastButtonClicked( object sender, EventArgs e )
		{
			GridNameHeader_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void NameSearchPagerControl_NextButtonClicked( object sender, EventArgs e )
		{
			GridNameHeader_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void NameSearchPagerControl_PrevButtonClicked( object sender, EventArgs e )
		{
			GridNameHeader_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void NameSearchPagerControl_FirstButtonClicked( object sender, EventArgs e )
		{
			GridNameHeader_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void MultiSearchPagerControl_PageSizeChanged( object sender, EventArgs e )
		{
			int PageSize = 0;
			bool result = Int32.TryParse( ( string ) ( ( CommandEventArgs ) e ).CommandArgument, out PageSize );

			gridHeaderResults.PageSize = PageSize;
			GridViewPageEventArgs args = new GridViewPageEventArgs( 0 );
			gridHeaderResults_PageIndexChanging( ( GridView ) sender, args );

		}

		void MultiSearchPagerControl_LastButtonClicked( object sender, EventArgs e )
		{
			gridHeaderResults_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void MultiSearchPagerControl_FirstButtonClicked( object sender, EventArgs e )
		{
			gridHeaderResults_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );
		}

		void MultiSearchPagerControl_PrevButtonClicked( object sender, EventArgs e )
		{
			gridHeaderResults_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );

		}

		void MultiSearchPagerControl_NextButtonClicked( object sender, EventArgs e )
		{
			gridHeaderResults_PageIndexChanging( ( GridView ) sender, ( GridViewPageEventArgs ) e );


		}

		#endregion

		#region Get User Security
		/// <summary>
		/// Get user access for the windows identity currently logged in
		/// </summary>
		/// <returns>UserSecurity object</returns>
		private AHTD.Entities.UserSecurity GetUserSecurity( )
		{
			//return Entities.CommonFunctions.GetUserSecurity( this.Page.User.Identity.Name.ToString( ) );
			return Entities.CommonFunctions.GetUserSecurity( this.Page.User );
		}
		#endregion

		#region Single Search Grid events
		
		protected void gridViewSingleResults_SelectedIndexChanged( object sender, EventArgs e )
		{
			int index = gridViewSingleResults.SelectedIndex;
			Entities.EarnStatement earning = new AHTD.Entities.EarnStatement( dsIndividualResult.Tables[ 0 ].Rows[ index ] );
			Session[ "EarnStatementObject" ] = earning;
			ScriptManager.RegisterClientScriptBlock( pnlMultiResults, pnlMultiResults.GetType( ), "openWindow", "window.open('EarnStateView.aspx','','width=700,scrollbars=1,menubar=1');", true );
		}

		#endregion

		#region Multi Search Grid events
		protected void gridHeaderResults_RowDataBound( object sender, GridViewRowEventArgs e )
		{
			if ( e.Row.RowType == DataControlRowType.DataRow )
			{
				DataRowView row = ( DataRowView ) e.Row.DataItem;
				ObjectDataSource ods = ( ObjectDataSource ) e.Row.FindControl( "ObjectDataSourceDetail" );
				ods.SelectParameters[ 1 ].DefaultValue = row[ "EmployeeNumber" ].ToString( );
			}
			else
			{
				
			}
		}

		protected void gridHeaderResults_PageIndexChanging( object sender, GridViewPageEventArgs e )
		{
			gridHeaderResults.PageIndex = e.NewPageIndex;
			if ( ViewState[ "CurrentSearch" ].ToString() == "Engineer" )
			{
				ObjDataSourceHeaderSearch.SelectMethod = "GetHeaderByEngineerSearch";
			}
			
			gridHeaderResults.DataSource = ObjDataSourceHeaderSearch;
			gridHeaderResults.DataBind( );
		}

		protected void lnkShowDetails_Click( object sender, EventArgs e )
		{
			LinkButton btn = ( LinkButton ) sender;
			GridViewRow gvr = ( GridViewRow ) btn.NamingContainer;
			Panel p1 = ( Panel ) gvr.FindControl( "pnlDetails" );
			LinkButton lbl = ( LinkButton ) gvr.FindControl( "lnkShowDetails" );

			if ( p1.Visible == false )
			{
				p1.Visible = true;
				lbl.Text = "-";
			}
			else if ( p1.Visible == true )
			{
				p1.Visible = false;
				lbl.Text = "+";
				ViewState[ "DetailDataSet" ] = null;
			}

			GridView gv = ( GridView ) btn.FindControl( "gridDetailView" );
			gv.DataBind( );
		}

		protected void ObjectDataSourceDetail_Selected( object sender, ObjectDataSourceStatusEventArgs e )
		{
			DataSet underlyingDataSet = ( DataSet ) e.ReturnValue;
			if ( underlyingDataSet != null )
			{
				ViewState[ "DetailDataSet" ] = underlyingDataSet;
			}

		}

		protected void gridDetailView_SelectedIndexChanged( object sender, EventArgs e )
		{
			int index = ( ( GridView ) sender ).SelectedIndex;
			if ( ViewState[ "DetailDataSet" ] != null )
			{
				DataSet ds = ( DataSet ) ViewState[ "DetailDataSet" ];
				Entities.EarnStatement earning = new AHTD.Entities.EarnStatement( ds.Tables[ 0 ].Rows[ index ] );
				Session[ "EarnStatementObject" ] = earning;
				//Server.Transfer( "EarnStateView.aspx" );
				//litMultiScript.Text = "<script type='text/javascript'>detailedresults=window.open('EarnStateView.aspx');</script>";
				//Context.Items[ "EarnStatementObject" ] = earning;
				ScriptManager.RegisterClientScriptBlock( pnlMultiResults, pnlMultiResults.GetType( ), "openWindow", "window.open('EarnStateView.aspx');", true );

			}

		}

		#endregion

		#region Name Search Grid Events
		protected void GridNameHeader_PageIndexChanging( object sender, GridViewPageEventArgs e )
		{
			GridNameHeader.PageIndex = e.NewPageIndex;
			GridNameHeader.DataSource = ObjectDataSourceName;
			GridNameHeader.DataBind( );
		}

		protected void GridNameHeader_RowDataBound( object sender, GridViewRowEventArgs e )
		{
			if ( e.Row.RowType == DataControlRowType.DataRow )
			{
				DataRowView row = ( DataRowView ) e.Row.DataItem;
				ObjectDataSource ods = ( ObjectDataSource ) e.Row.FindControl( "ObjectDataSourceDetail" );
				ods.SelectParameters[ 1 ].DefaultValue = row[ "EmployeeNumber" ].ToString( );
			}
		}

		#endregion

		#region Grid Search Click

		protected void btnSearch_Click( object sender, EventArgs e )
		{
			if ( ViewState[ "CurrentSearch" ] != null )
			{
				switch ( ViewState[ "CurrentSearch" ].ToString( ) )
				{
					case ( "EmpNo" ):
						DoEmpNoSearch( txtSearchCriteria.Text.Trim( ) );
						break;
					case ( "SSN" ):
						DoSSNSearch( txtSearchCriteria.Text.Trim( ) );
						break;
				}
			}
			txtSearchCriteria.Text = string.Empty;
		}

		protected void btnSearchBudgetFilter_Click( object sender, EventArgs e )
		{
			if ( ViewState[ "CurrentSearch" ] != null )
			{
				switch ( ViewState[ "CurrentSearch" ].ToString( ) )
				{
					case ( "Budget" ):
						valMultiSearch.Validate( );
						if ( Page.IsValid )
						{
							DoBudgetSearch( txtSearchCritBudget.Text.Trim( ) );
							lblMultiSearchType.Text = " Budget " + txtSearchCritBudget.Text.Trim( );
							
						}
						break;
					case ( "Engineer" ):
						valMultiSearch.Validate( );
						if ( Page.IsValid )
						{
							DoEngineerSearch( txtSearchCritBudget.Text.Trim( ) );
							lblMultiSearchType.Text = "Budget Number 490 and Engineer Number " + txtSearchCritBudget.Text.Trim( );
							
						}
						break;
					case( "Name"):
						valMultiSearch.Validate( );
						if ( Page.IsValid )
						{
							DoNameSearch( txtSearchCritBudget.Text.Trim( ) );
							lblMultiSearchType.Text = "Name " + Entities.EarnStatement.ToTitleCase( txtSearchCritBudget.Text.Trim( ).ToLower( ) );
							
						}

						break;
				}
				txtSearchCritBudget.Text = string.Empty;
			}
		}
		#endregion

		#region Search Criteria Button Clicks

		protected void btnSearchEmpNo_Click( object sender, EventArgs e )
		{
			ViewState[ "CurrentSearch" ] = "EmpNo";
			lblSearch.Text = "Employee Number: ";
			valEmpNo.Validate( );
			if ( Page.IsValid )
			{
				DoEmpNoSearch( txtEmpNo.Text.Trim( ) );
				txtEmpNo.Text = string.Empty;
				//this.SearchFilter1.Style[ "Display" ] = "none";
			}

		}

		protected void btnSearchSSN_Click( object sender, EventArgs e )
		{
			ViewState[ "CurrentSearch" ] = "SSN";
			lblSearch.Text = "Social Security Number: ";
			valSSN.Validate( );
			if ( Page.IsValid )
			{
				DoSSNSearch( txtSSN.Text.Trim( ) );
				txtSSN.Text = string.Empty;
				//this.SearchFilter1.Style[ "Display" ] = "none";
			}

		}

		protected void btnSearchBudget_Click( object sender, EventArgs e )
		{

			ViewState[ "CurrentSearch" ] = "Budget";
			lblSearchBudget.Text = "Budget Number (not 490): ";
			lblMultiSearchType.Text = " Budget " + txtBudget.Text.Trim( );
			txtSearchCritBudget.MaxLength = 3;
			valBudget.Validate( );
			if ( Page.IsValid )
			{
				DoBudgetSearch( txtBudget.Text.Trim( ) );
				txtBudget.Text = string.Empty;
				//this.SearchFilter1.Style[ "Display" ] = "none";
			}
			
		}

		protected void btnSearchConstr_Click( object sender, EventArgs e )
		{
			ViewState[ "CurrentSearch" ] = "Engineer";
			lblSearchBudget.Text = "Engineer Number: ";
			lblMultiSearchType.Text = "Budget Number 490 and Engineer Number " + txtConstr.Text.Trim( );
			txtSearchCritBudget.MaxLength = 3;
			valConstr.Validate( );
			if ( Page.IsValid )
			{
				DoEngineerSearch( txtConstr.Text.Trim( ) );
				txtConstr.Text = string.Empty;
				//this.SearchFilter1.Style[ "Display" ] = "none";
				
			}
		}

		protected void btnSearchName_Click( object sender, EventArgs e )
		{
			ViewState[ "CurrentSearch" ] = "Name";
			
			NameLabelSearch.Text = " Name " + Entities.EarnStatement.ToTitleCase( txtName.Text.Trim( ).ToLower() );
			valName.Validate( );
			
			if ( Page.IsValid )
			{
				DoNameSearch( txtName.Text.Trim( ) );
				txtName.Text = string.Empty;
			}
		}

		#endregion

		#region Searches

		/// <summary>
		/// Searches for earning statements based on Employee number
		/// </summary>
		/// <param name="empNumber">Employee number to search for</param>
		private void DoEmpNoSearch( string empNumber )
		{
			string empNo = empNumber;
			if ( empNumber.Length < 9 )
			{
				for ( int x = 0; x < 9 - empNumber.Length; x++ )
				{
					empNo = "0" + empNo;
				}
			}

			dsIndividualResult = Entities.CommonFunctions.SearchByEmpNo( UserSec, empNo );

			if ( dsIndividualResult != null )
			{
				ViewState[ "IndSearchResults" ] = dsIndividualResult;
			}
			else
			{
				ViewState[ "IndSearchResults" ] = null;
			}

			if ( dsIndividualResult.Tables[ 0 ].Rows.Count > 0 )
			{
				DataRow row = dsIndividualResult.Tables[ 0 ].Rows[ 0 ];
				string EmpName;
				EmpName = row[ "NameLast" ].ToString( ) + ", " + row[ "NameFirst" ].ToString( ) + " " + row[ "NameMiddle" ].ToString( ).Substring( 0, 1 );
				lblSingleSearchName.Text = EmpName;
				//lblSingleSearchType.Text = "Employee Number " + empNo;

				GridSingleResults.Style[ "display" ] = "block";
				SearchFilter1.Style[ "display" ] = "none";
				gridViewSingleResults.DataSource = dsIndividualResult;
				gridViewSingleResults.DataBind( );
				spanResult.Style[ "display" ] = "none";
			}
			else
			{
				spanResult.Style[ "display" ] = "inline";
				GridSingleResults.Style[ "display" ] = "none";
				SearchFilter1.Style[ "display" ] = "block";
			}

		}
		
		/// <summary>
		/// Searches for earning statements based on SSN
		/// </summary>
		/// <param name="ssn">Social Security number to search for</param>
		private void DoSSNSearch( string ssn )
		{
			dsIndividualResult = Entities.CommonFunctions.SearchBySSN( UserSec, ssn );

			if ( dsIndividualResult != null )
			{
				ViewState[ "IndSearchResults" ] = dsIndividualResult;
			}
			else
			{
				ViewState[ "IndSearchResults" ] = null;
			}

			if ( dsIndividualResult.Tables[ 0 ].Rows.Count > 0 )
			{
				DataRow row = dsIndividualResult.Tables[ 0 ].Rows[ 0 ];
				string EmpName;
				EmpName = row[ "NameLast" ].ToString( ) + ", " + row[ "NameFirst" ].ToString( ) + " " + row[ "NameMiddle" ].ToString( ).Substring( 0, 1 );
				lblSingleSearchName.Text = EmpName;
				
				//lblSingleSearchType.Text = EmpName;

				GridSingleResults.Style[ "Display" ] = "block";
				SearchFilter1.Style[ "display" ] = "none";
				gridViewSingleResults.DataSource = dsIndividualResult;
				gridViewSingleResults.DataBind( );
				spanResult.Style[ "display" ] = "none";
			}
			else
			{
				spanResult.Style[ "display" ] = "inline";
				GridSingleResults.Style[ "display" ] = "none";
				SearchFilter1.Style[ "display" ] = "block";
			}
		}
				
		/// <summary>
		/// Searches for earning statements based on Budget number
		/// </summary>
		/// <param name="budget">Budget number to search for</param>
		private void DoBudgetSearch( string budget )
		{
			if ( budget.Trim( ).Length > 0 )
			{
				this.gridHeaderResults.DataSource = null;
				gridHeaderResults.PageIndex = 0;
				this.ObjDataSourceHeaderSearch.SelectParameters[ 1 ].DefaultValue = budget;
				this.gridHeaderResults.DataSource = ObjDataSourceHeaderSearch;
				this.gridHeaderResults.DataBind( );
				
				if ( gridHeaderResults.Rows.Count > 0 )
				{
					this.GridMultiResults.Style[ "Display" ] = "block";
					SearchFilter1.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "none";
				}
				else
				{
					SearchFilter1.Style[ "display" ] = "block";
					GridMultiResults.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "inline";
				}
			}
		}
		
		/// <summary>
		/// Searches for earning statements based on Budget 490 and Engineering number
		/// </summary>
		/// <param name="engineerNo">Engineerning number to search for</param>
		private void DoEngineerSearch( string engineerNo )
		{
			if ( engineerNo.Trim( ).Length > 0 )
			{
				this.gridHeaderResults.DataSource = null;
				gridHeaderResults.PageIndex = 0;
				this.ObjDataSourceHeaderSearch.SelectParameters[ 1 ].DefaultValue = engineerNo;
				this.ObjDataSourceHeaderSearch.SelectMethod = "GetHeaderByEngineerSearch";
				this.gridHeaderResults.DataSource = ObjDataSourceHeaderSearch;
				this.gridHeaderResults.DataBind( );
				

				if ( gridHeaderResults.Rows.Count > 0 )
				{
					this.GridMultiResults.Style[ "Display" ] = "block";
					SearchFilter1.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "none";
				}
				else
				{
					SearchFilter1.Style[ "display" ] = "block";
					GridMultiResults.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "inline";
				}
			}
		
		}

		/// <summary>
		/// Searches for earning statements based on employee name
		/// </summary>
		/// <param name="name">Name to be searched for</param>
		private void DoNameSearch( string name )
		{
			if ( name.Trim( ).Length > 0 )
			{
				//this.gridHeaderResults.DataSource = null;
				//gridHeaderResults.PageIndex = 0;
				//this.ObjDataSourceHeaderSearch.SelectMethod = "GetHeaderByName";
				//this.ObjDataSourceHeaderSearch.SelectParameters[ 1 ].DefaultValue = name;
				//this.gridHeaderResults.DataSource = ObjDataSourceHeaderSearch;
				//this.gridHeaderResults.DataBind( );
				this.GridNameHeader.DataSource = null;
				GridNameHeader.PageIndex = 0;
				this.ObjectDataSourceName.SelectMethod = "GetHeaderByName";
				this.ObjectDataSourceName.SelectParameters[ 1 ].DefaultValue = name;
				this.GridNameHeader.DataSource = ObjectDataSourceName;
				this.GridNameHeader.DataBind( );

				//if ( gridHeaderResults.Rows.Count > 1 )
				if( GridNameHeader.Rows.Count > 1 )
				{
					this.GridMultiResults.Style[ "Display" ] = "none";
					NameSearchDiv.Style[ "Display" ] = "block";
					SearchFilter1.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "none";
				}
				else if ( GridNameHeader.Rows.Count == 1 )
				{

					Panel p1 = ( Panel ) GridNameHeader.Rows[ 0 ].Cells[ 0 ].FindControl( "pnlDetails" );
					LinkButton lbl = ( LinkButton ) GridNameHeader.Rows[ 0 ].Cells[ 0 ].FindControl( "lnkShowDetails" );
					p1.Visible = true;
					lbl.Text = "";


					this.GridMultiResults.Style[ "Display" ] = "none";
					this.NameSearchDiv.Style[ "display" ] = "block";
					SearchFilter1.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "none";

				}
				else
				{
					SearchFilter1.Style[ "display" ] = "block";
					GridMultiResults.Style[ "display" ] = "none";
					this.NameSearchDiv.Style[ "display" ] = "none";
					spanResult.Style[ "display" ] = "inline";
				}
			}
		}
		#endregion

		#region Search Criteria Validation
		protected void valEmpNo_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;
			
			if ( txtEmpNo.Text.Trim( ).Length < 5 )
			{
				valEmpNo.ErrorMessage = "Pleaser enter at least 5 numbers for Employee Number";
				result = false;
			}
			else if ( !Entities.CommonFunctions.IsNumeric( txtEmpNo.Text.Trim( ) ) )
			{
				valEmpNo.ErrorMessage = "Employee Number must be numeric";
				result = false;
			}
			
			args.IsValid = result;
		}

		protected void valBudget_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;
			
			if( txtBudget.Text.Trim().Length < 3 )
			{
				result = false;
				valBudget.ErrorMessage = "Budget search criteria must be 3 digits";
			}
			else if( !Entities.CommonFunctions.IsNumeric( txtBudget.Text.Trim() ) )
			{
				result = false;
				valBudget.ErrorMessage = "Budget search criteria must be numeric";
			}
			else if( txtBudget.Text.Trim() == "490")
			{
				valBudget.ErrorMessage = "To search Budget 490, use the fourth search criteria and enter the R.E. No.";
				result = false;
			}

			args.IsValid = result;
		}

		protected void valConstr_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;
			
			if( txtConstr.Text.Trim().Length < 2 )
			{
				valConstr.ErrorMessage = "Budget Engineering should be two digits";
				result = false;
			}
			else if( !Entities.CommonFunctions.IsNumeric( txtConstr.Text.Trim() ) )
			{
				valConstr.ErrorMessage = "Budget Engineering searches must be numeric";
				result = false;
			}
			args.IsValid = result;
		}

		protected void valSSN_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;

			if( txtSSN.Text.Trim().Length < 9 )
			{
				valSSN.ErrorMessage = "SSN must be 9 characters";
				result = false;
			}
			else if( !Entities.CommonFunctions.IsNumeric( txtSSN.Text.Trim() ) )
			{
				valSSN.ErrorMessage = "SSN Number must be numeric";
				result = false;
			}
			
			args.IsValid = result;
		}

		protected void valName_ServerValidate( object source, ServerValidateEventArgs args )
		{
			
		}
		#endregion

		#region Quick Search Validation
		protected void valMultiSearch_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;
			args.IsValid = result;

			if ( ViewState[ "CurrentSearch" ] != null )
			{
				if ( ViewState[ "CurrentSearch" ].ToString() == "Engineer" )
				{
					if ( txtSearchCritBudget.Text.Trim( ).Length < 2 )
					{
						valMultiSearch.ErrorMessage = "Engineer number must be 2 numbers";
						result = false;
					}
					else if ( txtSearchCritBudget.Text.Trim( ).Length > 2 )
					{
						valMultiSearch.ErrorMessage = "Engineer number must be 2 numbers";
						result = false;
					}
					else if ( !Entities.CommonFunctions.IsNumeric( txtSearchCritBudget.Text.Trim( ) ) )
					{
						valMultiSearch.ErrorMessage = "Engineer number must be numeric";
						result = false;
					}
				}

				if ( ViewState[ "CurrentSearch" ].ToString() == "Budget" )
				{
					if ( txtSearchCritBudget.Text.Trim( ).Length < 3 )
					{
						valMultiSearch.ErrorMessage = "Budget must be 3 numbers";
						result = false;
					}
					else if ( txtSearchCritBudget.Text.Trim( ) == "490" )
					{
						valMultiSearch.ErrorMessage = "To Search Budget 490 please use the appropriate option";
						result = false;
					}
					else if ( !Entities.CommonFunctions.IsNumeric( txtSearchCritBudget.Text.Trim( ) ) )
					{
						valMultiSearch.ErrorMessage = "Must be numeric";
						result = false;
					}
				}

				if ( ViewState[ "CurrentSearch" ].ToString( ) == "Name" )
				{
					if ( txtSearchCritBudget.Text.Trim( ).Length == 0 )
					{
						valMultiSearch.ErrorMessage = "Employee name must be provided";
						result = false;
					}
				}
			}
			else
			{
				result = false;
			}

			args.IsValid = result;
		}

		protected void valSingleSearch_ServerValidate( object source, ServerValidateEventArgs args )
		{
			bool result = true;
			if ( ViewState[ "CurrentSearch" ] != null )
			{
				if( ViewState["CurrentSearch"].ToString() == "EmpNo" )
				{
					if ( txtSearchCriteria.Text.Trim( ).Length < 5 )
					{
						valSingleSearch.ErrorMessage = "Employee Number must be at least 5 characters";
						result = false;
					}
					else if ( !Entities.CommonFunctions.IsNumeric( txtSearchCriteria.Text.Trim( ) ) )
					{
						valSingleSearch.ErrorMessage = "Employee Number must be numeric";
						result = false;
					}
				}

				if ( ViewState[ "CurrentSearch" ].ToString( ) == "SSN" )
				{
					if ( txtSearchCriteria.Text.Trim( ).Length < 9 )
					{
						valSingleSearch.ErrorMessage = "SSN must be 9 characters long";
						result = false;
					}
					else if ( !Entities.CommonFunctions.IsNumeric( txtSearchCriteria.Text.Trim( ) ) )
					{
						valSingleSearch.ErrorMessage = "SSN must be numeric";
						result = false;
					}
				}
			}
			else
			{
				result = false;
			}

			args.IsValid = result;
		}

		protected void ValQuickNameSearch_ServerValidate( object source, ServerValidateEventArgs args )
		{
			if ( TxtQuickNameSearch.Text.Trim( ).Length == 0 )
			{
				valMultiSearch.ErrorMessage = "Employee name must be provided";
				args.IsValid = false;
			}
		}

		#endregion

		#region Navigation Buttons
		protected void btnHome_Click( object sender, EventArgs e )
		{
			ViewState[ "CurrentSearch" ] = null;
			Session[ "EarnStatementObject" ] = null;
			ViewState[ "DetailDataSet" ] = null;
			ViewState[ "IndSearchResults" ] = null;

			Server.Transfer( "Default.aspx" );
		}

		protected void btnAHTDNet_Click( object sender, EventArgs e )
		{
			Response.Redirect( "http://ahtdnet" );
		}

		protected void BtnQuickNameSearch_Click( object sender, EventArgs e )
		{

			ValQuickNameSearch.Validate( );
			if ( Page.IsValid )
			{
				DoNameSearch( TxtQuickNameSearch.Text.Trim() );
				NameLabelSearch.Text = "Name " + Entities.EarnStatement.ToTitleCase( TxtQuickNameSearch.Text.Trim( ).ToLower( ) );

			}
		}

		#endregion

		protected void txtEmpNo_TextChanged( object sender, EventArgs e )
		{

		}
		
	}
}
