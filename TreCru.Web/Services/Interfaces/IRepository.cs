using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TreCru.Web.Services.Interfaces
{
    public interface IRepository<EntityType> : IEnumerable<EntityType>
    {
        bool Any(Expression<Func<EntityType, bool>> predicate);
        int Count(Expression<Func<EntityType, bool>> predicate);
        EntityType First(Expression<Func<EntityType, bool>> predicate);
        IEnumerable<IGrouping<TKey, EntityType>> GroupBy<TKey>(Expression<Func<EntityType, TKey>> predicate);
        IOrderedQueryable<EntityType> OrderBy<TKey>(Expression<Func<EntityType, TKey>> predicate);
        void Insert(EntityType entity);
        void Insert(IEnumerable<EntityType> entities);

        void Save();
        void SaveAsync();

        void Remove(EntityType entity);
        void Remove(Expression<Func<EntityType, bool>> predicate);

        IEnumerable<TResult> Select<TResult>(Expression<Func<EntityType, TResult>> predicate);
        IEnumerable<EntityType> Where(Expression<Func<EntityType, bool>> predicate);
        IEnumerable<EntityType> GetAll();
    }
}