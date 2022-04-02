using JCsDiner;
using Microsoft.EntityFrameworkCore;

namespace DinerBlazorServer
{
    public class DinerDbContext : DbContext
    {
        public DbSet<SimulatorResults> Results { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DinerDB.db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SimulatorResults>().ToTable("Results");
        }
    }
}
