using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopListImplement.Models
{
    public class StorageIngridient
    {
        public int Id { set; get; }
        public int StorageId { set; get; }
        public int MaterialId { set; get; }
        public int Count { set; get; }
    }
}
