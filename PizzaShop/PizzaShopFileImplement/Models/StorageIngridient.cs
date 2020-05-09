using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopFileImplement.Models
{
    public class StorageIngridient
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int IngridientId { get; set; }
        public int Count { get; set; }
    }
}
