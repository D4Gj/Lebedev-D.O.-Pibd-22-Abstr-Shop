using System;
using PizzaShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzaShopDatabaseImplement
{
    public class PizzaShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0S6SE5G\SQLEXPRESS;Initial Catalog=PizzaShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Ingridient> Ingridients { set; get; }
        public virtual DbSet<Pizza> Pizzas { set; get; }
        public virtual DbSet<PizzaIngridient> PizzaIngridients { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<StorageIngridient> StorageIngridients { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
    }
}
