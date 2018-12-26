using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Utilities
{
    public static class Extensions
    {
        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }
        public static string GetExceptionMessages(this DbEntityValidationException e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            if (e.EntityValidationErrors != null)
            {
                foreach (var error in e.EntityValidationErrors)
                {
                    foreach (var msg in error.ValidationErrors)
                    {
                        msgs += "\r\nDBValidationException: " + msg.PropertyName + ": " + msg.ErrorMessage;
                    }
                }
                
            }
            return msgs;
        }

        public static string AsRomanNumeral(this short number)
        {
            var romanNumerals = new string[][]
        {
            new string[]{"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"}, // ones
            new string[]{"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"}, // tens
            new string[]{"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"}, // hundreds
            new string[]{"", "M", "MM", "MMM"} // thousands
        };

            // split integer string into array and reverse array
            var intArr = number.ToString().Reverse().ToArray();
            var len = intArr.Length;
            var romanNumeral = "";
            var i = len;

            // starting with the highest place (for 3046, it would be the thousands
            // place, or 3), get the roman numeral representation for that place
            // and add it to the final roman numeral string
            while (i-- > 0)
            {
                romanNumeral += romanNumerals[i][Int32.Parse(intArr[i].ToString())];
            }

            return romanNumeral;
        }

        public static string FormatNullDate(DateTime? dt)
        {
            return dt == null ? "n/a" : dt.Value.ToShortDateString();
        }

    }
}