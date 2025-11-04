using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products => Set<Product>();                               //why not {get;set;} ?
    }
}
