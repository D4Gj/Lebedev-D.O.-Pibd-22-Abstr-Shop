using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaShopDatabaseImplement.Models
{
    public class StorageIngridient
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int IngridientId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Ingridient Ingridient { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
