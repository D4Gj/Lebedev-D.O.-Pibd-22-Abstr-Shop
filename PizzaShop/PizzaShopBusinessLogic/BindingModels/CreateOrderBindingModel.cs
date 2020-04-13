using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int PizzaId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
