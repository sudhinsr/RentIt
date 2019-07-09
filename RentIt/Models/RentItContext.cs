using Microsoft.EntityFrameworkCore;

namespace RentIt.Models
{
    public class RentItContext : DbContext
    {
        public RentItContext(DbContextOptions<RentItContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
