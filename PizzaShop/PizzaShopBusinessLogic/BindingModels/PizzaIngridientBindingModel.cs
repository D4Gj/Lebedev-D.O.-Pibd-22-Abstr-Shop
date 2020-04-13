using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopBusinessLogic.BindingModels
{
    public class ProductComponentBindingModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngridientId { get; set; }
        public int Count { get; set; }
    }
}
