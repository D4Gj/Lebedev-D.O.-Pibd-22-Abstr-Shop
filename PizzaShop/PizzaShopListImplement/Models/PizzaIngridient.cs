using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopListImplement.Models
{
    public class PizzaIngridient
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngridientID { get; set; }
        public int Count { get; set; }
    }
}
