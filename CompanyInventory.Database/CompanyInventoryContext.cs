using CompanyInventory.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Database
{
    public class CompanyInventoryContext : DbContext
    {
        public CompanyInventoryContext(DbContextOptions<CompanyInventoryContext> options) 
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(500)
                    .IsRequired(true);
              
                entity.Property(p => p.FoundationYear)
                    .IsRequired(true);
            });
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(p => p.FirstName)
                    .HasMaxLength(500)
                    .IsRequired(true);
              
                entity.Property(p => p.LastName)
                    .HasMaxLength(500)
                    .IsRequired(true);
                
                entity.Property(p => p.Position)
                    .HasColumnType("tinyint")
                    .IsRequired(true);
                
                entity.Property(p => p.BirthDate)
                    .HasColumnType("date")
                    .IsRequired(true);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}