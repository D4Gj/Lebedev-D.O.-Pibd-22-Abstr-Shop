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
        public List<ReportPizzaIngridientViewModel> PizzaIngridients { get; set; }
    }
}
