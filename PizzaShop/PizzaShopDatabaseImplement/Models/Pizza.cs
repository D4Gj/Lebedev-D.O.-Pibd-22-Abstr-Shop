using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzaShopDatabaseImplement.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        public string PizzaName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("MebelId")]
        public virtual List<PizzaIngridient> PizzaIngridients { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
