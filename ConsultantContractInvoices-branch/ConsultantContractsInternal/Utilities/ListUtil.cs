using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Utilities
{
    public static class ListUtil
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, T, bool> lambda)
        {
            return list.Distinct(new LambdaComparer<T>(lambda));
        }
    }
}