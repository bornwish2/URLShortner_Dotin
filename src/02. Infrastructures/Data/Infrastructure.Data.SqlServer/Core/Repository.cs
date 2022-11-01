using Framework.ApplicationServices.Data;
using Framework.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SqlServer.Core
{
    public class Repository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        protected readonly ShortUrlContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ShortUrlContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        #region Async Method

        ///// <summary>
        ///// returns a collection of entities  (Async)
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public virtual async Task<ICollection<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
        //{
        //    return await Entities.ToListAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// finds by Id (Async)
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        //{
        //    return Entities.FindAsync(ids, cancellationToken);
        //}
        ///// <summary>
        ///// adds entity (Async)
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// adds range (Async)
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// updates the entity (Async)
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    Entities.Update(entity);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// updates by Range  (Async)
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    Entities.UpdateRange(entities);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// deletes the entity (Async)
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    Entities.Remove(entity);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        ///// <summary>
        ///// updates by Range  (Async)
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="saveNow"></param>
        ///// <returns></returns>
        //public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    Entities.RemoveRange(entities);
        //    if (saveNow)
        //        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}
        #endregion

        #region Sync Methods

        ///// <summary>
        ///// gets a collection of entities
        ///// </summary>
        ///// <returns></returns>
        //public virtual ICollection<TEntity> GetEntities()
        //{
        //    return Entities.ToList();
        //}
        ///// <summary>
        ///// gets entity by id 
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public virtual TEntity GetById(params object[] ids)
        //{
        //    return Entities.Find(ids);
        //}
        ///// <summary>
        ///// adds the entity
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="saveNow"></param>
        //public virtual void Add(TEntity entity, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    Entities.Add(entity);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        ///// <summary>
        ///// adds range
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="saveNow"></param>
        //public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    Entities.AddRange(entities);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        ///// <summary>
        ///// updates the entity
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="saveNow"></param>
        //public virtual void Update(TEntity entity, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    Entities.Update(entity);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        ///// <summary>
        ///// updates Range
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="saveNow"></param>
        //public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    Entities.UpdateRange(entities);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        ///// <summary>
        ///// deletes
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="saveNow"></param>
        //public virtual void Delete(TEntity entity, bool saveNow = true)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    Entities.Remove(entity);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        ///// <summary>
        /////updates by Range 
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="saveNow"></param>
        //public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        //{
        //    Assert.NotNull(entities, nameof(entities));
        //    Entities.RemoveRange(entities);
        //    if (saveNow)
        //        DbContext.SaveChanges();
        //}
        #endregion

        #region Attach & Detach
        /// <summary>
        /// detch the entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }
        /// <summary>
        /// attach the entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        /// <summary>
        /// Loads Collection Async
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Loads Collection
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }
        /// <summary>
        /// Loads Reference Async
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Loads Reference
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion
    }
}
