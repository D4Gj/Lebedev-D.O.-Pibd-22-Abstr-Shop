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
        public List<PizzaIngredient> PizzaIngredients { get; set; }
        private DataListSingleton()
        {
            Ingridients = new List<Ingridient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
            PizzaIngredients = new List<PizzaIngredient>();
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
