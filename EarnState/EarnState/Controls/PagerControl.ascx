<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagerControl.ascx.cs" Inherits="AHTD.EarnState.Controls.PagerControl" %>
	<div class="FooterControl">

			<asp:LinkButton ID="LnkFirst" runat="server" Text="<<" OnClick="LnkFirst_Click" OnPreRender="LnkFirst_PreRender" ToolTip="Go to first page"></asp:LinkButton>
				&nbsp;&nbsp;
			<asp:LinkButton ID="LnkPrev" runat="server" Text="<" OnClick="LnkPrev_Click" OnPreRender="LnkPrev_PreRender"></asp:LinkButton>
				&nbsp;&nbsp;
			<asp:LinkButton ID="LnkNext" Text=">" runat="server" OnClick="LnkNext_Click" OnPreRender="LnkNext_PreRender" ></asp:LinkButton>
				&nbsp;&nbsp;
			<asp:LinkButton ID="LnkLast" runat="server" Text=">>" OnClick="LnkLast_Click" OnPreRender="LnkLast_PreRender" ToolTip="Go to last page"></asp:LinkButton>
				&nbsp;&nbsp;

			<span style="text-align:Right">Records per page: 
				<asp:DropDownList ID="DDLRecords" runat="server" AutoPostBack="True" OnPreRender="DDLRecords_PreRender" OnSelectedIndexChanged="DDLRecords_SelectedIndexChanged" ToolTip="Set the number of records visible at one time">
					<asp:ListItem>10</asp:ListItem>
					<asp:ListItem Selected="True">20</asp:ListItem>
					<asp:ListItem>50</asp:ListItem>
					<asp:ListItem>100</asp:ListItem>
				</asp:DropDownList>
			</span>
	</div>