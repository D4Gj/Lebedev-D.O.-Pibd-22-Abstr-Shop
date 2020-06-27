using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<IGrouping<DateTime, OrderViewModel>> Orders { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
