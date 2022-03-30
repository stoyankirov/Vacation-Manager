namespace VacationManager.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VacationManager.Data.Contracts;

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected VacationManagerContext DbContext { get; }

        protected DbSet<TEntity> Entities { get; }

        protected Repository(VacationManagerContext dbContext)
        {
            DbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
        }

        public virtual void AddAsync(TEntity entity)
        {
            this.Entities.Add(entity);

            this.DbContext.SaveChangesAsync();
        }

        public Task<int> CountAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
            => this.Entities.ToListAsync();


        public virtual Task<TEntity> GetByIdAsync(Guid id)
            => this.Entities.FindAsync(id).AsTask();

        public Task UpdateAsync(TEntity entity)
        {
            this.Entities.Update(entity);

            return this.DbContext.SaveChangesAsync();
        }
    }
}
