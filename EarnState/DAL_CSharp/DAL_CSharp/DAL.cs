using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.SqlClient;

namespace DAL
{
	public class DAL :IDisposable 
	{
		#region "Fields"

		protected string _connectionString = "", _storedProcedure = "";
		protected SqlDataAdapter _da = new SqlDataAdapter( );
		protected SqlCommand _command = new SqlCommand( );
		protected SqlConnection _connection = null;
		protected CommandType _commType = CommandType.StoredProcedure;
		protected ArrayList _params, _outputs;
		protected bool _disposed;
		protected SqlDataReader _dr;

		#endregion

		#region "Properties"
		
		public string ConnectionString
		{
			get
			{
				return _connectionString;
			}
			set
			{
				_connectionString = value;
			}
		}
		public ArrayList Params
		{
			get
			{
				return _params;
			}
			set
			{
				_params = value;
			}
		}
		public ArrayList Outputs
		{
			get
			{
				return _outputs;
			}
		}

		public string StoredProcedureName
		{
			get
			{
				return _storedProcedure;
			}
			set
			{
				_storedProcedure = value;
			}
		}
		public CommandType CommandType
		{
			get
			{
				return _commType;
			}
			set
			{
				_commType = value;
			}
		}

		public SqlDataReader DataReader
		{
			get
			{
				return _dr;
			}
		}
		
		#endregion

		#region "Constructors"
		public DAL( )
		{
			
		}

		public DAL( string connection )
		{
			PrepareConnection( connection );
		}

		#endregion

		#region "Public Methods"

		

		#region GetDataSet
		public DataSet getDataSet( string connection, string procedureName, ArrayList parms )
		{
			DataSet ds = new DataSet( );
			PrepareConnection( connection );
			ds = getDataSet( procedureName, parms );
			return ds;
		}

		public DataSet getDataSet( string procedureName, ArrayList parms )
		{
			DataSet ds = new DataSet( );
			_storedProcedure = procedureName;
			if ( parms != null )
			{
				_params = parms;
			}
			ds = getDataSet( );
			return ds;
		}

		public DataSet getDataSet( )
		{
			//Check that stuff is set?
			DataSet ds = new DataSet( );
			PrepareCommand( );
			if ( _params != null && _params.Count > 0 )
			{
				setCommandParameters( );
			}
			_da.SelectCommand = _command;
			try
			{
				_da.Fill( ds );
			}
			catch ( Exception e )
			{
				throw e;
			}
			finally
			{
				CloseDB( );
			}
			decipherOutputParameters( );
			return ds;
		}

		#endregion

		#region GetDataTable
		public DataTable getTable( string connection, string procedureName, ArrayList parms )
		{
			DataTable dt = new DataTable( );
			PrepareConnection( connection );
			dt = getTable( procedureName, parms );
			return dt;
		}

		public DataTable getTable( string procedureName, ArrayList parms )
		{
			DataTable dt = new DataTable( );
			_storedProcedure = procedureName;
			if ( parms != null )
			{
				Console.WriteLine( "Parm count: {0}", parms.Count );
				_params = parms;
			}
			dt = getTable( );
			return dt;
		}
		
		public DataTable getTable( )
		{
			//Check that stuff is set?
			// Put that somewhere else later
			DataTable dt = new DataTable( );
			PrepareCommand( );
			if ( _params != null && _params.Count > 0 )
			{
				setCommandParameters( );
			}
			_da.SelectCommand = _command;
			try
			{
				_da.Fill( dt );
			}
			catch ( Exception e )
			{
				throw e;
			}
			finally
			{
				CloseDB( );
			}
			decipherOutputParameters( );
			return dt;
		}
		#endregion

		public void ExecuteDataReader( string connection, string procedureName, ArrayList parms )
		{
			PrepareConnection( connection );
			ExecuteDataReader( procedureName, parms );
		}

		public void ExecuteDataReader( string procedureName, ArrayList parms )
		{
			if ( parms != null )
			{
				_params = parms;
			}
			_storedProcedure = procedureName;
			ExecuteDataReader( );
		}

		public void ExecuteDataReader( )
		{
			PrepareCommand( );
			if ( _params != null && _params.Count > 0 )
			{
				setCommandParameters( );
			}
			OpenDB( );
			_dr = _command.ExecuteReader( );
			
		}

