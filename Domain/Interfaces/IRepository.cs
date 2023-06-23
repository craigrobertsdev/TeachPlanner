using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IRepository<TEntity> where TEntity : class {
    DbSet<TEntity> Entities { get; }
    DbContext Context { get; }

    /// <summary>
    /// Get all items of an entity by asynchronous method
    /// </summary>
    /// <returns></returns>
    Task<IList<TEntity>> GetAllAsync();
    /// <summary>
    /// Fin one item of an entity synchronous method
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    TEntity Find(params object[] keyValues);
    /// <summary>
    /// Find one item of an entity by asynchronous method 
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(params object[] keyValues);
    /// <summary>
    /// Insert item into an entity by asynchronous method
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <returns></returns>
    Task InsertAsync(TEntity entity, bool saveChanges = true);
    /// <summary>
    /// Insert multiple items into an entity by asynchronous method
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="saveChanges"></param>
    /// <returns></returns>
    Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true);
    /// <summary>
    /// Remove one item from an entity by asynchronous method
    /// </summary>
    /// <param name="id"></param>
    /// <param name="saveChanges"></param>
    /// <returns></returns>
    Task DeleteAsync(int id, bool saveChanges = true);
    /// <summary>
    /// Remove one item from an entity by asynchronous method
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity, bool saveChanges = true);
    /// <summary>
    /// Remove multiple items from an entity by asynchronous method
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="saveChanges"></param>
    /// <returns></returns>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true);

}
