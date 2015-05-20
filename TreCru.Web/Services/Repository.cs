using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TreCru.Web.Services.Interfaces;

namespace TreCru.Web.Services
{
    public class Repository<ContextType, EntityType> : IRepository<EntityType>, IDisposable
        where ContextType : DbContext
        where EntityType : class
    {

        private readonly ContextType _context;

        public Repository(ContextType context)
        {
            _context = context;
        }

        public virtual bool Any(Expression<Func<EntityType, bool>> predicate)
        {
            return _context.Set<EntityType>().Any(predicate);
        }

        public virtual int Count(Expression<Func<EntityType, bool>> predicate)
        {
            return _context.Set<EntityType>().Count(predicate);
        }

        public virtual EntityType First(Expression<Func<EntityType, bool>> predicate)
        {
            return _context.Set<EntityType>().First(predicate);
        }

        public virtual IEnumerable<IGrouping<TKey, EntityType>> GroupBy<TKey>(Expression<Func<EntityType, TKey>> predicate)
        {
            return _context.Set<EntityType>().GroupBy(predicate);
        }

        public virtual IOrderedQueryable<EntityType> OrderBy<TKey>(Expression<Func<EntityType, TKey>> predicate)
        {
            return _context.Set<EntityType>().OrderBy(predicate);
        }

        public virtual void Insert(EntityType entity)
        {
            _context.Set<EntityType>().Add(entity);
        }

        public virtual void Insert(IEnumerable<EntityType> entities)
        {
            _context.Set<EntityType>().AddRange(entities);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public virtual void Remove(EntityType entity)
        {
            _context.Set<EntityType>().Attach(entity);
            _context.Set<EntityType>().Remove(entity);
        }

        public virtual void Remove(Expression<Func<EntityType, bool>> predicate)
        {
            _context.Set<EntityType>().RemoveRange(Where(predicate));
        }

        public virtual IEnumerable<TResult> Select<TResult>(Expression<Func<EntityType, TResult>> predicate)
        {
            return _context.Set<EntityType>().Select(predicate);
        }

        public virtual IEnumerable<EntityType> Where(Expression<Func<EntityType, bool>> predicate)
        {
            return _context.Set<EntityType>().Where(predicate);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual IEnumerator<EntityType> GetEnumerator()
        {
            return _context.Set<EntityType>().AsEnumerable<EntityType>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerable<EntityType> GetAll()
        {
            return _context.Set<EntityType>().ToList();
        }

    }
}