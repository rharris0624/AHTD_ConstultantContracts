using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ConsultantContracts.Interfaces.DAL;
using ConsultantContracts.Infrastructure.Models;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity;

namespace ConsultantContracts.Infrastructure.DAL
{
    public class Repository : IRepository, IDisposable
    {
        private ConsultantContractsEntities _context;
        private bool bDisposed;
        private IUnitOfWork _unitOfWork;

        public Repository() { }

        public Repository(ConsultantContractsEntities context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork(_context);
                }
                return _unitOfWork; 
            }
        }

        
        public TEntity GetByKey<TEntity>(object keyValue) where TEntity : class
        {
            EntityKey key = GetEntityKey<TEntity>(keyValue);

            object originalItem;
            if (((IObjectContextAdapter)_context).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                return (TEntity)originalItem;
            }
            return default(TEntity);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            string entityName = GetEntityName<TEntity>();
            return ((IObjectContextAdapter)_context).ObjectContext.CreateQuery<TEntity>(entityName);
        }

        public IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetQuery<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity , TOrderBy>> orderBy, int pageIndex, int pageSize, System.Data.SqlClient.SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable(); 
            }
            return GetQuery<TEntity>().OrderByDescending(orderBy).Skip((pageIndex = 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, System.Data.SqlClient.SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Single<TEntity>(criteria);
        }

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().First<TEntity>(predicate);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Where(criteria);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Where(criteria).FirstOrDefault();
        }

        public int Count<TEntity>() where TEntity : class
        {
            return GetQuery<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Count(criteria);
        }

        public DbSet<TEntity> GetEntity<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        #region Transaction Methods

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Add(entity);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Attach(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            string fqen = GetEntityName<TEntity>();

            object orginalItem;
            EntityKey key = ((IObjectContextAdapter)_context).ObjectContext.CreateEntityKey(fqen, entity);
            if(((IObjectContextAdapter)_context).ObjectContext.TryGetObjectByKey(key, out orginalItem))
            {
                ((IObjectContextAdapter)_context).ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            IEnumerable<TEntity> records = Find(criteria);
            foreach (TEntity record in records)
            {
                Delete(record);
            }
        }

        #endregion

        public void Dispose()
        {
            Close();
        }

        #region Private Methods
        
        private EntityKey GetEntityKey<TEntity>(object keyValue) where TEntity : class
        {
            string entitySetName = GetEntityName<TEntity>();
            ObjectSet<TEntity> objectSet =
            ((IObjectContextAdapter)_context).ObjectContext.CreateObjectSet<TEntity>();
            string keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey
            (entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }
        
        private string GetEntityName<TEntity>() where TEntity : class
        {
            // Thanks to Kamyar Paykhan -
            // http://huyrua.wordpress.com/2011/04/13/
            // entity-framework-4-poco-repository-and-specification-pattern-upgraded-to-ef-4-1/
            // #comment-688
            string entitySetName = ((IObjectContextAdapter)_context).ObjectContext.MetadataWorkspace.GetEntityContainer(((IObjectContextAdapter)_context).ObjectContext.DefaultContainerName,DataSpace.CSpace)
                .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(TEntity).Name).First().Name;
            return string.Format("{0}.{1}",((IObjectContextAdapter)_context).ObjectContext.DefaultContainerName, entitySetName);
        }
        
        #endregion

        #region Disposing Methods

        protected void Dispose(bool bDisposing)
        {
            if (!bDisposed)
            {
                if (bDisposing)
                {
                    if (null != _context)
                    {
                        _context.Dispose();
                    }
                }
                bDisposed = true;
            }
        }

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}