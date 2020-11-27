using CompanyInventory.Database;

namespace CompanyInventory.Repository.Repositories
{
    public abstract class BaseRepository
    {
        public CompanyInventoryContext Context { get; }

        public BaseRepository(CompanyInventoryContext context)
        {
            Context = context;
        }
    }
}