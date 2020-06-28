using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopRestApi.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }

        public string PizzaName { get; set; }

        public decimal Price { get; set; }
    }
}
