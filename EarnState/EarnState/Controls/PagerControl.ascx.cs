using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[assembly: System.Web.UI.TagPrefix( "AHTD.EarnState.Controls", "PC" )]
namespace AHTD.EarnState.Controls
{
	
	public partial class PagerControl : System.Web.UI.UserControl
	{
		#region private members
		GridView _grid;
		string _gridName;
		#endregion

		#region Public properties
		public event EventHandler<EventArgs> NextButtonClicked;
		public event EventHandler<EventArgs> PrevButtonClicked;
		public event EventHandler<EventArgs> FirstButtonClicked;
		public event EventHandler<EventArgs> LastButtonClicked;

		public event EventHandler<EventArgs> PageSizeChanged;

		[Browsable( true)]
		public string ParentGrid
		{
			get
			{
				return _gridName;
			}
			set
			{
				_gridName = value;
			}
		}
		#endregion

		#region Page Load
		protected void Page_Load( object sender, EventArgs e )
		{
			
		}
		#endregion

		#region On Init
		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );
			if ( !string.IsNullOrEmpty( _gridName ) )
			{
				_grid = ( GridView ) Page.FindControl( _gridName );
			}

		}
		#endregion

		#region Link Buttons clicked
		protected void LnkNext_Click( object sender, EventArgs e )
		{
			if ( NextButtonClicked != null )
			{
				GridViewPageEventArgs args = new GridViewPageEventArgs(_grid.PageIndex +1);
				
				NextButtonClicked( _grid, args );
			}

		}

		protected void LnkPrev_Click( object sender, EventArgs e )
		{
			if ( PrevButtonClicked != null )
			{
				GridViewPageEventArgs args = new GridViewPageEventArgs( _grid.PageIndex - 1 );
				PrevButtonClicked( _grid, args );

			}
		}

		protected void LnkFirst_Click( object sender, EventArgs e )
		{
			if ( FirstButtonClicked != null )
			{
				GridViewPageEventArgs args = new GridViewPageEventArgs( 0 );
				FirstButtonClicked( _grid, args );
			}
		}

		protected void LnkLast_Click( object sender, EventArgs e )
		{
			if ( LastButtonClicked != null )
			{
				GridViewPageEventArgs args = new GridViewPageEventArgs( _grid.PageCount - 1 );
				LastButtonClicked( _grid, args );
			}
		}
		#endregion

		#region Button PreRender Events
		protected void LnkPrev_PreRender( object sender, EventArgs e )
		{
			if ( _grid.PageIndex == 0 )
			{
				LnkPrev.Style.Add( "display", "none" );
			}
			else 
			{
				LnkPrev.Style.Add( "display", "inline" );
				LnkPrev.ToolTip = GetPreviousRecordCount( );
			}

		}

		protected void LnkFirst_PreRender( object sender, EventArgs e )
		{
			if ( _grid.PageIndex == 0 )
			{
				LnkFirst.Style.Add( "display", "none" );
			}
			else
			{
				LnkFirst.Style.Add( "display", "inline" );
			}
		}

		protected void LnkNext_PreRender( object sender, EventArgs e )
		{
			if ( _grid.PageIndex == _grid.PageCount - 1 )
			{
				LnkNext.Style.Add( "display", "none" );

			}
			else
			{
				LnkNext.Style.Add( "display", "inline");
				LnkNext.ToolTip = GetNextRecordCount( );
			}

			
		}

		protected void LnkLast_PreRender( object sender, EventArgs e )
		{
			if ( _grid.PageIndex == _grid.PageCount - 1 )
			{
				LnkLast.Style.Add( "display", "none" );
			}
			else
			{
				LnkLast.Style.Add( "display", "inline" );
			}
		}

		protected void DDLRecords_PreRender( object sender, EventArgs e )
		{
			DDLRecords.SelectedIndex = DDLRecords.Items.IndexOf( DDLRecords.Items.FindByValue( _grid.PageSize.ToString( ) ) );
		}

		#endregion

		#region DDLRecords Selected Index Changed
		protected void DDLRecords_SelectedIndexChanged( object sender, EventArgs e )
		{
			if ( PageSizeChanged != null )
			{
				CommandEventArgs args = new CommandEventArgs( "Rows", DDLRecords.SelectedValue );
				PageSizeChanged( _grid, args );
			}
		}
		#endregion

		#region Get Record count for tool tip
		protected string GetNextRecordCount( )
		{
			string count;
			count = "Go to page " + Convert.ToString( ( _grid.PageIndex + 2 ) ) + " of " + _grid.PageCount.ToString();
						
			return count;
		}

		protected string GetPreviousRecordCount( )
		{
			string count;

			count = "Go to page " + _grid.PageIndex.ToString( ) + " of " + _grid.PageCount.ToString( );

			return count;
		}
		#endregion

	}
}