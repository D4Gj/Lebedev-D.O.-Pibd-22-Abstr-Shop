using System;
using PizzaShopListImplement.Models;
using System.Collections.Generic;

namespace PizzaShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Ingridient> Ingridients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<PizzaIngridient> PizzaIngridients { get; set; }
        public List<Storage> Storages { get; set; }
        public List<StorageIngridient> StorageIngridients { get; set; }
        private DataListSingleton()
        {
            Ingridients = new List<Ingridient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
            PizzaIngridients = new List<PizzaIngridient>();
            Storages = new List<Storage>();
            StorageIngridients = new List<StorageIngridient>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
