using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AHTD.Entities
{
	public class EarnStatement
	{

		#region Member Variables
		string _ssn;
		DateTime _payPeriodEnding;
		string _empNo;
		string _budgetNo;
		string _engNo;
		string _crewNo;
		string _nameLast;
		string _nameFirst;
		string _nameMiddle;
		string _address;
		string _city;
		string _state;
		string _zip;
		string _netPay;
		string _regPay;
		string _regHours;
		string _otPay;
		string _otHours;
		string _wcCompHours;
		string _grossPay;
		string _fedWithholding;
		string _stateWithholding;
		string _fica;
		string _retirement;
		string _autoUse;
		string _eic;
		string _totalDeductions;
		string _groupIns;
		string _lifeIns;
		string _universalLife;
		string _cancerIntensiveCare;
		string _autoIns;
		string _disabilityIns;
		string _dentalIns;
		string _transAmerica;
		string _canadaLife;
		string _cigna;
		string _bonds;
		string _deferredComp;
		string _creditUnion;
		string _milRetirement;
		string _asea;
		string _unitedway;
		string _accident;
		string _aflac;
		string _ltc;
		string _vision;
		string _healthBank;
		string _dayCare;
		string _bankruptcy;
		string _garnishment;
		string _irs;
		string _childSupport;
		string _ytdGrossPay;
		string _ytdFedWithholding;
		string _ytdStateWithholding;
		string _ytdFICA;
		string _ytdTaxGross;
		string _ytdEIC;
		string _ytdDeferredComp;
		string _ytdRetirement;
		string _ytdUnitedWay;
		string _ytdLTC;
		string _paymentType;
        string _UniAllowance;
        string _ToolAllowance;
		#endregion

		#region Public Properties
		public string SSN
		{
			get
			{
				return _ssn;
			}
		}

		public DateTime PayPeriodEnding
		{
			get
			{
				return _payPeriodEnding;
			}
		}

		public string EmpNo
		{
			get
			{
				return _empNo;
			}
		}

		public string BudgetNo
		{
			get
			{
				return _budgetNo;
			}
		}

		public string EngNo
		{
			get
			{
				return _engNo;
			}
		}

		public string CrewNo
		{
			get
			{
				return _crewNo;
			}
		}

		public string NameLast
		{
			get
			{
				return _nameLast;
			}
		}

		public string NameFirst
		{
			get
			{
				return _nameFirst;
			}
		}

		public string NameMiddle
		{
			get
			{
				return _nameMiddle;
			}
		}

		public string Address
		{
			get
			{
				return _address;
			}
		}

		public string City
		{
			get
			{
				return _city;
			}
		}

		public string State
		{
			get
			{
				return _state;
			}
		}

		public string Zip
		{
			get
			{
				return _zip;
			}
		}

		public string NetPay
		{
			get
			{
				return _netPay;
			}
		}

		public string RegPay
		{
			get
			{
				return _regPay;
			}
		}

		public string RegHours
		{
			get{return _regHours;}
		}
		
		public string OTPay
		{
			get
			{
				return _otPay;
			}
		}

		public string OTHours
		{
			get
			{
				return _otHours;
			}
		}

		public string WrkCompHours
		{
			get
			{
				return _wcCompHours;
			}
		}

		public string GrossPay
		{
			get
			{
				return _grossPay;
			}
		}

		public string FederalWithholding
		{
			get
			{
				return _fedWithholding;
			}
		}

		public string StateWithholding
		{
			get
			{
				return _stateWithholding;
			}
		}

		public string FICA
		{
			get
			{
				return _fica;
			}
		}

		public string Retirement
		{
			get
			{
				return _retirement;
			}
		}

		public string AutoUse
		{
			get
			{
				return _autoUse;
			}
		}

		public string EIC
		{
			get
			{
				return _eic;
			}
		}

		public string TotalDeductions
		{
			get
			{
				return _totalDeductions;
			}
		}

		public string GroupIns
		{
			get
			{
				return _groupIns;
			}
		}

		public string LifeIns
		{
			get
			{
				return _lifeIns;
			}
		}

		public string UniversalLife
		{
			get
			{
				return _universalLife;
			}
		}

		public string CancerIntensiveCare
		{
			get
			{
				return _cancerIntensiveCare;
			}
		}

		public string AutoIns
		{
			get
			{
				return _autoIns;
			}
		}

		public string DisabilityIns
		{
			get
			{
				return _disabilityIns;
			}
		}

		public string DentalIns
		{
			get
			{
				return _dentalIns;
			}
		}

		public string TransAmerica
		{
			get
			{
				return _transAmerica;
			}
		}

		public string CanadaLife
		{
			get
			{
				return _canadaLife;
			}
		}

		public string CIGNA
		{
			get
			{
				return _cigna;
			}
		}

		public string Bonds
		{
			get
			{
				return _bonds;
			}
		}

		public string DeferredComp
		{
			get
			{
				return _deferredComp;
			}
		}

		public string CreditUnion
		{
			get
			{
				return _creditUnion;
			}
		}

		public string MilitaryRetirement
		{
			get
			{
				return _milRetirement;
			}
		}

		public string ASEA
		{
			get
			{
				return _asea;
			}
		}

		public string Unitedway
		{
			get
			{
				return _unitedway;
			}
		}

		public string Accident
		{
			get
			{
				return _accident;
			}
		}

		public string AFLAC
		{
			get
			{
				return _aflac;
			}
		}

		public string LTC
		{
			get
			{
				return _ltc;
			}
		}

		public string Vision
		{
			get
			{
				return _vision;
			}
		}

		public string HealthBank
		{
			get
			{
				return _healthBank;
			}
		}

		public string DayCare
		{
			get
			{
				return _dayCare;
			}
		}

		public string Bankruptcy
		{
			get
			{
				return _bankruptcy;
			}
		}

		public string Garnishment
		{
			get
			{
				return _garnishment;
			}
		}

		public string IRS
		{
			get
			{
				return _irs;
			}
		}

		public string ChildSupport
		{
			get
			{
				return _childSupport;
			}
		}

		public string YTDGrossPay
		{
			get
			{
				return _ytdGrossPay;
			}
		}

		public string YTDFedWithholding
		{
			get
			{
				return _ytdFedWithholding;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member" )]
		public string YTDStateWithholding
		{
			get
			{
				return _ytdStateWithholding;
			}
		}

		public string YTDFICA
		{
			get
			{
				return _ytdFICA;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member" )]
		public string YTDTaxGross
		{
			get
			{
				return _ytdTaxGross;
			}
		}

		public string YTDEIC
		{
			get
			{
				return _ytdEIC;
			}
		}

		public string YTDDeferredComp
		{
			get
			{
				return _ytdDeferredComp;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member" )]
		public string YTDRetirement
		{
			get
			{
				return _ytdRetirement;
			}
		}

		public string YTDUnitedWay
		{
			get
			{
				return _ytdUnitedWay;
			}
		}

		public string YTDLTC
		{
			get
			{
				return _ytdLTC;
			}
		}

		public string PaymentType
		{
			get
			{
				return _paymentType;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower" )]
		public string DisplayName
		{
			get
			{
				return EarnStatement.ToTitleCase( NameLast.ToLower() ) + ", " + EarnStatement.ToTitleCase( NameFirst.ToLower() ) + " " + EarnStatement.ToTitleCase( NameMiddle.Substring( 0, 1 ).ToLower());
			}
		}

        public string UniformAllowance
        {
            get
            {
                return _UniAllowance;
            }
        }

        public string ToolAllowance
        {
            get
            {
                return _ToolAllowance;
            }
        }

		#endregion

		public EarnStatement( DataRow row )
		{
			PackageItem( row );
		}

		private void PackageItem( DataRow row )
		{
			_ssn = row[ "SSNO" ].ToString( );
			_payPeriodEnding = (DateTime) row[ "PayperiodEndingDate" ];
			_empNo = row[ "EmployeeNumber" ].ToString( );
			_budgetNo = row[ "Budget" ].ToString( );
			_engNo = row[ "EngNo" ].ToString( );
			_crewNo = row[ "CrewNo" ].ToString( );
			_nameLast = row[ "NameLast" ].ToString( );
			_nameFirst = row[ "NameFirst" ].ToString( );
			_nameMiddle = row[ "NameMiddle" ].ToString( );
			_address = row[ "Address" ].ToString( );
			_city = row[ "City" ].ToString( );
			_state = row[ "State" ].ToString( );
			_zip = row[ "ZipCode" ].ToString( );
			_netPay = row[ "NetPay" ].ToString( );
			_regPay = row[ "RegularPay" ].ToString( );
			_regHours = row[ "RegularHours" ].ToString( );
			_otPay = row[ "OverTimePay" ].ToString( );
			_otHours = row[ "OverTimeHours" ].ToString( );
			_wcCompHours = row[ "WorkCompCatHours" ].ToString( );
			_grossPay = row[ "GrossPay" ].ToString( );
			_fedWithholding = row[ "FederalWithholding" ].ToString( );
			_stateWithholding = row[ "StateWithholding" ].ToString( );
			_fica = row[ "FICAWithholding" ].ToString( );
			_retirement = row[ "Retirement" ].ToString( );
			_autoUse = row[ "AutoUse" ].ToString( );
			_eic = row[ "EIC" ].ToString( );
			_totalDeductions = row[ "TotalDeductions" ].ToString( );
			_groupIns = row[ "GroupInsurance" ].ToString( );
			_lifeIns = row[ "LifeInsurance" ].ToString( );
			_universalLife = row[ "UniversalLife" ].ToString( );
			_cancerIntensiveCare = row[ "CancerIntensiveCare" ].ToString( );
			_autoIns = row[ "AutoInsurance" ].ToString( );
			_disabilityIns = row[ "DisabilityInsurance" ].ToString( );
			_dentalIns = row[ "DentalInsurance" ].ToString( );
			_transAmerica = row[ "TransAmericaLife" ].ToString( );
			_canadaLife = row[ "CanadaLife" ].ToString( );
			_cigna = row[ "CIGNA" ].ToString( );
			_bonds = row[ "Bonds" ].ToString( );
			_deferredComp = row[ "DeferredComp" ].ToString( );
			_creditUnion = row[ "CreditUnion" ].ToString( );
			_milRetirement = row[ "MilitaryRetirement" ].ToString( );
			_asea = row[ "ASEA" ].ToString( );
			_unitedway = row[ "UnitedWay" ].ToString( );
			_accident = row[ "Accident" ].ToString( );
			_aflac = row[ "AFLACDisability" ].ToString( );
			_ltc = row[ "LongTermCare" ].ToString( );
			_vision = row[ "VisionCare" ].ToString( );
			_healthBank = row[ "HealthBank" ].ToString( );
			_dayCare = row[ "DayCare" ].ToString( );
			_bankruptcy = row[ "Bankruptcy" ].ToString( );
			_garnishment = row[ "Garnishment" ].ToString( );
			_irs = row[ "IRSLevy" ].ToString( );
			_childSupport = row[ "ChildSupport" ].ToString( );
			_ytdGrossPay = row[ "YTDGrossPay" ].ToString( );
			_ytdFedWithholding = row[ "YTDFederalWithholding" ].ToString( );
			_ytdStateWithholding = row[ "YTDStateWithholding" ].ToString( );
			_ytdFICA = row[ "YTDFICAWithholding" ].ToString( );
			_ytdTaxGross = row[ "YTDTaxableGross" ].ToString( );
			_ytdEIC = row[ "YTDEIC" ].ToString( );
			_ytdDeferredComp = row[ "YTDDeferredComp" ].ToString( );
			_ytdRetirement = row[ "YTDRetirement" ].ToString( );
			_ytdUnitedWay = row[ "YTDUnitedWay" ].ToString( );
			_ytdLTC = row[ "YTDLongTermCare" ].ToString( );
			_paymentType = row[ "WarrantOrDirectDeposit" ].ToString( );

            if (!string.IsNullOrEmpty( row["ExpenseAllowance"].ToString()))
            {
                _ToolAllowance = row["ExpenseAllowance"].ToString();
            }
            else
            {
                _ToolAllowance = "0";
            }

            if (!string.IsNullOrEmpty( row["UniformAllowance"].ToString() ))
            {
                _UniAllowance = row["UniformAllowance"].ToString();
            }
            else
            {
                _UniAllowance = "0";
            }

		}

		public static string ToTitleCase( string text )
		{
			string rText = "";
			try
			{
				System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
				System.Globalization.TextInfo TextInfo = cultureInfo.TextInfo;
				rText = TextInfo.ToTitleCase( text );
			}
			catch
			{
				rText = text;
			}
			return rText;
		}  


	}
}
