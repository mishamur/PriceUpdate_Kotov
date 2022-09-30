using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
namespace DbApi
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public ApplicationContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //очень глупо выбирать первичным ключом строку, ну а что поделать
            modelBuilder.Entity<Product>().HasKey(product => new { product.Position });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductsDb;Username=postgres;Password={p@ssw0rd}");
        }


    }
}
