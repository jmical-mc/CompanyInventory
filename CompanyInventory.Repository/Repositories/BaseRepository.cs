using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CompanyInventory.Database;
using CompanyInventory.Database.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CompanyInventory.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public CompanyInventoryContext Context { get; }

        public BaseRepository(CompanyInventoryContext context)
        {
            Context = context;
        }

        public async Task<EntityEntry<TEntity>> UpdateAsync([NotNull] TEntity entity)
            => await Task.Run(() => Context.Set<TEntity>().Update(entity));

        public async ValueTask<EntityEntry<TEntity>> AddAsync([NotNull] TEntity entity,
            CancellationToken cancellationToken = default)
            => await Context.AddAsync(entity, cancellationToken);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await Context.SaveChangesAsync(cancellationToken);

        public async Task<EntityEntry<TEntity>> RemoveAsync([NotNull] TEntity entity)
            => await Task.Run(() => Context.Set<TEntity>().Remove(entity));
    }
}