		public void CloseDataReader( )
		{
			if (_dr != null)
			{
				_dr.Close();
			}
			CloseDB( );
		}

	
		#region ExecuteStoredProcedure
		public Int32 ExecuteStoredProcedure( string connection, string procedureName, ArrayList parms )
		{
			PrepareConnection( connection );
			Int32 ret = ExecuteStoredProcedure( procedureName, parms );
			return ret;
		}

		public Int32 ExecuteStoredProcedure( string procedureName, ArrayList parms )
		{
			Console.WriteLine( "Passed Parm Count: {0}", parms.Count );
			if ( parms != null )
			{
				_params = parms;
			}
			_storedProcedure = procedureName;
			Int32 ret = ExecuteStoredProcedure( );
			return ret;
		}

		public Int32 ExecuteStoredProcedure( )
		{
			PrepareCommand( );
			if ( _params != null && _params.Count > 0 )
			{
				setCommandParameters( );
			}
			OpenDB( );
			//Console.WriteLine("Just before execute non query");
			//for (int i = 0; i < _command.Parameters.Count; i++)
			//{
			//    Console.WriteLine("Param: {0}, Direction: {1}, Value: {2}, Data Type: {3}", _command.Parameters[i].ParameterName,
			//                        _command.Parameters[i].Direction, _command.Parameters[i].Value,
			//                        _command.Parameters[i].DbType);
			//}
			Int32 ret = _command.ExecuteNonQuery( );
			//Console.WriteLine("return value: {0}", ret);
			CloseDB( );
			decipherOutputParameters( );
			return ret;
		}
		#endregion

		#region Transactions
		public void ExecuteTransactions( List<TransactionProcedure> procedures )
		{
			if ( procedures != null )
			{
				OpenDB( );
				SqlTransaction trans = _connection.BeginTransaction();

				try
				{

					foreach ( TransactionProcedure tp in procedures )
					{
						_params.Clear( );
						_outputs.Clear( );

						_storedProcedure = tp.ProcName;
						if ( tp.ProcParms != null )
						{
							_params = tp.ProcParms;
						}

						PrepareCommand( );
						if ( _params != null && _params.Count > 0 )
						{
							setCommandParameters( );
						}

						_command.Transaction = trans;

						_command.ExecuteNonQuery( );
						decipherOutputParameters( );

					}

					trans.Commit( );
				}
				catch( Exception ep )
				{
					trans.Rollback( );
					throw;
				}
				finally
				{
					CloseDB( );
				}
			}
		}



		#endregion

		#region Get Single Value
		public object GetSingleValue( string connection, string procedureName, ArrayList parms )
		{
			PrepareConnection( connection );
			object ret = GetSingleValue( procedureName, parms );
			return ret;
		}

		public object GetSingleValue( string procedureName, ArrayList parms )
		{
			if ( parms != null )
			{
				_params = parms;
			}
			_storedProcedure = procedureName;
			object ret = GetSingleValue( );
			return ret;
		}

		public object GetSingleValue( )
		{
			PrepareCommand( );
			if ( _params != null && _params.Count > 0 )
			{
				setCommandParameters( );
			}
			OpenDB( );
			object ret = _command.ExecuteScalar( );
			CloseDB( );
			decipherOutputParameters( );
			return ret;
		}

		#endregion

		#endregion

		#region "Private Methods"

		private void PrepareConnection( string connection )
		{
			_connectionString = connection;
			_connection = new SqlConnection( _connectionString );
		}

		protected void decipherOutputParameters( )
		{
			_outputs = new ArrayList( );
			for ( int i = 0; i < _command.Parameters.Count; i++ )
			{
				if ( _command.Parameters[ i ].Direction == ParameterDirection.Output ||
					_command.Parameters[ i ].Direction == ParameterDirection.InputOutput )
				{
					_outputs.Add( _command.Parameters[ i ].Value );
					//Console.WriteLine("DECIPHER ___ Param: {0}, Direction: {1}, Value: {2}, Data Type: {3}", _command.Parameters[i].ParameterName,
					//                    _command.Parameters[i].Direction, _command.Parameters[i].Value,
					//                    _command.Parameters[i].DbType);
				}
			}
		}
		
