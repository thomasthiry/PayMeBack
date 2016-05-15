using System.Linq;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts;

namespace PayMeBack.Backend.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        internal PayMeBackContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(PayMeBackContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null/*,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, IEntity>> includeProperties = null*/)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //if (orderBy != null)
            //{
            //    return orderBy(query).ToList();
            //}
            //else
            //{
            return query.ToList();
            //}
        }

        public virtual IList<TEntity> GetWithIncludedProperties(Expression<Func<TEntity, IEntity>> includeProperties, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }

            return query.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.SingleOrDefault(e => e.Id == id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = GetById(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}