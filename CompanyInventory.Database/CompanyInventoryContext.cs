using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Database
{
    public class CompanyInventoryContext : DbContext
    {
        public CompanyInventoryContext(DbContextOptions<CompanyInventoryContext> options) 
            : base(options)
        {
        }
    }
}