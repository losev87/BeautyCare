using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using IntraVision.Data;

namespace IntraVision.Repository
{
    public interface IRepository<TEntity> : IDisposable, IRepository
        where TEntity : class, IEntityBase
    {
        IQueryable<TEntity> GetQuery();
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(int id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(int id);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        TEntity First(int id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        void BatchUpdate(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression);
        void BatchDelete(IQueryable<TEntity> queryable);
        void ReferenceLoad(TEntity entity, params string[] reference);
        void Delete(TEntity entity);
        void BatchDelete(Expression<Func<TEntity, bool>> expression);
        void Attach(TEntity entity);
        void SaveChanges();
    }

    public interface IRepository
    {
        object GetEntitiesByIds(IEnumerable<int> ids);
        IEnumerable<IEntityBase> GetEntityBases();
        IEnumerable<INamedEntityBase> GetNamedEntityBases();
    }
}
