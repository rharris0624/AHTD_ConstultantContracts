using System;
using System.Collections.Generic;
using System.Text;

namespace AHTD.Entities
{
	[Serializable]
	public class BudgetItem
	{
		string _budget;
		string _functionalArea;
		
		public string Budget
		{
			get{ return _budget;}
		}
		
		public string FunctionalArea
		{
			get{ return _functionalArea;}
		}
		
		/// <summary>
		/// Class representing the Budget ID and Functional area that a user 
		/// has access to
		/// </summary>
		/// <param name="budgetId"></param>
		/// <param name="functionalArea"></param>
		public BudgetItem( string budgetId, string functionalArea)
		{
			_budget = budgetId;
			_functionalArea = functionalArea;
		}

		public override string ToString( )
		{
			return _budget;
		}
	}
}
