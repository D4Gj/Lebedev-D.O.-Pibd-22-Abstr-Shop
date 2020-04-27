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
        public List<PizzaIngridient> PizzaIngredients { get; set; }
        public List<Client> Clients { get; set; }
        private DataListSingleton()
        {
            Ingridients = new List<Ingridient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
            PizzaIngredients = new List<PizzaIngridient>();
            Clients = new List<Client>();
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
