using E_Commerse2.Migrations;
using E_Commerse2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using E_Commerse2.ViewModel;

namespace E_Commerse2.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }
        public ApplicationDbContext()
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build()
            .GetConnectionString("DefualtConection");
            optionsBuilder.UseSqlServer(builder);

        }
        public DbSet<E_Commerse2.ViewModel.ApplicationUserVM> ApplicationUserVM { get; set; } = default!;
        public DbSet<E_Commerse2.ViewModel.LoginVM> LoginVM { get; set; } = default!;
    }
}
