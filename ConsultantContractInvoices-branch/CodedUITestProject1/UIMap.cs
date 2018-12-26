namespace CodedUITestProject1
{
	using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Windows.Input;
	using System.CodeDom.Compiler;
	using System.Text.RegularExpressions;
	using Microsoft.VisualStudio.TestTools.UITest.Extension;
	using Microsoft.VisualStudio.TestTools.UITesting;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
	using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
	using MouseButtons = System.Windows.Forms.MouseButtons;


	public partial class UIMap
	{

		/// <summary>
		/// Creates Contract and a few Supplementals to make sure things ain't broke.
		/// </summary>
		public void TestRun()
		{
			#region Variable Declarations
			BrowserWindow uINewContractMyAHTDWebWindow = this.UINewContractMyAHTDWebWindow;
			HtmlSpan uIContractsPane = this.UINewContractMyAHTDWebWindow.UIHomeMyAHTDWebAppDocument.UIContractsPane;
			HtmlHyperlink uINewContractHyperlink = this.UINewContractMyAHTDWebWindow.UIHomeMyAHTDWebAppDocument.UINewContractHyperlink;
			HtmlEdit uIJobNumberEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIJobNumberEdit;
			HtmlEdit uIConsultantEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIConsultantEdit;
			HtmlComboBox uIContractTypeComboBox = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContractTypeComboBox;
			HtmlComboBox uIContractStatusComboBox = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContractStatusComboBox;
			HtmlEdit uIAgreementDateEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIAgreementDateEdit;
			HtmlEdit uINoticeProceedDateEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UINoticeProceedDateEdit;
			HtmlComboBox uIAgreementTypeComboBox = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIAgreementTypeComboBox;
			HtmlComboBox uIResponsibleDivisionComboBox = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIResponsibleDivisionComboBox;
			HtmlEdit uIContractCeilingEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContractCeilingEdit;
			HtmlEdit uITitleIServicesCeilinEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UITitleIServicesCeilinEdit;
			HtmlEdit uITitleIFixedFeeMaxEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UITitleIFixedFeeMaxEdit;
			HtmlEdit uITitleIIServicesCeiliEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UITitleIIServicesCeiliEdit;
			HtmlEdit uITitleIIFixedFeeMaxEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UITitleIIFixedFeeMaxEdit;
			HtmlEdit uIHomeOfficeOverheadRaEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIHomeOfficeOverheadRaEdit;
			HtmlEdit uIFieldServiceOverheadEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIFieldServiceOverheadEdit;
			HtmlEdit uIFCCMEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIFCCMEdit;
			HtmlEdit uIMultiplierEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIMultiplierEdit;
			HtmlEdit uIScheduledCompletionDEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIScheduledCompletionDEdit;
			HtmlList uIItemList = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemList;
			HtmlDiv uIWorkTypesOtherEnviroPane = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIWorkTypesOtherEnviroPane;
			HtmlHyperlink uIAddHyperlink = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIAddHyperlink;
			HtmlEdit uIItemEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit;
			HtmlEdit uIItemEdit1 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit1;
			HtmlEdit uIItemEdit2 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit2;
			HtmlEdit uIItemEdit3 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit3;
			HtmlEdit uIItemEdit4 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit4;
			HtmlEdit uIItemEdit5 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit5;
			HtmlHyperlink uIAddHyperlink1 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIAddHyperlink1;
			HtmlEdit uIItemEdit6 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit6;
			HtmlEdit uIItemEdit7 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit7;
			HtmlEdit uIItemEdit8 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit8;
			HtmlEdit uIItemEdit9 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit9;
			HtmlEdit uIItemEdit10 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit10;
			HtmlEdit uIItemEdit11 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit11;
			HtmlHyperlink uIResetHyperlink = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIResetHyperlink;
			HtmlEdit uIItemEdit12 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit12;
			HtmlEdit uISelectSubConEdit = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UISelectSubConEdit;
			HtmlEdit uIItemEdit13 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit13;
			HtmlEdit uIItemEdit14 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit14;
			HtmlEdit uIItemEdit15 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit15;
			HtmlEdit uIItemEdit16 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit16;
			HtmlEdit uIItemEdit17 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit17;
			HtmlEdit uIItemEdit18 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit18;
			HtmlEdit uIItemEdit19 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit19;
			HtmlEdit uIItemEdit20 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit20;
			HtmlEdit uIItemEdit21 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit21;
			HtmlHyperlink uIAddHyperlink2 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIAddHyperlink2;
			HtmlEdit uIItemEdit22 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit22;
			HtmlEdit uIItemEdit23 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit23;
			HtmlEdit uIItemEdit24 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit24;
			HtmlEdit uIItemEdit25 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit25;
			HtmlEdit uIItemEdit26 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit26;
			HtmlEdit uIItemEdit27 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit27;
			HtmlHyperlink uIAddHyperlink3 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIAddHyperlink3;
			HtmlEdit uIItemEdit28 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit28;
			HtmlEdit uIItemEdit29 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit29;
			HtmlEdit uIItemEdit30 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit30;
			HtmlEdit uIItemEdit31 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit31;
			HtmlEdit uIItemEdit32 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit32;
			HtmlEdit uIItemEdit33 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit33;
			HtmlTextArea uIItemEdit34 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UIItemEdit34;
			HtmlInputButton uISubmitButton1 = this.UINewContractMyAHTDWebWindow.UINewContractMyAHTDWebDocument.UIContentCustom.UISubmitButton1;
			HtmlDiv uITitleIITitleIIServicPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane.UITitleIITitleIIServicPane;
			HtmlDiv uITitleITitleIServicesPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane.UITitleITitleIServicesPane;
			HtmlDiv uIContractCode2364JobNPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane.UIContractCode2364JobNPane;
			HtmlDiv uISubConsultantsTHEMEHPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISubConsultantsTHEMEHPane;
			HtmlDiv uIRecentInvoicesInvoicPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UIRecentInvoicesInvoicPane;
			HtmlDiv uISupplementalsNoneAddPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISupplementalsNoneAddPane;
			HtmlHyperlink uIAddSupplementalHyperlink = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UINoneAddSupplementalPane.UIAddSupplementalHyperlink;
			HtmlEdit uIContractCeilingEdit1 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContractCeilingEdit;
			HtmlEdit uIT1SvcsCeilingEdit = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIT1SvcsCeilingEdit;
			HtmlEdit uIItemEdit35 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit;
			HtmlEdit uIItemEdit110 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit1;
			HtmlEdit uIItemEdit210 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit2;
			HtmlCheckBox uIItemCheckBox = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemCheckBox;
			HtmlTextArea uIItemEdit36 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit3;
			HtmlInputButton uISubmitButton = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UISubmitButton;
			HtmlEdit uISuppAgreementDateEdit = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UISuppAgreementDateEdit;
			HtmlDiv uIContractIndirectCostPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane.UIContractIndirectCostPane;
			HtmlDiv uIContractInfoPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane;
			HtmlDiv uITitleITitleIServicesPane1 = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContractInfoPane.UITitleITitleIServicesPane1;
			HtmlDiv uISalaryRatesServiceNaPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISalaryRatesServiceNaPane;
			HtmlDiv uISupplementals1SupplePane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISupplementals1SupplePane;
			HtmlDiv uIUiid18Pane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIUiid18Pane;
			HtmlHyperlink uIAddSupplementalHyperlink1 = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIUiid18Pane.UIAddSupplementalHyperlink;
			HtmlEdit uISelectSubConEdit1 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UISelectSubConEdit;
			HtmlEdit uIItemEdit41 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit4;
			HtmlEdit uIItemEdit111 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit11;
			HtmlEdit uIItemEdit211 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit21;
			HtmlEdit uIItemEdit311 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit31;
			HtmlEdit uIItemEdit411 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit41;
			HtmlEdit uIItemEdit51 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit5;
			HtmlEdit uIItemEdit61 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit6;
			HtmlEdit uIItemEdit71 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit7;
			HtmlEdit uIItemEdit81 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit8;
			HtmlHyperlink uIAddHyperlink4 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIAddHyperlink;
			HtmlEdit uIItemEdit91 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit9;
			HtmlEdit uIItemEdit101 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit10;
			HtmlEdit uIItemEdit1111 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit111;
			HtmlEdit uIItemEdit121 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit12;
			HtmlEdit uIItemEdit131 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit13;
			HtmlEdit uIItemEdit141 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit14;
			HtmlHyperlink uIAddHyperlink11 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIAddHyperlink1;
			HtmlEdit uIItemEdit151 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit15;
			HtmlEdit uIItemEdit161 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit16;
			HtmlEdit uIItemEdit171 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit17;
			HtmlLabel uIRemoveLabel = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIRemoveLabel;
			HtmlHyperlink uIAddHyperlink21 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIAddHyperlink2;
			HtmlEdit uIItemEdit181 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit18;
			HtmlEdit uIItemEdit191 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit19;
			HtmlEdit uIItemEdit201 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit20;
			HtmlEdit uIItemEdit221 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit22;
			HtmlEdit uIItemEdit231 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UIItemEdit23;
			HtmlInputButton uISubmitButton11 = this.UINewContractMyAHTDWebWindow.UIAddSupplementalMyAHTDocument.UIContentCustom.UISubmitButton1;
			HtmlDiv uISupplementals12SupplPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISupplementals12SupplPane;
			HtmlCell uIItem111111Cell = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UIItemTable.UIItem111111Cell;
			HtmlDiv uISubConsultantsHARTMAPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom.UISubConsultantsHARTMAPane;
			HtmlCustom uIContentCustom = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIContentCustom;
			HtmlSpan uIItemPane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIUiid33Custom.UIItemPane;
			HtmlCustom uITHEMEHLBURGERFIRMCustom = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UIUiid34Pane.UITHEMEHLBURGERFIRMCustom;
			HtmlDiv uISubCon9Pane = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UISubCon9Pane;
			HtmlHyperlink uITHEMEHLBURGERFIRMHyperlink = this.UINewContractMyAHTDWebWindow.UIDetailsMyAHTDWebAppDocument.UITHEMEHLBURGERFIRMHyperlink;
			#endregion

			// Go to web page 'http://localhost:61349/ConsultantContracts/'
			uINewContractMyAHTDWebWindow.NavigateToUrl(new System.Uri(this.TestRunParams.UINewContractMyAHTDWebWindowUrl));

			// Click 'Contracts ▾' pane
			Mouse.Click(uIContractsPane, new Point(60, 19));

			// Click 'New Contract' link
			Mouse.Click(uINewContractHyperlink, new Point(41, 5));

			// Type '01' in 'Job Number' text box
			uIJobNumberEdit.Text = this.TestRunParams.UIJobNumberEditText;

			// Type '012014' in 'Job Number' text box
			uIJobNumberEdit.Text = this.TestRunParams.UIJobNumberEditText1;

			// Type '{Tab}' in 'Job Number' text box
			Keyboard.SendKeys(uIJobNumberEdit, this.TestRunParams.UIJobNumberEditSendKeys, ModifierKeys.None);

			// Type 'garver llc' in 'Consultant' text box
			uIConsultantEdit.Text = this.TestRunParams.UIConsultantEditText;

			// Type '{Tab}' in 'Consultant' text box
			Keyboard.SendKeys(uIConsultantEdit, this.TestRunParams.UIConsultantEditSendKeys, ModifierKeys.None);

			// Select 'Cost plus' in 'Contract Type' combo box
			uIContractTypeComboBox.SelectedItem = this.TestRunParams.UIContractTypeComboBoxSelectedItem;

			// Select 'A' in 'Contract Status' combo box
			uIContractStatusComboBox.SelectedItem = this.TestRunParams.UIContractStatusComboBoxSelectedItem;

			// Type '{Enter}{Tab}' in 'Agreement Date' text box
			Keyboard.SendKeys(uIAgreementDateEdit, this.TestRunParams.UIAgreementDateEditSendKeys, ModifierKeys.None);

			// Type '{Enter}{Tab}' in 'Notice Proceed Date' text box
			Keyboard.SendKeys(uINoticeProceedDateEdit, this.TestRunParams.UINoticeProceedDateEditSendKeys, ModifierKeys.None);

			// Select 'City/County Agreement' in 'Agreement Type' combo box
			uIAgreementTypeComboBox.SelectedItem = this.TestRunParams.UIAgreementTypeComboBoxSelectedItem;

			// Select 'Env.' in 'Responsible Division' combo box
			uIResponsibleDivisionComboBox.SelectedItem = this.TestRunParams.UIResponsibleDivisionComboBoxSelectedItem;

			// Type '' in 'Contract Ceiling' text box
			uIContractCeilingEdit.Text = this.TestRunParams.UIContractCeilingEditText;

			// Type '100000' in 'Contract Ceiling' text box
			uIContractCeilingEdit.Text = this.TestRunParams.UIContractCeilingEditText1;

			// Type '{Tab}' in 'Contract Ceiling' text box
			Keyboard.SendKeys(uIContractCeilingEdit, this.TestRunParams.UIContractCeilingEditSendKeys, ModifierKeys.None);

			// Type '90000' in 'Title I Services Ceiling' text box
			uITitleIServicesCeilinEdit.Text = this.TestRunParams.UITitleIServicesCeilinEditText;

			// Type '{Tab}' in 'Title I Services Ceiling' text box
			Keyboard.SendKeys(uITitleIServicesCeilinEdit, this.TestRunParams.UITitleIServicesCeilinEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Title I Fixed Fee Max' text box
			Keyboard.SendKeys(uITitleIFixedFeeMaxEdit, this.TestRunParams.UITitleIFixedFeeMaxEditSendKeys, ModifierKeys.None);

			// Type '10000' in 'Title II Services Ceiling' text box
			uITitleIIServicesCeiliEdit.Text = this.TestRunParams.UITitleIIServicesCeiliEditText;

			// Type '{Tab}' in 'Title II Services Ceiling' text box
			Keyboard.SendKeys(uITitleIIServicesCeiliEdit, this.TestRunParams.UITitleIIServicesCeiliEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Title II Fixed Fee Max' text box
			Keyboard.SendKeys(uITitleIIFixedFeeMaxEdit, this.TestRunParams.UITitleIIFixedFeeMaxEditSendKeys, ModifierKeys.None);

			// Type '10' in 'Home Office Overhead Rate Max' text box
			uIHomeOfficeOverheadRaEdit.Text = this.TestRunParams.UIHomeOfficeOverheadRaEditText;

			// Type '{Tab}' in 'Home Office Overhead Rate Max' text box
			Keyboard.SendKeys(uIHomeOfficeOverheadRaEdit, this.TestRunParams.UIHomeOfficeOverheadRaEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Field Service Overhead Rate Max' text box
			Keyboard.SendKeys(uIFieldServiceOverheadEdit, this.TestRunParams.UIFieldServiceOverheadEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'FCCM' text box
			Keyboard.SendKeys(uIFCCMEdit, this.TestRunParams.UIFCCMEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Multiplier' text box
			Keyboard.SendKeys(uIMultiplierEdit, this.TestRunParams.UIMultiplierEditSendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Scheduled Completion Date' text box
			Keyboard.SendKeys(uIScheduledCompletionDEdit, this.TestRunParams.UIScheduledCompletionDEditSendKeys, ModifierKeys.None);

			// Type 'Shift + {Tab}' in list box
			Keyboard.SendKeys(uIItemList, this.TestRunParams.UIItemListSendKeys, ModifierKeys.Shift);

			// Type '{Enter}{Tab}' in 'Scheduled Completion Date' text box
			Keyboard.SendKeys(uIScheduledCompletionDEdit, this.TestRunParams.UIScheduledCompletionDEditSendKeys1, ModifierKeys.None);

			// Select 'Construction' in list box
			uIItemList.SelectedItemsAsString = this.TestRunParams.UIItemListSelectedItemsAsString;

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Work Types OtherEnvironmentalPlanningSu' pane at (1, 1)
			Mouse.Hover(uIWorkTypesOtherEnviroPane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink, new Point(16, 3));

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink, new Point(14, 4));

			// Type 'River Builder' in text box
			uIItemEdit.Text = this.TestRunParams.UIItemEditText;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit, this.TestRunParams.UIItemEditSendKeys, ModifierKeys.None);

			// Type '10' in text box
			uIItemEdit1.Text = this.TestRunParams.UIItemEdit1Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit1, this.TestRunParams.UIItemEdit1SendKeys, ModifierKeys.None);

			// Type '10' in text box
			uIItemEdit2.Text = this.TestRunParams.UIItemEdit2Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit2, this.TestRunParams.UIItemEdit2SendKeys, ModifierKeys.None);

			// Type 'Supervisor' in text box
			uIItemEdit3.Text = this.TestRunParams.UIItemEdit3Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit3, this.TestRunParams.UIItemEdit3SendKeys, ModifierKeys.None);

			// Type '20' in text box
			uIItemEdit4.Text = this.TestRunParams.UIItemEdit4Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit4, this.TestRunParams.UIItemEdit4SendKeys, ModifierKeys.None);

			// Type '20' in text box
			uIItemEdit5.Text = this.TestRunParams.UIItemEdit5Text;

			// Double-Click '+Add' link
			Mouse.DoubleClick(uIAddHyperlink1, new Point(16, 3));

			// Type 'Water Import' in text box
			uIItemEdit6.Text = this.TestRunParams.UIItemEdit6Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit6, this.TestRunParams.UIItemEdit6SendKeys, ModifierKeys.None);

			// Type '45' in text box
			uIItemEdit7.Text = this.TestRunParams.UIItemEdit7Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit7, this.TestRunParams.UIItemEdit7SendKeys, ModifierKeys.None);

			// Type '45' in text box
			uIItemEdit8.Text = this.TestRunParams.UIItemEdit8Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit8, this.TestRunParams.UIItemEdit8SendKeys, ModifierKeys.None);

			// Type 'Soil Excavation' in text box
			uIItemEdit9.Text = this.TestRunParams.UIItemEdit9Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit9, this.TestRunParams.UIItemEdit9SendKeys, ModifierKeys.None);

			// Type '75' in text box
			uIItemEdit10.Text = this.TestRunParams.UIItemEdit10Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit10, this.TestRunParams.UIItemEdit10SendKeys, ModifierKeys.None);

			// Type '85' in text box
			uIItemEdit11.Text = this.TestRunParams.UIItemEdit11Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit11, this.TestRunParams.UIItemEdit11SendKeys, ModifierKeys.None);

			// Type '{Tab}' in 'Reset' link
			Keyboard.SendKeys(uIResetHyperlink, this.TestRunParams.UIResetHyperlinkSendKeys, ModifierKeys.None);

			// Type '1' in text box
			uIItemEdit12.Text = this.TestRunParams.UIItemEdit12Text;

			// Type '710389310' in 'selectSubCon' text box
			uISelectSubConEdit.Text = this.TestRunParams.UISelectSubConEditText;

			// Wait for 1 seconds for user delay between actions; Type '{Tab}' in 'selectSubCon' text box
			Playback.Wait(1000);
			Keyboard.SendKeys(uISelectSubConEdit, this.TestRunParams.UISelectSubConEditSendKeys, ModifierKeys.None);

			// Type '10000' in text box
			uIItemEdit13.Text = this.TestRunParams.UIItemEdit13Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit13, this.TestRunParams.UIItemEdit13SendKeys, ModifierKeys.None);

			// Type '5000' in text box
			uIItemEdit14.Text = this.TestRunParams.UIItemEdit14Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit14, this.TestRunParams.UIItemEdit14SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit15, this.TestRunParams.UIItemEdit15SendKeys, ModifierKeys.None);

			// Type '5000' in text box
			uIItemEdit16.Text = this.TestRunParams.UIItemEdit16Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit16, this.TestRunParams.UIItemEdit16SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit17, this.TestRunParams.UIItemEdit17SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit18, this.TestRunParams.UIItemEdit18SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit19, this.TestRunParams.UIItemEdit19SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit20, this.TestRunParams.UIItemEdit20SendKeys, ModifierKeys.None);

			// Type '1' in text box
			uIItemEdit21.Text = this.TestRunParams.UIItemEdit21Text;

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink2, new Point(18, 6));

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink2, new Point(18, 6));

			// Type 'Burger Flipper' in text box
			uIItemEdit22.Text = this.TestRunParams.UIItemEdit22Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit22, this.TestRunParams.UIItemEdit22SendKeys, ModifierKeys.None);

			// Type '8.5' in text box
			uIItemEdit23.Text = this.TestRunParams.UIItemEdit23Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit23, this.TestRunParams.UIItemEdit23SendKeys, ModifierKeys.None);

			// Type '10' in text box
			uIItemEdit24.Text = this.TestRunParams.UIItemEdit24Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit24, this.TestRunParams.UIItemEdit24SendKeys, ModifierKeys.None);

			// Type 'Grease Jockey' in text box
			uIItemEdit25.Text = this.TestRunParams.UIItemEdit25Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit25, this.TestRunParams.UIItemEdit25SendKeys, ModifierKeys.None);

			// Type '10' in text box
			uIItemEdit26.Text = this.TestRunParams.UIItemEdit26Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit26, this.TestRunParams.UIItemEdit26SendKeys, ModifierKeys.None);

			// Type '10' in text box
			uIItemEdit27.Text = this.TestRunParams.UIItemEdit27Text;

			// Double-Click '+Add' link
			Mouse.DoubleClick(uIAddHyperlink3, new Point(14, 8));

			// Type 'Delivery' in text box
			uIItemEdit28.Text = this.TestRunParams.UIItemEdit28Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit28, this.TestRunParams.UIItemEdit28SendKeys, ModifierKeys.None);

			// Type '15' in text box
			uIItemEdit29.Text = this.TestRunParams.UIItemEdit29Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit29, this.TestRunParams.UIItemEdit29SendKeys, ModifierKeys.None);

			// Type '20' in text box
			uIItemEdit30.Text = this.TestRunParams.UIItemEdit30Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit30, this.TestRunParams.UIItemEdit30SendKeys, ModifierKeys.None);

			// Type 'Ordering those little toys in the happy meals' in text box
			uIItemEdit31.Text = this.TestRunParams.UIItemEdit31Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit31, this.TestRunParams.UIItemEdit31SendKeys, ModifierKeys.None);

			// Type '5' in text box
			uIItemEdit32.Text = this.TestRunParams.UIItemEdit32Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit32, this.TestRunParams.UIItemEdit32SendKeys, ModifierKeys.None);

			// Type '6' in text box
			uIItemEdit33.Text = this.TestRunParams.UIItemEdit33Text;

			// Type 'Autogenerated test data' in text box
			uIItemEdit34.Text = this.TestRunParams.UIItemEdit34Text;

			// Click 'Submit' button
			Mouse.Click(uISubmitButton1, new Point(34, 16));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Title II Title II Services Ceiling: $' pane at (1, 1)
			Mouse.Hover(uITitleIITitleIIServicPane, new Point(1, 1));

			// Mouse hover 'Title I Title I Services Ceiling: $90' pane at (1, 1)
			Mouse.Hover(uITitleITitleIServicesPane, new Point(1, 1));

			// Mouse hover 'Contract Code: 2364 JobNo: 012014' pane at (1, 1)
			Mouse.Hover(uIContractCode2364JobNPane, new Point(1, 1));

			// Mouse hover 'Sub Consultants THE MEHLBURGER FIRM' pane at (1, 1)
			Mouse.Hover(uISubConsultantsTHEMEHPane, new Point(1, 1));

			// Mouse hover 'Recent Invoices Invoice Date Inv' pane at (1, 1)
			Mouse.Hover(uIRecentInvoicesInvoicPane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click 'Supplementals None Add Supplemental' pane
			Mouse.Click(uISupplementalsNoneAddPane, new Point(76, 15));

			// Click 'Add Supplemental' link
			Mouse.Click(uIAddSupplementalHyperlink, new Point(60, 14));

			// Type '90000' in 'ContractCeiling' text box
			uIContractCeilingEdit1.Text = this.TestRunParams.UIContractCeilingEditText2;

			// Type '{Tab}' in 'ContractCeiling' text box
			Keyboard.SendKeys(uIContractCeilingEdit1, this.TestRunParams.UIContractCeilingEditSendKeys1, ModifierKeys.None);

			// Type '80000' in 'T1SvcsCeiling' text box
			uIT1SvcsCeilingEdit.Text = this.TestRunParams.UIT1SvcsCeilingEditText;

			// Type '{Tab}' in 'T1SvcsCeiling' text box
			Keyboard.SendKeys(uIT1SvcsCeilingEdit, this.TestRunParams.UIT1SvcsCeilingEditSendKeys, ModifierKeys.None);

			// Type '25' in text box
			uIItemEdit35.Text = this.TestRunParams.UIItemEditText1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit35, this.TestRunParams.UIItemEditSendKeys1, ModifierKeys.None);

			// Type '25' in text box
			uIItemEdit110.Text = this.TestRunParams.UIItemEdit1Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit110, this.TestRunParams.UIItemEdit1SendKeys1, ModifierKeys.None);

			// Type '50' in text box
			uIItemEdit210.Text = this.TestRunParams.UIItemEdit2Text1;

			// Select check box
			uIItemCheckBox.Checked = this.TestRunParams.UIItemCheckBoxChecked;

			// Type 'Autogenerated Supplemental Prime ' in text box
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text1;

			// Type 'Autogenerated Supplemental Prime - ' in text box
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text2;

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000
			//'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text3;

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000
			//Salaries increased
			//Soil Excavation Service removed'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text4;

			// Click 'Submit' button
			Mouse.Click(uISubmitButton, new Point(25, 10));

			// Click 'SuppAgreementDate' text box
			Mouse.Click(uISuppAgreementDateEdit, new Point(73, 12));

			// Type '{Enter}' in 'SuppAgreementDate' text box
			Keyboard.SendKeys(uISuppAgreementDateEdit, this.TestRunParams.UISuppAgreementDateEditSendKeys, ModifierKeys.None);

			// Click 'Submit' button
			Mouse.Click(uISubmitButton, new Point(34, 16));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Contract Indirect Cost Rate Home Offi' pane at (1, 1)
			Mouse.Hover(uIContractIndirectCostPane, new Point(1, 1));

			// Mouse hover 'contractInfo' pane at (1, 1)
			Mouse.Hover(uIContractInfoPane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click 'Title I Title I Services Ceiling: $80' pane
			Mouse.Click(uITitleITitleIServicesPane1, new Point(59, 16));

			// Click 'Title I Title I Services Ceiling: $80' pane
			Mouse.Click(uITitleITitleIServicesPane1, new Point(59, 16));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Salary Rates Service Name Min Ra' pane at (1, 1)
			Mouse.Hover(uISalaryRatesServiceNaPane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click 'Supplementals 1 Supplemental 1' pane
			Mouse.Click(uISupplementals1SupplePane, new Point(56, 12));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'ui-id-18' pane at (1, 1)
			Mouse.Hover(uIUiid18Pane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click 'Add Supplemental' link
			Mouse.Click(uIAddSupplementalHyperlink1, new Point(108, 19));

			// Type 'ha' in 'selectSubCon' text box
			uISelectSubConEdit1.Text = this.TestRunParams.UISelectSubConEditText1;

			// Type '{Enter}' in 'selectSubCon' text box
			Keyboard.SendKeys(uISelectSubConEdit1, this.TestRunParams.UISelectSubConEditSendKeys1, ModifierKeys.None);

			// Type '20000' in text box
			uIItemEdit41.Text = this.TestRunParams.UIItemEdit4Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit41, this.TestRunParams.UIItemEdit4SendKeys1, ModifierKeys.None);

			// Type '15000' in text box
			uIItemEdit111.Text = this.TestRunParams.UIItemEdit11Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit111, this.TestRunParams.UIItemEdit11SendKeys1, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit211, this.TestRunParams.UIItemEdit21SendKeys, ModifierKeys.None);

			// Type '5000' in text box
			uIItemEdit311.Text = this.TestRunParams.UIItemEdit31Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit311, this.TestRunParams.UIItemEdit31SendKeys1, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit411, this.TestRunParams.UIItemEdit41SendKeys, ModifierKeys.None);

			// Type '1' in text box
			uIItemEdit51.Text = this.TestRunParams.UIItemEdit5Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit51, this.TestRunParams.UIItemEdit5SendKeys, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit61, this.TestRunParams.UIItemEdit6SendKeys1, ModifierKeys.None);

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit71, this.TestRunParams.UIItemEdit7SendKeys1, ModifierKeys.None);

			// Type '1' in text box
			uIItemEdit81.Text = this.TestRunParams.UIItemEdit8Text1;

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink4, new Point(19, 9));

			// Type 'Engineerer' in text box
			uIItemEdit91.Text = this.TestRunParams.UIItemEdit9Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit91, this.TestRunParams.UIItemEdit9SendKeys1, ModifierKeys.None);

			// Type '13.37' in text box
			uIItemEdit101.Text = this.TestRunParams.UIItemEdit10Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit101, this.TestRunParams.UIItemEdit10SendKeys1, ModifierKeys.None);

			// Type '350' in text box
			uIItemEdit1111.Text = this.TestRunParams.UIItemEdit111Text;

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink4, new Point(16, 5));

			// Type 'Secretary' in text box
			uIItemEdit121.Text = this.TestRunParams.UIItemEdit12Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit121, this.TestRunParams.UIItemEdit12SendKeys, ModifierKeys.None);

			// Type '15' in text box
			uIItemEdit131.Text = this.TestRunParams.UIItemEdit13Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit131, this.TestRunParams.UIItemEdit13SendKeys1, ModifierKeys.None);

			// Type '15' in text box
			uIItemEdit141.Text = this.TestRunParams.UIItemEdit14Text1;

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink11, new Point(18, 9));

			// Type 'Consult' in text box
			uIItemEdit151.Text = this.TestRunParams.UIItemEdit15Text;

			// Type 'Consultin'' in text box
			uIItemEdit151.Text = this.TestRunParams.UIItemEdit15Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit151, this.TestRunParams.UIItemEdit15SendKeys1, ModifierKeys.None);

			// Type '200' in text box
			uIItemEdit161.Text = this.TestRunParams.UIItemEdit16Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit161, this.TestRunParams.UIItemEdit16SendKeys1, ModifierKeys.None);

			// Type '300' in text box
			uIItemEdit171.Text = this.TestRunParams.UIItemEdit17Text;

			// Click 'Remove' label
			Mouse.Click(uIRemoveLabel, new Point(23, 14));

			// Click '+Add' link
			Mouse.Click(uIAddHyperlink21, new Point(20, 9));

			// Type 'Non-binary gender assignment' in text box
			uIItemEdit181.Text = this.TestRunParams.UIItemEdit18Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit181, this.TestRunParams.UIItemEdit18SendKeys1, ModifierKeys.None);

			// Type 'Shift + {Tab}' in text box
			Keyboard.SendKeys(uIItemEdit191, this.TestRunParams.UIItemEdit19SendKeys1, ModifierKeys.Shift);

			// Type 'More burgers' in text box
			uIItemEdit181.Text = this.TestRunParams.UIItemEdit18Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit181, this.TestRunParams.UIItemEdit18SendKeys2, ModifierKeys.None);

			// Type '0' in text box
			uIItemEdit191.Text = this.TestRunParams.UIItemEdit19Text;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit191, this.TestRunParams.UIItemEdit19SendKeys2, ModifierKeys.None);

			// Type '25' in text box
			uIItemEdit201.Text = this.TestRunParams.UIItemEdit20Text;

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental - Sub Consultants
			//Removed MehlBurger's Grease Jockey Salary
			//Added More Burgers Service'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text5;

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental - Sub Consultants
			//Removed MehlBurger's Grease Jockey Salary
			//Added More Burgers Service
			//Added new SubCon'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text6;

			// Type '6000' in text box
			uIItemEdit221.Text = this.TestRunParams.UIItemEdit22Text1;

			// Type '4000' in text box
			uIItemEdit231.Text = this.TestRunParams.UIItemEdit23Text1;

			// Type '{Tab}' in text box
			Keyboard.SendKeys(uIItemEdit231, this.TestRunParams.UIItemEdit23SendKeys1, ModifierKeys.None);

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental - Sub Consultants
			//Edited Mehlburger's T1/T2 Ceilings to be 6000/4000
			//Removed MehlBurger's Grease Jockey Salary
			//Added More Burgers Service
			//Added new SubCon'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text7;

			// Type the following paragraph in text box 
			//'Autogenerated Supplemental - Sub Consultants
			//Edited Mehlburger's T1/T2 Ceilings to be 6000/4000
			//Removed MehlBurger's Grease Jockey Salary
			//Added More Burgers Service
			//Added new SubCon - Hartman Engineering'
			uIItemEdit36.Text = this.TestRunParams.UIItemEdit3Text8;

			// Click 'Submit' button
			Mouse.Click(uISubmitButton11, new Point(36, 18));

			// Click 'SuppAgreementDate' text box
			Mouse.Click(uISuppAgreementDateEdit, new Point(54, 12));

			// Type '{Enter}' in 'SuppAgreementDate' text box
			Keyboard.SendKeys(uISuppAgreementDateEdit, this.TestRunParams.UISuppAgreementDateEditSendKeys1, ModifierKeys.None);

			// Click 'Submit' button
			Mouse.Click(uISubmitButton11, new Point(34, 12));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Contract Indirect Cost Rate Home Offi' pane at (1, 1)
			Mouse.Hover(uIContractIndirectCostPane, new Point(1, 1));

			// Mouse hover 'contractInfo' pane at (1, 1)
			Mouse.Hover(uIContractInfoPane, new Point(1, 1));

			// Mouse hover 'Supplementals 1 2 Supplemental' pane at (1, 1)
			Mouse.Hover(uISupplementals12SupplPane, new Point(1, 1));

			// Mouse hover '1/1/1111' cell at (1, 1)
			Mouse.Hover(uIItem111111Cell, new Point(1, 1));

			// Mouse hover 'Sub Consultants HARTMAN ENGINEERING T' pane at (1, 1)
			Mouse.Hover(uISubConsultantsHARTMAPane, new Point(1, 1));

			// Mouse hover 'content' custom control at (1, 1)
			Mouse.Hover(uIContentCustom, new Point(1, 1));

			// Mouse hover 'Sub Consultants HARTMAN ENGINEERING T' pane at (1, 1)
			Mouse.Hover(uISubConsultantsHARTMAPane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click pane
			Mouse.Click(uIItemPane, new Point(8, 5));

			// Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
			Playback.PlaybackSettings.ContinueOnError = true;

			// Mouse hover 'Sub Consultants HARTMAN ENGINEERING T' pane at (1, 1)
			Mouse.Hover(uISubConsultantsHARTMAPane, new Point(1, 1));

			// Mouse hover 'THE MEHLBURGER FIRM' custom control at (1, 1)
			Mouse.Hover(uITHEMEHLBURGERFIRMCustom, new Point(1, 1));

			// Mouse hover 'SubCon9' pane at (1, 1)
			Mouse.Hover(uISubCon9Pane, new Point(1, 1));

			// Reset flag to ensure that play back stops if there is an error.
			Playback.PlaybackSettings.ContinueOnError = false;

			// Click 'THE MEHLBURGER FIRM' link
			Mouse.Click(uITHEMEHLBURGERFIRMHyperlink, new Point(60, 27));
		}

		public virtual TestRunParams TestRunParams
		{
			get
			{
				if ((this.mTestRunParams == null))
				{
					this.mTestRunParams = new TestRunParams();
				}
				return this.mTestRunParams;
			}
		}

		private TestRunParams mTestRunParams;
	}
	/// <summary>
	/// Parameters to be passed into 'TestRun'
	/// </summary>
	[GeneratedCode("Coded UITest Builder", "11.0.60315.1")]
	public class TestRunParams
	{

		#region Fields
		/// <summary>
		/// Go to web page 'http://localhost:61349/ConsultantContracts/'
		/// </summary>
		public string UINewContractMyAHTDWebWindowUrl = "http://localhost:61349/ConsultantContracts/";

		/// <summary>
		/// Type '01' in 'Job Number' text box
		/// </summary>
		public string UIJobNumberEditText = "01";

		/// <summary>
		/// Type '012014' in 'Job Number' text box
		/// </summary>
		public string UIJobNumberEditText1 = "012014";

		/// <summary>
		/// Type '{Tab}' in 'Job Number' text box
		/// </summary>
		public string UIJobNumberEditSendKeys = "{Tab}";

		/// <summary>
		/// Type 'garver llc' in 'Consultant' text box
		/// </summary>
		public string UIConsultantEditText = "garver llc";

		/// <summary>
		/// Type '{Tab}' in 'Consultant' text box
		/// </summary>
		public string UIConsultantEditSendKeys = "{Tab}";

		/// <summary>
		/// Select 'Cost plus' in 'Contract Type' combo box
		/// </summary>
		public string UIContractTypeComboBoxSelectedItem = "Cost plus";

		/// <summary>
		/// Select 'A' in 'Contract Status' combo box
		/// </summary>
		public string UIContractStatusComboBoxSelectedItem = "A";

		/// <summary>
		/// Type '{Enter}{Tab}' in 'Agreement Date' text box
		/// </summary>
		public string UIAgreementDateEditSendKeys = "{Enter}{Tab}";

		/// <summary>
		/// Type '{Enter}{Tab}' in 'Notice Proceed Date' text box
		/// </summary>
		public string UINoticeProceedDateEditSendKeys = "{Enter}{Tab}";

		/// <summary>
		/// Select 'City/County Agreement' in 'Agreement Type' combo box
		/// </summary>
		public string UIAgreementTypeComboBoxSelectedItem = "City/County Agreement";

		/// <summary>
		/// Select 'Env.' in 'Responsible Division' combo box
		/// </summary>
		public string UIResponsibleDivisionComboBoxSelectedItem = "Env.";

		/// <summary>
		/// Type '' in 'Contract Ceiling' text box
		/// </summary>
		public string UIContractCeilingEditText = "";

		/// <summary>
		/// Type '100000' in 'Contract Ceiling' text box
		/// </summary>
		public string UIContractCeilingEditText1 = "100000";

		/// <summary>
		/// Type '{Tab}' in 'Contract Ceiling' text box
		/// </summary>
		public string UIContractCeilingEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '90000' in 'Title I Services Ceiling' text box
		/// </summary>
		public string UITitleIServicesCeilinEditText = "90000";

		/// <summary>
		/// Type '{Tab}' in 'Title I Services Ceiling' text box
		/// </summary>
		public string UITitleIServicesCeilinEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Title I Fixed Fee Max' text box
		/// </summary>
		public string UITitleIFixedFeeMaxEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '10000' in 'Title II Services Ceiling' text box
		/// </summary>
		public string UITitleIIServicesCeiliEditText = "10000";

		/// <summary>
		/// Type '{Tab}' in 'Title II Services Ceiling' text box
		/// </summary>
		public string UITitleIIServicesCeiliEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Title II Fixed Fee Max' text box
		/// </summary>
		public string UITitleIIFixedFeeMaxEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in 'Home Office Overhead Rate Max' text box
		/// </summary>
		public string UIHomeOfficeOverheadRaEditText = "10";

		/// <summary>
		/// Type '{Tab}' in 'Home Office Overhead Rate Max' text box
		/// </summary>
		public string UIHomeOfficeOverheadRaEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Field Service Overhead Rate Max' text box
		/// </summary>
		public string UIFieldServiceOverheadEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'FCCM' text box
		/// </summary>
		public string UIFCCMEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Multiplier' text box
		/// </summary>
		public string UIMultiplierEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Scheduled Completion Date' text box
		/// </summary>
		public string UIScheduledCompletionDEditSendKeys = "{Tab}";

		/// <summary>
		/// Type 'Shift + {Tab}' in list box
		/// </summary>
		public string UIItemListSendKeys = "{Tab}";

		/// <summary>
		/// Type '{Enter}{Tab}' in 'Scheduled Completion Date' text box
		/// </summary>
		public string UIScheduledCompletionDEditSendKeys1 = "{Enter}{Tab}";

		/// <summary>
		/// Select 'Construction' in list box
		/// </summary>
		public string UIItemListSelectedItemsAsString = "Construction";

		/// <summary>
		/// Type 'River Builder' in text box
		/// </summary>
		public string UIItemEditText = "River Builder";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in text box
		/// </summary>
		public string UIItemEdit1Text = "10";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit1SendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in text box
		/// </summary>
		public string UIItemEdit2Text = "10";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit2SendKeys = "{Tab}";

		/// <summary>
		/// Type 'Supervisor' in text box
		/// </summary>
		public string UIItemEdit3Text = "Supervisor";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit3SendKeys = "{Tab}";

		/// <summary>
		/// Type '20' in text box
		/// </summary>
		public string UIItemEdit4Text = "20";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit4SendKeys = "{Tab}";

		/// <summary>
		/// Type '20' in text box
		/// </summary>
		public string UIItemEdit5Text = "20";

		/// <summary>
		/// Type 'Water Import' in text box
		/// </summary>
		public string UIItemEdit6Text = "Water Import";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit6SendKeys = "{Tab}";

		/// <summary>
		/// Type '45' in text box
		/// </summary>
		public string UIItemEdit7Text = "45";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit7SendKeys = "{Tab}";

		/// <summary>
		/// Type '45' in text box
		/// </summary>
		public string UIItemEdit8Text = "45";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit8SendKeys = "{Tab}";

		/// <summary>
		/// Type 'Soil Excavation' in text box
		/// </summary>
		public string UIItemEdit9Text = "Soil Excavation";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit9SendKeys = "{Tab}";

		/// <summary>
		/// Type '75' in text box
		/// </summary>
		public string UIItemEdit10Text = "75";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit10SendKeys = "{Tab}";

		/// <summary>
		/// Type '85' in text box
		/// </summary>
		public string UIItemEdit11Text = "85";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit11SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in 'Reset' link
		/// </summary>
		public string UIResetHyperlinkSendKeys = "{Tab}";

		/// <summary>
		/// Type '1' in text box
		/// </summary>
		public string UIItemEdit12Text = "1";

		/// <summary>
		/// Type '710389310' in 'selectSubCon' text box
		/// </summary>
		public string UISelectSubConEditText = "710389310";

		/// <summary>
		/// Wait for 1 seconds for user delay between actions; Type '{Tab}' in 'selectSubCon' text box
		/// </summary>
		public string UISelectSubConEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '10000' in text box
		/// </summary>
		public string UIItemEdit13Text = "10000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit13SendKeys = "{Tab}";

		/// <summary>
		/// Type '5000' in text box
		/// </summary>
		public string UIItemEdit14Text = "5000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit14SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit15SendKeys = "{Tab}";

		/// <summary>
		/// Type '5000' in text box
		/// </summary>
		public string UIItemEdit16Text = "5000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit16SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit17SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit18SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit19SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit20SendKeys = "{Tab}";

		/// <summary>
		/// Type '1' in text box
		/// </summary>
		public string UIItemEdit21Text = "1";

		/// <summary>
		/// Type 'Burger Flipper' in text box
		/// </summary>
		public string UIItemEdit22Text = "Burger Flipper";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit22SendKeys = "{Tab}";

		/// <summary>
		/// Type '8.5' in text box
		/// </summary>
		public string UIItemEdit23Text = "8.5";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit23SendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in text box
		/// </summary>
		public string UIItemEdit24Text = "10";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit24SendKeys = "{Tab}";

		/// <summary>
		/// Type 'Grease Jockey' in text box
		/// </summary>
		public string UIItemEdit25Text = "Grease Jockey";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit25SendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in text box
		/// </summary>
		public string UIItemEdit26Text = "10";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit26SendKeys = "{Tab}";

		/// <summary>
		/// Type '10' in text box
		/// </summary>
		public string UIItemEdit27Text = "10";

		/// <summary>
		/// Type 'Delivery' in text box
		/// </summary>
		public string UIItemEdit28Text = "Delivery";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit28SendKeys = "{Tab}";

		/// <summary>
		/// Type '15' in text box
		/// </summary>
		public string UIItemEdit29Text = "15";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit29SendKeys = "{Tab}";

		/// <summary>
		/// Type '20' in text box
		/// </summary>
		public string UIItemEdit30Text = "20";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit30SendKeys = "{Tab}";

		/// <summary>
		/// Type 'Ordering those little toys in the happy meals' in text box
		/// </summary>
		public string UIItemEdit31Text = "Ordering those little toys in the happy meals";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit31SendKeys = "{Tab}";

		/// <summary>
		/// Type '5' in text box
		/// </summary>
		public string UIItemEdit32Text = "5";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit32SendKeys = "{Tab}";

		/// <summary>
		/// Type '6' in text box
		/// </summary>
		public string UIItemEdit33Text = "6";

		/// <summary>
		/// Type 'Autogenerated test data' in text box
		/// </summary>
		public string UIItemEdit34Text = "Autogenerated test data";

		/// <summary>
		/// Type '90000' in 'ContractCeiling' text box
		/// </summary>
		public string UIContractCeilingEditText2 = "90000";

		/// <summary>
		/// Type '{Tab}' in 'ContractCeiling' text box
		/// </summary>
		public string UIContractCeilingEditSendKeys1 = "{Tab}";

		/// <summary>
		/// Type '80000' in 'T1SvcsCeiling' text box
		/// </summary>
		public string UIT1SvcsCeilingEditText = "80000";

		/// <summary>
		/// Type '{Tab}' in 'T1SvcsCeiling' text box
		/// </summary>
		public string UIT1SvcsCeilingEditSendKeys = "{Tab}";

		/// <summary>
		/// Type '25' in text box
		/// </summary>
		public string UIItemEditText1 = "25";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEditSendKeys1 = "{Tab}";

		/// <summary>
		/// Type '25' in text box
		/// </summary>
		public string UIItemEdit1Text1 = "25";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit1SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '50' in text box
		/// </summary>
		public string UIItemEdit2Text1 = "50";

		/// <summary>
		/// Select check box
		/// </summary>
		public bool UIItemCheckBoxChecked = true;

		/// <summary>
		/// Type 'Autogenerated Supplemental Prime ' in text box
		/// </summary>
		public string UIItemEdit3Text1 = "Autogenerated Supplemental Prime ";

		/// <summary>
		/// Type 'Autogenerated Supplemental Prime - ' in text box
		/// </summary>
		public string UIItemEdit3Text2 = "Autogenerated Supplemental Prime - ";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000
		///'
		/// </summary>
		public string UIItemEdit3Text3 = "Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000\n";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000
		///Salaries increased
		///Soil Excavation Service removed'
		/// </summary>
		public string UIItemEdit3Text4 = "Autogenerated Supplemental Prime - Contract Ceiling lowered $10,000\nSalaries incr" +
			"eased\nSoil Excavation Service removed";

		/// <summary>
		/// Type '{Enter}' in 'SuppAgreementDate' text box
		/// </summary>
		public string UISuppAgreementDateEditSendKeys = "{Enter}";

		/// <summary>
		/// Type 'ha' in 'selectSubCon' text box
		/// </summary>
		public string UISelectSubConEditText1 = "ha";

		/// <summary>
		/// Type '{Enter}' in 'selectSubCon' text box
		/// </summary>
		public string UISelectSubConEditSendKeys1 = "{Enter}";

		/// <summary>
		/// Type '20000' in text box
		/// </summary>
		public string UIItemEdit4Text1 = "20000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit4SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '15000' in text box
		/// </summary>
		public string UIItemEdit11Text1 = "15000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit11SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit21SendKeys = "{Tab}";

		/// <summary>
		/// Type '5000' in text box
		/// </summary>
		public string UIItemEdit31Text1 = "5000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit31SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit41SendKeys = "{Tab}";

		/// <summary>
		/// Type '1' in text box
		/// </summary>
		public string UIItemEdit5Text1 = "1";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit5SendKeys = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit6SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit7SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '1' in text box
		/// </summary>
		public string UIItemEdit8Text1 = "1";

		/// <summary>
		/// Type 'Engineerer' in text box
		/// </summary>
		public string UIItemEdit9Text1 = "Engineerer";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit9SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '13.37' in text box
		/// </summary>
		public string UIItemEdit10Text1 = "13.37";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit10SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '350' in text box
		/// </summary>
		public string UIItemEdit111Text = "350";

		/// <summary>
		/// Type 'Secretary' in text box
		/// </summary>
		public string UIItemEdit12Text1 = "Secretary";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit12SendKeys = "{Tab}";

		/// <summary>
		/// Type '15' in text box
		/// </summary>
		public string UIItemEdit13Text1 = "15";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit13SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '15' in text box
		/// </summary>
		public string UIItemEdit14Text1 = "15";

		/// <summary>
		/// Type 'Consult' in text box
		/// </summary>
		public string UIItemEdit15Text = "Consult";

		/// <summary>
		/// Type 'Consultin'' in text box
		/// </summary>
		public string UIItemEdit15Text1 = "Consultin\'";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit15SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '200' in text box
		/// </summary>
		public string UIItemEdit16Text1 = "200";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit16SendKeys1 = "{Tab}";

		/// <summary>
		/// Type '300' in text box
		/// </summary>
		public string UIItemEdit17Text = "300";

		/// <summary>
		/// Type 'Non-binary gender assignment' in text box
		/// </summary>
		public string UIItemEdit18Text = "Non-binary gender assignment";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit18SendKeys1 = "{Tab}";

		/// <summary>
		/// Type 'Shift + {Tab}' in text box
		/// </summary>
		public string UIItemEdit19SendKeys1 = "{Tab}";

		/// <summary>
		/// Type 'More burgers' in text box
		/// </summary>
		public string UIItemEdit18Text1 = "More burgers";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit18SendKeys2 = "{Tab}";

		/// <summary>
		/// Type '0' in text box
		/// </summary>
		public string UIItemEdit19Text = "0";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit19SendKeys2 = "{Tab}";

		/// <summary>
		/// Type '25' in text box
		/// </summary>
		public string UIItemEdit20Text = "25";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental - Sub Consultants
		///Removed MehlBurger's Grease Jockey Salary
		///Added More Burgers Service'
		/// </summary>
		public string UIItemEdit3Text5 = "Autogenerated Supplemental - Sub Consultants\nRemoved MehlBurger\'s Grease Jockey S" +
			"alary\nAdded More Burgers Service";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental - Sub Consultants
		///Removed MehlBurger's Grease Jockey Salary
		///Added More Burgers Service
		///Added new SubCon'
		/// </summary>
		public string UIItemEdit3Text6 = "Autogenerated Supplemental - Sub Consultants\nRemoved MehlBurger\'s Grease Jockey S" +
			"alary\nAdded More Burgers Service\nAdded new SubCon";

		/// <summary>
		/// Type '6000' in text box
		/// </summary>
		public string UIItemEdit22Text1 = "6000";

		/// <summary>
		/// Type '4000' in text box
		/// </summary>
		public string UIItemEdit23Text1 = "4000";

		/// <summary>
		/// Type '{Tab}' in text box
		/// </summary>
		public string UIItemEdit23SendKeys1 = "{Tab}";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental - Sub Consultants
		///Edited Mehlburger's T1/T2 Ceilings to be 6000/4000
		///Removed MehlBurger's Grease Jockey Salary
		///Added More Burgers Service
		///Added new SubCon'
		/// </summary>
		public string UIItemEdit3Text7 = "Autogenerated Supplemental - Sub Consultants\nEdited Mehlburger\'s T1/T2 Ceilings t" +
			"o be 6000/4000\nRemoved MehlBurger\'s Grease Jockey Salary\nAdded More Burgers Serv" +
			"ice\nAdded new SubCon";

		/// <summary>
		/// Type the following paragraph in text box 
		///'Autogenerated Supplemental - Sub Consultants
		///Edited Mehlburger's T1/T2 Ceilings to be 6000/4000
		///Removed MehlBurger's Grease Jockey Salary
		///Added More Burgers Service
		///Added new SubCon - Hartman Engineering'
		/// </summary>
		public string UIItemEdit3Text8 = "Autogenerated Supplemental - Sub Consultants\nEdited Mehlburger\'s T1/T2 Ceilings t" +
			"o be 6000/4000\nRemoved MehlBurger\'s Grease Jockey Salary\nAdded More Burgers Serv" +
			"ice\nAdded new SubCon - Hartman Engineering";

		/// <summary>
		/// Type '{Enter}' in 'SuppAgreementDate' text box
		/// </summary>
		public string UISuppAgreementDateEditSendKeys1 = "{Enter}";
		#endregion
}
}
