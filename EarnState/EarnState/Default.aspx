<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="MainSkin" MaintainScrollPositionOnPostback="false" CodeBehind="Default.aspx.cs" Inherits="AHTD.EarnState._Default" %>

<%@ Register Src="Controls/PagerControl.ascx" TagName="PagerControl" TagPrefix="uc1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >


<script type="text/javascript">
function TrapKeyDown(btnName){
     var btn = document.getElementById(btnName.id);
      if (event.keyCode == 13)
      {
       event.returnValue=false;
       event.cancel = true;
       btn.click();
      }
}

</script>

<head runat="server">
    <title>PAHR Earning Statements</title>
	<link href="App_Themes/MainSkin/MainStyle.css" rel="stylesheet" type="text/css" />
	<link href="App_Themes/MainSkin/Header.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
	  <div id="MainDiv" style="text-align:center">
		      <div class="header">
				<div class="Outer">
					<div class="Inner">
						<div class="Content">
							<h2>PAHR Employee Earnings Statement</h2>
						</div>
					</div>
				</div>
			</div>
		<asp:ScriptManager ID="ScriptManager1" runat="server" >
				</asp:ScriptManager>
		
		  <div id="SearchFilter1" class="SearchBody" runat="server" style="border-style:solid;border-width:1px" >
			<div class="Outer">
			    	<div class="Inner">	
				    	<div class="Content">
						
							<div> 
								<span >
									Employee Number:
								</span><asp:CustomValidator ID="valEmpNo" runat="Server" Text="*" ControlToValidate="txtEmpNo"  OnServerValidate="valEmpNo_ServerValidate" EnableClientScript="False" ErrorMessage="Employee number must be numeric"></asp:CustomValidator>
								
								
								<asp:TextBox ID="txtEmpNo" SkinID="search" runat="server" Width="63px" MaxLength="9" BackColor="WhiteSmoke" AutoPostBack="False" ></asp:TextBox>
									
								<asp:Button ID="btnSearchEmpNo" runat="Server" Text="Search" OnClick="btnSearchEmpNo_Click"/>
							</div>
							<div style="display:none">
								<span>
									Social Security Number: 
								</span><asp:CustomValidator ID="valSSN" runat="Server" Text="*" ControlToValidate="txtSSN"  EnableClientScript="False" ErrorMessage="SSN must be numeric" OnServerValidate="valSSN_ServerValidate"></asp:CustomValidator>
								<asp:textbox ID="txtSSN" SkinID="search" runat="server" Width="63px" MaxLength="9" BackColor="WhiteSmoke"></asp:textbox>
								
								<asp:Button ID="btnSearchSSN" runat="Server" Text="Search" OnClick="btnSearchSSN_Click" />
							</div>
							<div >
								<span>
									Budget Number: 
								</span><asp:CustomValidator ID="valBudget" runat="Server" Text="*" ControlToValidate="txtBudget"  EnableClientScript="False" OnServerValidate="valBudget_ServerValidate"></asp:CustomValidator>
								<asp:TextBox ID="txtBudget" SkinID="search" runat="server" Width="63px" MaxLength="3" BackColor="WhiteSmoke"></asp:TextBox>
								
								<asp:Button ID="btnSearchBudget" runat="server" Text="Search" OnClick="btnSearchBudget_Click" />
							</div>
							<div >
								<span>
									Construction Budget #490 and Engineer Number 
								</span><asp:CustomValidator ID="valConstr" runat="Server" Text="*" ControlToValidate="txtConstr"  EnableClientScript="False" ErrorMessage="Criteria must be numeric" OnServerValidate="valConstr_ServerValidate" SetFocusOnError="True"></asp:CustomValidator>
								<asp:Textbox ID="txtConstr" SkinID="search" runat="server" Width="63px" MaxLength="2" BackColor="WhiteSmoke"></asp:Textbox>
								
								<asp:Button ID="btnSearchConstr" runat="server" Text="Search" OnClick="btnSearchConstr_Click" />
								<span id="InstructionLabel"><asp:Label ID="lblBudget" runat="server" Width="328px">(Enter R.E. No. for Construction Budget 490 Search)</asp:Label></span>
							</div>
							<div>
								<span>
									Employee Last Name:
								</span><asp:CustomValidator ID="valName" runat="Server" Text="*" EnableClientScript="false" OnServerValidate="valName_ServerValidate" ErrorMessage="A last name must be provided"></asp:CustomValidator>
								<asp:TextBox ID="txtName" SkinID="Search" runat="server" Width="170px" MaxLength="27" BackColor="whitesmoke"></asp:TextBox>
								
								<asp:Button ID="btnSearchName" runat="server" Text="Search" OnClick="btnSearchName_Click" />
							</div>
						
						</div>
					
			    	</div>
			  </div>
		  </div>
		
		<div id="results" runat="server">
			<span id="spanResult" class="spanResult" runat="Server">Either no results were found or you do not have permission to complete this search.</span>
			<div id="GridSingleResults" runat="server" >
				<table id="tblGridSingleResults">
					<tr>
						<td align="left">
							<asp:Label ID="lblSearch" runat="server"></asp:Label><asp:TextBox ID="txtSearchCriteria" runat="server" MaxLength="9"></asp:TextBox>
							<asp:CustomValidator ID="valSingleSearch" runat="server" Text="*" SkinID="multiSearchVal" ControlToValidate="txtSearchCriteria" OnServerValidate="valSingleSearch_ServerValidate" />
							<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
							<asp:Button ID="btnSingleResultReturn" runat="server" Text="Earnings Statement Home" OnClick="btnHome_Click" Width="161px" /></td>
					</tr>
					<tr>
						<td align="left">
							<div id="divDisplaySingleSearchInfo" runat="server">
								<asp:Label ID="lblSingleSearchNameHdr" runat="server">Name:</asp:Label>
								<asp:label ID="lblSingleSearchName" runat="server"></asp:label>
							</div>
						</td>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="gridViewSingleResults" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gridViewSingleResults_SelectedIndexChanged" PageSize="5" >
								<Columns>
									<asp:BoundField HeaderText="Pay Period" DataField="PayPeriodEndingDate" DataFormatString="{0:d}" HtmlEncode="False" >
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
									</asp:BoundField>
									<asp:BoundField HeaderText="Budget" DataField="Budget" />
									<asp:BoundField HeaderText="Crew No" DataField="CrewNo" />
									<asp:BoundField HeaderText="Engineer No" DataField="EngNo" />
									<asp:BoundField HeaderText="Payment Type" DataField="WarrantOrDirectDeposit" />
									<asp:CommandField SelectText="View" ShowSelectButton="True" />
								</Columns>
								<HeaderStyle BackColor="#3E6497" BorderColor="#E8F2F0" BorderStyle="Solid" BorderWidth="1px"
									Font-Names="Verdana" Font-Size="10pt" ForeColor="WhiteSmoke" />
								<RowStyle BackColor="WhiteSmoke" BorderColor="#E8F2F0" BorderStyle="Solid" BorderWidth="1px"
									Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" />
								<AlternatingRowStyle BackColor="#7D93B6" BorderColor="#E8F2F0" BorderStyle="Solid"
									BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="WhiteSmoke" />
							</asp:GridView>
						</td>
					</tr>
				</table>
			</div>
			
			<div id="GridMultiResults" runat="server"  >
				<table id="tblMultiResults">
					<tr>
						<td align="left">
							<asp:Label ID="lblSearchBudget" runat="server"></asp:Label><asp:TextBox ID="txtSearchCritBudget" runat="server" MaxLength="3"></asp:TextBox>
							<asp:CustomValidator ID="valMultiSearch" SkinID="multiSearchVal" runat="server" ControlToValidate= "txtSearchCritBudget" Text="*" OnServerValidate="valMultiSearch_ServerValidate" />
							<asp:Button ID="btnSearchBudgetFilter" runat="server" Text="Search" OnClick="btnSearchBudgetFilter_Click" />
							<asp:Button ID="btnMultiResultReturn" runat="server" Text="Earnings Statement Home" OnClick="btnHome_Click" Width="161px" /></td>
					</tr>
					<tr>
						<td align="left">
							<div id="divDisplayMultiSearchInfo" runat="server">
								<asp:Label ID="lblMultiSearchInfoHeader" runat="Server" 
									 Text = "Employee Earning Statement for"></asp:Label>
								<asp:Label ID="lblMultiSearchType" runat="server" ></asp:Label>
							</div>
						</td>
					</tr>
					<tr>
						<td align= "left">
						  <asp:UpdatePanel runat="server" id="pnlMultiResults">
							<ContentTemplate>
						
								<asp:GridView ID="gridHeaderResults" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridHeaderResults_RowDataBound" AllowPaging="True" OnPageIndexChanging="gridHeaderResults_PageIndexChanging" style="overflow:hidden"  PageSize="20"  >
									<Columns>
										<asp:TemplateField>
										  
											<HeaderTemplate>
												<span class="Col1">+</span>
												<span class="Col2">Name</span>
												<span class="Col3">Employee #</span>
												
											 </HeaderTemplate>
											<ItemTemplate>
												 <asp:Panel id="pnlHeaderInfo" runat="server">
													  <span class="Col1" style="height:19px "><asp:LinkButton ID="lnkShowDetails" runat="server" Font-Size="Medium" OnClick="lnkShowDetails_Click" Font-Underline="False" ForeColor="Black">+</asp:LinkButton></span>
													  <span class="Col2" style="height:19px ">  <%#Eval("Name") %></span>
													  <span class="Col3" style="height:19px">	<%#Eval("EmployeeNumber") %></span>
													  
												 </asp:Panel>
												<div id="ResultPanel">
											
												<asp:Panel ID="pnlDetails" runat="server" style="width:25px" Visible="false" >
													<asp:GridView ID="gridDetailView" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceDetail" OnSelectedIndexChanged="gridDetailView_SelectedIndexChanged" SkinID="detailGrid">
													<Columns>
														<asp:BoundField HeaderText="Pay Period" DataField="PayPeriodEndingDate" DataFormatString="{0:d}" HtmlEncode="False" >
															<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
														</asp:BoundField>
														<asp:BoundField HeaderText="Budget" DataField="Budget" />
														<asp:BoundField HeaderText="Crew No" DataField="CrewNo" />
														<asp:BoundField HeaderText="Engineer No" DataField="EngNo" >
															<ItemStyle Width="75px" />
															<HeaderStyle Width="85px" />
														</asp:BoundField>
														<asp:BoundField HeaderText="Payment Type" DataField="WarrantOrDirectDeposit" />
														<asp:CommandField SelectText="View" ShowSelectButton="True" />
													</Columns>
												<RowStyle BackColor="WhiteSmoke" BorderColor="#D2D6D9" BorderStyle="Solid" BorderWidth="1px"
													Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" />
												<HeaderStyle BackColor="#7D93B6" BorderColor="#D2D6D9" BorderStyle="Solid" BorderWidth="1pt"
													Font-Names="Verdana" Font-Size="9pt" ForeColor="#E8F2F0" HorizontalAlign="Center"
													Wrap="False" />
												<AlternatingRowStyle BackColor="#3E6497" BorderColor="#D2D6D9" BorderStyle="Solid"
													BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="#E8F2F0" />
											</asp:GridView>
														<asp:ObjectDataSource ID="ObjectDataSourceDetail" runat="server"
															TypeName="AHTD.Entities.CommonFunctions" EnableCaching="True" SelectMethod="GetMultiSearchDetail" OnSelected="ObjectDataSourceDetail_Selected">
															<SelectParameters>
																<asp:SessionParameter Name="UserSec" SessionField="UserSec" Type="Object" />
																<asp:Parameter Name="criteria" Type="String" />
															</SelectParameters>
														</asp:ObjectDataSource>
												</asp:Panel>
											 </div>
											</ItemTemplate>
											<FooterTemplate>
												
											</FooterTemplate>
										</asp:TemplateField>
									</Columns>
									<PagerStyle BorderColor="#7D93B6" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt"
										ForeColor="#3E6497" HorizontalAlign="Center" Width="200px" />
									<PagerSettings Mode="NextPreviousFirstLast" />
									<PagerTemplate>
										<uc1:PagerControl ID="PagerControl1" runat="server" ParentGrid="gridHeaderResults" EnableViewState="true" />
									</PagerTemplate>
								</asp:GridView>
								
								
								
							</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="gridHeaderResults" EventName="RowCommand" />
									
								</Triggers>
							</asp:UpdatePanel>
							<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="pnlMultiResults">
									
									<ProgressTemplate>
										<div id="Progress" >
											<img src="Images/loader64.gif"  alt="Loading"/>
											<p>Loading...</p>
										</div>
									</ProgressTemplate>
								</asp:UpdateProgress>
						</td>
					</tr>
				
				
				</table>		
				<asp:ObjectDataSource ID="ObjDataSourceHeaderSearch" runat="server" SelectMethod="GetHeaderByBudgetSearch" TypeName="AHTD.Entities.CommonFunctions" EnableCaching="True">
					<SelectParameters>
						<asp:SessionParameter Name="UserSec" SessionField="UserSec" Type="Object" />
						<asp:Parameter Name="criteria" Type="String" />
					</SelectParameters>
				</asp:ObjectDataSource>
				
			</div>
			<div id="NameSearchDiv" runat="server">
				<table id="Table1">
					<tr>
						<td align="left">
							<asp:Label ID="Label1" runat="server">Employee Name: </asp:Label><asp:TextBox ID="TxtQuickNameSearch" runat="server" MaxLength="27"></asp:TextBox>
							<asp:CustomValidator ID="ValQuickNameSearch" SkinID="multiSearchVal" runat="server" ControlToValidate= "TxtQuickNameSearch" Text="*" OnServerValidate="ValQuickNameSearch_ServerValidate" />
							<asp:Button ID="BtnQuickNameSearch" runat="server" Text="Search" OnClick="BtnQuickNameSearch_Click" />
							<asp:Button ID="BtnNameSearchHome" runat="server" Text="Earnings Statement Home" OnClick="btnHome_Click" Width="161px" /></td>
					</tr>
					<tr>
						<td align="left">
							<div id="div1" runat="server">
								<asp:Label ID="Label2" runat="Server" 
									 Text = "Employee Earning Statement for"></asp:Label>
								<asp:Label ID="NameLabelSearch" runat="server" ></asp:Label>
							</div>
						</td>
					</tr>
					<tr>
						<td align= "left">
						  <asp:UpdatePanel runat="server" id="UpdatePanel1">
							<ContentTemplate>
						
							<asp:GridView ID="GridNameHeader" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridNameHeader_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridNameHeader_PageIndexChanging" style="overflow:hidden" PageSize="20" >
									<Columns>
										<asp:TemplateField>
										  
											<HeaderTemplate>
												<span class="NameCol1">+</span>
												<span class="NameCol2">Name</span>
												<span class="NameCol3">Employee #</span>
												<span class="NameCol4">Eng #</span>
												<span class="NameCol5">Crew</span>
											 </HeaderTemplate>
											<ItemTemplate>
												 <asp:Panel id="pnlHeaderInfo" runat="server">
													  <span class="NameCol1" style="height:19px "><asp:LinkButton ID="lnkShowDetails" runat="server" Font-Size="Medium" OnClick="lnkShowDetails_Click" Font-Underline="False" ForeColor="Black">+</asp:LinkButton></span>
													  <span class="NameCol2" style="height:19px ">  <%#Eval("Name") %></span>
													  <span class="NameCol3" style="height:19px">	<%#Eval("EmployeeNumber") %></span>
													  <span class="NameCol4" style="height:19px">   <%#Eval("EngNo") %></span>
													  <span class="NameCol5" style="height:19px">   <%#Eval("CrewNo") %></span>
												</asp:Panel>
												<div id="ResultPanel">
												<asp:Panel ID="pnlDetails" runat="server" style="width:25px" Visible="false" >
													<asp:GridView ID="gridDetailView" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceDetail" OnSelectedIndexChanged="gridDetailView_SelectedIndexChanged" SkinID="detailGrid">
													<Columns>
														<asp:BoundField HeaderText="Pay Period" DataField="PayPeriodEndingDate" DataFormatString="{0:d}" HtmlEncode="False" >
															<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
														</asp:BoundField>
														<asp:BoundField HeaderText="Budget" DataField="Budget" />
														<asp:BoundField HeaderText="Crew No" DataField="CrewNo" />
														<asp:BoundField HeaderText="Engineer No" DataField="EngNo" >
															<ItemStyle Width="75px" />
															<HeaderStyle Width="85px" />
														</asp:BoundField>
														<asp:BoundField HeaderText="Payment Type" DataField="WarrantOrDirectDeposit" />
														<asp:CommandField SelectText="View" ShowSelectButton="True" />
													</Columns>
												<RowStyle BackColor="WhiteSmoke" BorderColor="#D2D6D9" BorderStyle="Solid" BorderWidth="1px"
													Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" />
												<HeaderStyle BackColor="#7D93B6" BorderColor="#D2D6D9" BorderStyle="Solid" BorderWidth="1pt"
													Font-Names="Verdana" Font-Size="9pt" ForeColor="#E8F2F0" HorizontalAlign="Center"
													Wrap="False" />
												<AlternatingRowStyle BackColor="#3E6497" BorderColor="#D2D6D9" BorderStyle="Solid"
													BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="#E8F2F0" />
											</asp:GridView>
														<asp:ObjectDataSource ID="ObjectDataSourceDetail" runat="server"
															TypeName="AHTD.Entities.CommonFunctions" EnableCaching="True" SelectMethod="GetMultiSearchDetail" OnSelected="ObjectDataSourceDetail_Selected">
															<SelectParameters>
																<asp:SessionParameter Name="UserSec" SessionField="UserSec" Type="Object" />
																<asp:Parameter Name="criteria" Type="String" />
															</SelectParameters>
														</asp:ObjectDataSource>
												</asp:Panel>
											 </div>
											</ItemTemplate>
											<FooterTemplate>
												
											</FooterTemplate>
										</asp:TemplateField>
									</Columns>
									<PagerStyle BorderColor="#7D93B6" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt"
										ForeColor="#3E6497" HorizontalAlign="Center" Width="200px"  />
									<PagerSettings Mode="NextPreviousFirstLast" />
								<RowStyle Wrap="False" />
								<PagerTemplate>
									<uc1:PagerControl ID="PagerControl2" runat="server" ParentGrid="GridNameHeader" />
								</PagerTemplate>
								</asp:GridView>
								
								
								
							</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="GridNameHeader" EventName="RowCommand" />
									
								</Triggers>
							</asp:UpdatePanel>
							<asp:UpdateProgress ID="UpdateProgress2" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="pnlMultiResults">
									
									<ProgressTemplate>
										<div id="Progress" >
											<img src="Images/loader64.gif"  alt="Loading"/>
											<p>Loading...</p>
										</div>
									</ProgressTemplate>
								</asp:UpdateProgress>
						</td>
					</tr>
				
				
				</table>		
				<asp:ObjectDataSource ID="ObjectDataSourceName" runat="server" SelectMethod="GetHeaderByName" TypeName="AHTD.Entities.CommonFunctions" EnableCaching="True">
					<SelectParameters>
						<asp:SessionParameter Name="UserSec" SessionField="UserSec" Type="Object" />
						<asp:Parameter Name="criteria" Type="String" />
					</SelectParameters>
				</asp:ObjectDataSource>
			</div>
			<div >
				<asp:Button ID="btnAHTDNet" runat="server" style="display:none" Text="AHTDnet" OnClick="btnAHTDNet_Click" Width="169px" />
				<asp:Button ID="btnHome" runat="server" Text="Earnings Statement Home" OnClick="btnHome_Click" Width="169px" />
			</div>
       </div>
		 <div id="ErrorSummary"> 
			<asp:ValidationSummary ID="valSummary" runat="server" />
		</div>
				
     </div>
		<div id="pageFooter">
			 <div class="content">
				<div ><span>Build Date: </span> <asp:Label ID="lblUpdated" runat="server"></asp:Label> </div>
				<div ><span>Document Path Location: </span><asp:Label ID="lblPath" runat="Server" Text=""></asp:Label> </div>
				<div ><span>Computer Help Desk: </span> <a href="mailto:helpdesk@arkansashighways.com?subject=Error Report" >helpdesk@arkansashighways.com</a>
				</div>             
			</div>	
		</div>
     </form>
</body>
</html>
