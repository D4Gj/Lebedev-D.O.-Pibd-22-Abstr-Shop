using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string PizzaName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PizzaIngridients { get; set; }
    }
}
