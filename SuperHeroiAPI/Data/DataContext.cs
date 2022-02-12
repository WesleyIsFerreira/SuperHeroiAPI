using Microsoft.EntityFrameworkCore;

namespace SuperHeroiAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHeroi> superHerois { get; set; }
    }
}
