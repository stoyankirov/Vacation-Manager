namespace VacationManager.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get entity by identifier.
        /// </summary>
        /// <param name="id"></param>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        ///  Get all entities.
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// Add an entity.
        /// <param name="entity">The entity instance.</param>
        /// </summary>
        void AddAsync(TEntity entity);

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Retrieve the count of all entities.
        /// </summary>
        Task<int> CountAllAsync();
    }
}
