using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        MyEshopContext _context = new MyEshopContext();
        DbSet<TEntity> _dbSet;

        public GenericRepository(MyEshopContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string Includes = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (Includes != "")
            {
                foreach (var includes in Includes.Split(','))
                {
                    query = query.Include(includes);
                }
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        public virtual void Delete(object id)
        {
            var Find = GetById(id);
            Delete(Find);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }




    }
}
