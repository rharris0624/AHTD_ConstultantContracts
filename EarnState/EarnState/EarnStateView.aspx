<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EarnStateView.aspx.cs" Inherits="AHTD.EarnState.EarnStateView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Earning Statements</title>
	<link href="App_Themes/MainSkin/ViewStatement.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/MainSkin/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
	
    <div id="MainStateDiv">
		<div id="Employee">
		<p style=" text-align:left">
			<asp:Label ID="EmpName" runat="server" style=""></asp:Label><br />
			<asp:Label ID="EmpAddr1" runat="server"></asp:Label><br />
			<asp:Label ID="EmpCityState" runat="server"></asp:Label><br />
		</p>
		
		</div>
		<div style="text-align:center">
			
			<h4>AHTD EMPLOYEE EARNINGS STATEMENT</h4>
			<table id="tblHeader">
				<tr class="EarnViewTableHeader">
					<td>
					<p>Employee No.</p>
					</td>
					<td>
						<p>Budget</p>
					 </td>
					<td>
						<p>Crew No.</p>
					</td>
					<td>
						<p>Engineer No.</p>
					</td>
				</tr>
				<tr>
					<td><asp:Label ID="lblEmpNo" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblBudgetNo" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblCrewNo" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblEngNo" runat="server"></asp:Label></td>
				</tr>
			</table>
		
		</div>
		
		<div id="PayPeriod">
			<h4>CURRENT PAYPERIOD HOURS/AMOUNT</h4>
			<table id="tblPayPeriod">
				<tr class="EarnViewTableHeader">
					<td><p>Payperiod Ending</p></td>
					<td><p>Net Pay</p></td>
					<td><p>Type of Payment</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblPayEnding" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblNetPay" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblPayType" runat="server"></asp:Label></td>
				</tr>
			</table>
			
			<table id="tblPayAmt">
				<tr class="EarnViewTableHeader">
					<td><p>Regular Pay</p></td>
					<td><p>Regular Hours</p></td>
					<td><p>Overtime Pay</p></td>
					<td><p>Overtime Hours</p></td>
					<td><p>WC/Cat Hours</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblRegPay" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblRegHours" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblOTPay" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblOTHours" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblWCHours" runat="server"></asp:Label></td>
				</tr>
			</table>
			
			<table id="tblPayWitholdings">
				<tr class="EarnViewTableHeader">
					<td><p>Gross Pay</p></td>
					<td><p>Federal Tax</p></td>
					<td><p>State Tax</p></td>
					<td><p>FICA</p></td>
					<td><p>Retirement</p></td>
					<td><p>Auto Use</p></td>
					<td><p>EIC</p></td>
					<td><p>Total Ded.</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblGross" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblFedTax" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblStateTax" runat="Server"></asp:Label></td>
					<td><asp:Label ID="lblFICA" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblRetirement" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblAutoUse" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblEIC" runat="Server"></asp:Label></td>
					<td><asp:Label ID="lblTotalDed" runat="server"></asp:Label></td>
				</tr>
			</table>
		</div>
		<br />
		<div id="Deductions">
			<h4>MISCELLANEOUS DEDUCTIONS/AMOUNTS</h4>
			<table>
				<tr class="EarnViewTableHeader">
					<td><p>Group Insurance</p></td>
					<td><p>Life Insurance</p></td>
					<td><p>Universal Life</p></td>
					<td><p>Cancer Insurance</p></td>
					<td><p>Personal Accident</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblGrpIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblLifeIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblUniversalIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblCancerIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblAccidentIns" runat="server"></asp:Label></td>
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>Auto Insurance</p></td>
					<td><p>Disability Insurance</p></td>
					<td><p>Dental Insurance</p></td>
					<td><p>Trans America</p></td>
					<td><p>Disability Income</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblAutoIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblDisabilityIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblDentalIns" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblTransAmerica" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblDisabilityIncome" runat="server"></asp:Label></td>
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>Unum Life</p></td>
					<td><p>Cigna</p></td>
					<td><p>Bonds</p></td>
					<td><p>Deferred Comp</p></td>
					<td><p>Long Term Care</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblUnum" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblCigna" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblBonds" runat="server"></asp:Label></td>
					<td><asp:label ID="lblDeferredComp" runat="server"></asp:label></td>
					<td><asp:Label ID="lblLongTerm" runat="server"></asp:Label></td>
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>Credit Union</p></td>
					<td><p>Military Retirement</p></td>
					<td><p>ASEA</p></td>
					<td><p>Health Bank</p></td>
					<td><p>Daycare</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblCreditUnion" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblMilitaryRet" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblASEA" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblHealthBank" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblDaycare" runat="server"></asp:Label></td>
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>United Way</p></td>
					<td><p>Vision Care</p></td>
					<td><p>Uniform Allowance</p></td>
					<td><p>Exp&nbsp; Allowance</p></td>
					<td ></td>
					
				</tr>
				<tr>
					<td style="height: 25px"><asp:Label ID="lblUnitedWay" runat="server"></asp:Label></td>
					<td style="height: 25px"><asp:Label ID="lblVisionCare" runat="server"></asp:Label></td>
					<td style="height: 25px"><asp:Label ID="lblUniAllowance" runat="server"></asp:Label></td>
					<td style="height: 25px"><asp:Label ID="lblToolAllowance" runat="server"></asp:Label></td>
					<td style="height: 25px"></td>
					
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>Child Support</p></td>
					<td><p>Bankruptcy</p></td>
					<td><p>Garnishment</p></td>
					<td><p>IRS Levy</p></td>
					<td></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblChildSupport" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblBankruptcy" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblGarnishment" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblIRS" runat="server"></asp:Label></td>
					<td></td>
				</tr>
			</table>
		</div>
		<br />
		<div id="YTD">
			<h4>YEAR TO DATE TOTALS</h4>
			<table id="tblYTD">
				<tr  class="EarnViewTableHeader">
					<td><p>Gross Pay</p></td>
					<td><p>Federal Tax</p></td>
					<td><p>State Tax</p></td>
					<td><p>FICA</p></td>
					<td><p>Retirement</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblGrossYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblFedYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblStateYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblFICAYTD" runat="server"></asp:Label></td>
					<td><asp:label id="lblRetirementYTD" runat="server"></asp:label></td>
				</tr>
				<tr class="EarnViewTableHeader">
					<td><p>Taxable Gross</p></td>
					<td><p>EIC</p></td>
					<td><p>Deferred Comp.</p></td>
					<td><p>United Way</p></td>
					<td><p>Long Term Care</p></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblTaxGrossYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblEICYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblDeferredCompYTD" runat="Server"></asp:Label></td>
					<td><asp:Label ID="lblUnitedWayYTD" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblLongTermYTD" runat="Server"></asp:Label></td>
				</tr>
			
			</table>	
		</div>
    </div>
    </form>
</body>
</html>