		protected void setCommandParameters( )
		{
			Int16 Direction = 0;
			DataTable dt = new DataTable( );
			SqlCommand _comm2 = new SqlCommand( );
			// Cannot use prepare since we would overwrite the proc name, etc.
			_comm2.CommandText = "sp_procedure_params_rowset";
			_comm2.Connection = _connection;
			_comm2.CommandType = CommandType.StoredProcedure;
			_comm2.Parameters.Clear( );
			//Need to account for Schema when getting parameters
			if ( _storedProcedure.Contains( "." ) )
			{
				string[ ] schemas = _storedProcedure.Split( new char[ ] { '.' } );
				string schema = schemas[ 0 ];
				string name = schemas[ schemas.Length - 1 ];

				_comm2.Parameters.Add( new SqlParameter( "@procedure_name", name ) );
				Console.WriteLine( "Determine parameters for: {0}", _storedProcedure );

				_comm2.Parameters.Add( new SqlParameter( "@procedure_schema", schema ) );
			}
			else
			{
				_comm2.Parameters.Add( new SqlParameter( "@procedure_name", _storedProcedure ) );
				Console.WriteLine( "Determine parameters for: {0}", _storedProcedure );
			}
			_da.SelectCommand = _comm2;
			try
			{
				_da.Fill( dt );
				Console.WriteLine( "Expected number of parameters: {0}", dt.Rows.Count );
			}
			catch ( Exception e )
			{
				Console.WriteLine( "Exception while determining number of parameters.\r\n{0}", e.Message );
				throw e;
				
			}
			finally
			{
				CloseDB( );
			}
			// Handles the return parameter which the database will send back
			Console.WriteLine( "SetCommandParamters\r\nParm Count: {0}", _params.Count );
			if ( _params != null )
			{
				Console.WriteLine( "Params not null" );
				_params.Insert( 0, 0 );
			}
			if ( _params.Count != dt.Rows.Count )
			{
				Console.WriteLine( "SP parm count: {0}", dt.Rows.Count );
				throw new Exception( "The number of Values does not match the number of parameters" );
			}
			_command.Parameters.Clear( );
			for ( Int32 i = 0; i < dt.Rows.Count; i++ )
			{
				SqlParameter sqlParam = new SqlParameter( );
				sqlParam.ParameterName = dt.Rows[ i ][ "Parameter_Name" ].ToString( );
				sqlParam.Value = _params[ i ];
				Direction = ( Int16 ) dt.Rows[ i ][ "Parameter_Type" ];
				Console.WriteLine( "Parameter Name: {0}", sqlParam.ParameterName );
				switch ( Direction )
				{
					case 1:
						sqlParam.Direction = ParameterDirection.Input;
						break;
					case 2:
						sqlParam.Direction = ParameterDirection.Output;
						break;
					case 3:
						sqlParam.Direction = ParameterDirection.InputOutput;
						break;
					case 4:
						sqlParam.Direction = ParameterDirection.ReturnValue;
						break;
				}
				_command.Parameters.Add( sqlParam );
			}
			//Don't need return parameter
			_command.Parameters.RemoveAt( 0 );
			_params.Clear( );
		}
		/// <summary>
		/// Sets command properties, clears parameters
		/// </summary>
		/// <param name="CommText">Name of stored procedure or text of sql statement</param>
		/// <param name="CommType">Stored Procedure, Table Direct or Text</param>
		private void PrepareCommand( )
		{
			Console.WriteLine( "Proc Name: {0}", _storedProcedure );
			// at some point set timeouts
			_command.CommandText = _storedProcedure;
			_command.CommandType = _commType;
			_command.Connection = _connection;
			_command.Parameters.Clear( );
		}
		private void OpenDB( )
		{
			if ( _connection == null )
			{
				_connection = new SqlConnection( _connectionString );
			}
			if ( _connection.State == ConnectionState.Closed )
			{
				_connection.Open( );
			}
		}
		private void CloseDB( )
		{
			if ( !( _connection == null ) &&
				_connection.State == ConnectionState.Open )
			{
				_connection.Close( );
			}
		}

		#endregion

		#region Dispose
		public void Dispose( )
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		public void Dispose( bool disposing )
		{
			if ( !disposing )
			{
				if ( !_disposed )
				{
					if ( _connection != null )
					{
						CloseDB( );
					}
				}
			}
			_disposed = true;
		}

		#endregion

	}

	public struct TransactionProcedure
	{
		public string ProcName;
		public ArrayList ProcParms;

		public TransactionProcedure( string procName, ArrayList procParms )
		{
			this.ProcName = procName;
			this.ProcParms = procParms; 
		}


	}
}
