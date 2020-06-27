using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PizzaShopDatabaseImplement.Models
{
    public class Ingridient
    {
        public int Id { get; set; }
        [Required]
        public string IngridientName { get; set; }
        [ForeignKey("IngridientId")]
        public virtual List<Ingridient> Ingridients { get; set; }
        [ForeignKey("IngridientId")]
        public virtual List<StorageIngridient> StorageIngridients { get; set; }
    }
}
