using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NUnit.Framework;

namespace DAL
{
	[TestFixture( )]
	class DALTest
	{
		[Test( )]
		public void getStartedTest( )
		{
			Assert.AreEqual( 0, 0, "Equality pass test" );
			Assert.AreNotEqual( 0, 1, "InEquality pass test" );
		}
		[Test( )]
		public void returnTableTest( )
		{
			DAL dal = new DAL( );
			ArrayList al = new ArrayList( );
			dal.StoredProcedureName = "returnTable";
			dal.CommandType = CommandType.StoredProcedure;
			DataTable dt = dal.getTable( );
			Assert.AreEqual( 4, dt.Rows.Count, "Number of rows returned" );
		}
		[Test( )]
		public void returnOutputTest( )
		{
			DAL dal = new DAL( );
			ArrayList al = new ArrayList( );
			al.Add( 1222 );
			al.Add( 2222 );
			al.Add( 2222 );
			dal.Params = al;
			dal.StoredProcedureName = "returnParameter";
			dal.CommandType = CommandType.StoredProcedure;
			dal.ExecuteStoredProcedure( );
			if ( dal.Outputs.Count > 0 )
			{
				Assert.AreEqual( 1000, dal.Outputs[ 0 ], "Output parameter" );
				Assert.AreEqual( 1111, dal.Outputs[ 1 ], "Second Output Parameter" );
			}
		}
	}
}
