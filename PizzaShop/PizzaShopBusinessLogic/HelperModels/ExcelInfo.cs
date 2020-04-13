using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersViewModel> Orders { get; set; }
        public List<ReportPizzaOrdersViewModel> Pizzas { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
