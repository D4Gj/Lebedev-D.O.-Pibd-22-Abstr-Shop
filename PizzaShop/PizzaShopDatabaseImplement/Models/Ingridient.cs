using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzaShopDatabaseImplement.Models
{
    public class Ingridient
    {
        public int Id { get; set; }
        [Required]
        public string IngridientName { get; set; }
        [ForeignKey("IngridientId")]
        public virtual List<PizzaIngridient> PizzaIngridients { get; set; }
        [ForeignKey("IngridientId")]
        public virtual List<StorageIngridient> StorageIngridients { get; set; }
    }
}
