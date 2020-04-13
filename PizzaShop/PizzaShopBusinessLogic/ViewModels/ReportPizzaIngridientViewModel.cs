using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class ReportPizzaIngridientViewModel
    {
        public string PizzaName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Pizzas { get; set; }
    }
}
