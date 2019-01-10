using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AHTD.Entities;

namespace AHTD.TestEarnState
{
	/// <summary>
	/// Test properites and methods for the user security class
	/// </summary>
	[TestClass]
	public class SecurityTest
	{
		string user;
		
		public SecurityTest( )
		{
			string[ ] userInfo = System.Security.Principal.WindowsIdentity.GetCurrent( ).Name.Split( new char[ ] { '\\' } );

			user = userInfo[ 1 ];
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		/// <summary>
		/// Confirms that the userid being passed in is being properly set in the UserSecurity Class
		/// </summary>
		[TestMethod]
		public void GetUserId()
		{
			UserSecurity sec = new UserSecurity( user);
			
			Console.WriteLine( user );
						
			Assert.IsTrue( user == sec.UserId);
		}
		
		/// <summary>
		/// Checks to see if the user priority is being retrieved correctly from the database.
		/// </summary>
		[TestMethod]
		public void GetUserPriority()
		{
			UserSecurity sec = new UserSecurity(user);
			sec.GetPriorityLevel();
			
			Assert.IsTrue( sec.Priority == "DA");
		}
		
		/// <summary>
		/// Sets how far back the user can see based on the priority property.  In this case
		/// we expect to only be able to view 6 months
		/// </summary>
		[TestMethod]
		public void TestView6MonthStartDate()
		{
			UserSecurity sec = new UserSecurity( user );
			sec.SetStartDate();
			Assert.IsTrue ( sec.PayPeriodStartDate == DateTime.Today.AddMonths(-6) );
		}
		
		/// <summary>
		/// Sets how far back the user can see based on the priority property.  In this case
		/// we expect to only be able to view all records
		/// </summary>
		[TestMethod]
		public void TestViewAllStartDate()
		{
			UserSecurity sec = new UserSecurity( "JAPA332");
			sec.SetStartDate();
			Assert.IsTrue( sec.PayPeriodStartDate.ToShortDateString() == "1/1/1900");
		}

		[TestMethod]
		public void CheckViewAllPropertyIsTrue()
		{
			UserSecurity sec = new UserSecurity( "JAPA332" );
			Assert.IsTrue( sec.ViewAll == true );
		}

		[TestMethod]
		public void CheckViewAllPropertyIsFalse( )
		{
			UserSecurity sec = new UserSecurity( "HKYA321" );
			Assert.IsTrue( sec.ViewAll == false );
		}

		[TestMethod]
		public void CheckBudgetList( )
		{
			UserSecurity sec = new UserSecurity( user );
			Assert.IsTrue( sec.NonEngineeringBudgetIds.Count == 1 );
		}

		[TestMethod]
		public void CheckEngineeringList( )
		{
			UserSecurity sec = new UserSecurity( user );
			Assert.IsTrue( sec.EngineeringBudgetIds.Count == 3 );
		}

		[TestMethod]
		public void CheckUserAccessGranted( )
		{
			UserSecurity sec = new UserSecurity( user );
			Assert.IsTrue( sec.GrantAccess == true );
		}

		[TestMethod]
		public void CheckUserAccessDenied( )
		{
			UserSecurity sec = new UserSecurity( "DHJA394" );
			Assert.IsTrue( sec.GrantAccess == false );
		}
	}
}
