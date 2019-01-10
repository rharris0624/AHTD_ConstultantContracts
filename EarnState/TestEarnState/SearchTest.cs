using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AHTD.TestEarnState
{
	/// <summary>
	/// Summary description for SearchTest
	/// </summary>
	[TestClass]
	public class SearchTest
	{
		string user;
		Entities.UserSecurity userSec;

		public SearchTest( )
		{
			string[ ] userInfo = System.Security.Principal.WindowsIdentity.GetCurrent( ).Name.Split( new char[ ] { '\\' } );

			user = userInfo[ 1 ];

		    userSec = new AHTD.Entities.UserSecurity( user );
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

		[TestMethod]
		public void TestMethod1( )
		{
			System.Data.DataSet ds =  Entities.CommonFunctions.GetHeaderByName( userSec, "Adams" );
			Assert.IsTrue( true );
		}
	}
}
