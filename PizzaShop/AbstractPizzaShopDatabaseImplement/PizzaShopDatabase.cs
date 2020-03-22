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
                optionsBuilder.UseSqlServer(@"Data Source=HOME\SQLEXPRESS;Initial
            Catalog=AbstractShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Ingridient> Ingridients { set; get; }
        public virtual DbSet<Pizza> Pizzas { set; get; }
        public virtual DbSet<PizzaIngridient> ProductComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }

    }
}
