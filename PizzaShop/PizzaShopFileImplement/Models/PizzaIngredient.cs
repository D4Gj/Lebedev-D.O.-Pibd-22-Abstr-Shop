using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopFileImplement.Models
{
    public class PizzaIngredient
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngridientId { get; set; }
        public int Count { get; set; }
    }
}
