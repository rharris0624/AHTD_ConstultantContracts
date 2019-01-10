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
	public partial class EarnStateView : System.Web.UI.Page
	{
		Entities.EarnStatement earnstate;

		protected void Page_Load( object sender, EventArgs e )
		{
			if ( !IsPostBack )
			{
				if ( Session[ "EarnStatementObject" ] != null )
				{
					earnstate = ( Entities.EarnStatement ) Session[ "EarnStatementObject" ];
					BindControls( );
				}
				else
				{
					Response.Redirect( "Default.aspx" );
				}

			}
		}

		private void BindControls( )
		{
			EmpName.Text = earnstate.DisplayName;
			EmpAddr1.Text = Entities.EarnStatement.ToTitleCase( earnstate.Address.ToLower());
			EmpCityState.Text = Entities.EarnStatement.ToTitleCase( earnstate.City.ToLower()) + ", " + earnstate.State + " " + earnstate.Zip;
			lblEmpNo.Text = earnstate.EmpNo;
			lblBudgetNo.Text = earnstate.BudgetNo;
			lblCrewNo.Text = earnstate.CrewNo;
			lblEngNo.Text = earnstate.EngNo;
			lblPayEnding.Text = earnstate.PayPeriodEnding.ToShortDateString();
			lblNetPay.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.NetPay ) );
			lblPayType.Text = earnstate.PaymentType;
			lblRegPay.Text = string.Format( "{0:C}", Convert.ToDouble(earnstate.RegPay));
			lblRegHours.Text = earnstate.RegHours;
			lblOTPay.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.OTPay ) );
			lblOTHours.Text = earnstate.OTHours;
			lblWCHours.Text = earnstate.WrkCompHours;
			lblGross.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.GrossPay ));
			lblFedTax.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.FederalWithholding ) );
			lblStateTax.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.StateWithholding ) );
			lblFICA.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.FICA ) );
			lblRetirement.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Retirement ) );
			lblAutoUse.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.AutoUse ) );
			lblEIC.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.EIC ) );
			lblTotalDed.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.TotalDeductions ) );

			lblGrpIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.GroupIns ) );
			lblLifeIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.LifeIns ) );
			lblUniversalIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.UniversalLife ) );
			lblCancerIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.CancerIntensiveCare ) );
			lblAccidentIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Accident ) );

			lblAutoIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.AutoIns ) );
			lblDisabilityIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.DisabilityIns ) );
			lblDentalIns.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.DentalIns ) );
			lblTransAmerica.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.TransAmerica ) );
			lblDisabilityIncome.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.AFLAC ) );

			lblUnum.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.CanadaLife ) );
			lblCigna.Text = string.Format( "{0:C}", Convert.ToDouble(earnstate.CIGNA));
			lblBonds.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Bonds ) );
			lblDeferredComp.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.DeferredComp ) );
			lblLongTerm.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.LTC ) );

			lblCreditUnion.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.CreditUnion ) );
			lblMilitaryRet.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.MilitaryRetirement ) );
			lblASEA.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.ASEA ) );
			lblHealthBank.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.HealthBank ) );
			lblDaycare.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.DayCare ) );

			lblUnitedWay.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Unitedway ) );
			lblVisionCare.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Vision ) );

			lblChildSupport.Text = string.Format( "{0:C}", Convert.ToDouble(earnstate.ChildSupport));
			lblBankruptcy.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Bankruptcy ) );
			lblGarnishment.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.Garnishment ) );
			lblIRS.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.IRS ) );

			lblGrossYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDGrossPay  ) );
			lblFedYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDFedWithholding ) );
			lblStateYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDStateWithholding ) );
			lblFICAYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDFICA  ) );
			lblRetirementYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDRetirement ) );

			lblTaxGrossYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDTaxGross));
			lblEICYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDEIC));
			lblDeferredCompYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDDeferredComp ));
			lblUnitedWayYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDUnitedWay));
			lblLongTermYTD.Text = string.Format( "{0:C}", Convert.ToDouble( earnstate.YTDLTC ) );

            lblUniAllowance.Text = string.Format("{0:C}", Convert.ToDouble(earnstate.UniformAllowance));
            lblToolAllowance.Text = string.Format("{0:C}", Convert.ToDouble(earnstate.ToolAllowance));
		}
	}
}
