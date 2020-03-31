using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.Enums;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string PizzaName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
    }
}
