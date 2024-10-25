using E_Commerse2.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse2.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Company> companies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build()
            .GetConnectionString("DefualtConection");
            optionsBuilder.UseSqlServer(builder);

        }
    }
}
