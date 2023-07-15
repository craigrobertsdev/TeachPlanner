using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : class {
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

    TEntity GetById(int id);

    void Insert(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
