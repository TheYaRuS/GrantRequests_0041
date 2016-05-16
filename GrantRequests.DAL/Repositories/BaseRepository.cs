using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GrantRequests.DAL.Repositories
{
    public class BaseRepository<TEntity, TContext> where TEntity : class
                                                   where TContext : DbContext
    {
        protected BaseRepository(TContext db)
        {
            this.db = db;
        }
        protected TContext db;
        public virtual IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public virtual TEntity Get(int id)
        {

            return db.Set<TEntity>().Find(id);
        }
        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return db.Set<TEntity>().Where(predicate).ToList();
        }
        public virtual void Create(TEntity item)
        {
            db.Set<TEntity>().Add(item);
        }
        public virtual void Update(TEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public virtual void Delete(int id)
        {
            TEntity item = db.Set<TEntity>().Find(id);
            if (item != null)
                db.Entry(item).State = EntityState.Deleted;
        }
    }
}
