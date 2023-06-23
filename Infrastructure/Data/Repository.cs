using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;
public class Repository<TEntity> where TEntity : class {
    internal ApplicationDbContext _applicationDbContext;
    internal DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext applicationDbContext) {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        string includeProperties = "") {
        IQueryable<TEntity> query = _dbSet; 

        if (filter != null) {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
            query = query.Include(includeProperty);
        }

        if (orderBy != null) {
            return orderBy(query).ToList();
        }
        else {
            return query.ToList();
        }
    }

    public virtual TEntity GetById(int id) {
        return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity) {
        _dbSet.Add(entity);
    }

    public virtual void Delete(object id) {
        TEntity entity = _dbSet.Find(id);
        _dbSet.Remove(entity);
    }

    public virtual void Delete(TEntity entityToDelete) {
        if (_applicationDbContext.Entry(entityToDelete).State == EntityState.Detached) {
            _dbSet.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate) {
        _dbSet.Attach(entityToUpdate);
        _applicationDbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
