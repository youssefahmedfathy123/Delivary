using ApplicationDbcontext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tawsel.Models;

namespace Tawsel.Data
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly TestClass _test;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, TestClass test) : base(options)
        {
            _test = test;
        }

      public DbSet<Product> Products { get; set; }
      public DbSet<Buy> Buys { get; set; }
      public DbSet<Delivary> Delivaries { get; set; }
      public DbSet<Status> Statuses { get; set; }
      public DbSet<Favourite> Favourites { get; set; }

        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_test);
            base.OnConfiguring(optionsBuilder);
        }



    }
}

