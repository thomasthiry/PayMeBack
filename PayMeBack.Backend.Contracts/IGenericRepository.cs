using PayMeBack.Backend.Models;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get();
        TEntity GetByID(int id);
        TEntity Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
