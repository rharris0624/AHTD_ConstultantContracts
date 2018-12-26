using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHTD.Logging
{
	internal static class FormatterUtil
	{
		internal static readonly string SeparatorLine = "----------------------------------------";

		internal static string GetMessageHeader(Exception ex, bool unhandled)
		{
			StringBuilder message = new StringBuilder();

			message.AppendLine(SeparatorLine);
			message.AppendLine(String.Format("Timestamp       : {0:g}", DateTimeOffset.Now));

			if (unhandled)
				message.AppendLine(String.Format("An unhandled exception of type '{0}' occurred.", ex.GetType().FullName));
			else
				message.AppendLine(String.Format("An exception of type '{0}' occurred and was caught.", ex.GetType().FullName));

			message.AppendLine(SeparatorLine);

			return message.ToString();
		}
		internal static string GetExceptionDetails(Exception ex, int indent = 0)
		{
			StringBuilder message = new StringBuilder();

			string indention = String.Empty.PadLeft(indent);

			// Write out type info
			message.AppendLine(String.Format("{0}{1,-16}: {2}", indention, "Type", ex.GetType()));

			// Reflect on all properties and write them out
			foreach (var prop in ex.GetType().GetProperties())
			{
				// Skip the InnerException property, if any, as we deal with it explicitly below
				if (prop.Name != "InnerException")
				{
					message.AppendLine(String.Format("{0}{1,-16}: {2}", indention, prop.Name, prop.GetValue(ex, null)));
				}
				var list = prop.GetValue(ex, null) as IList;
				if (list != null)
				{
					foreach (var item in list)
					{
						message.AppendLine(String.Format("{0}    {1}", indention, item.ToString()));
					}
				}
			}

			// Write out inner exception details
			if (ex.InnerException != null)
			{
				message.AppendLine();
				message.AppendLine(String.Format("{0}InnerException Details", indention));
				message.Append(GetExceptionDetails(ex.InnerException, indent + 4));
			}

			return message.ToString();
		}
	}
}
