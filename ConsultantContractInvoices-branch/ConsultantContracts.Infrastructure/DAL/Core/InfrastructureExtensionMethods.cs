using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ConsultantContracts.Infrastructure.DAL.Core
{
    public static class InfrastructureExtensionMethods
    {
        public static IQueryable<TEntity> Include<TEntity>(this IDbSet<TEntity> dbSet, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query == null ? dbSet : query;
        }
    }
}
