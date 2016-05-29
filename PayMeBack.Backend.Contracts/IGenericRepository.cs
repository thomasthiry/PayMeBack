using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PayMeBack.Backend.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null);
        IList<TEntity> GetWithIncludedProperties(Expression<Func<TEntity, IEntity>> includeProperties, Expression<Func<TEntity, bool>> filter = null);
        TEntity GetById(int id);
        TEntity Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
