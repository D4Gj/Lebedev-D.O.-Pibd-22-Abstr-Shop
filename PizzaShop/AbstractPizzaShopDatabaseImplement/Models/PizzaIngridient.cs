﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace PizzaShopDatabaseImplement.Models
{
    public class PizzaIngridient
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngridientId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Ingridient Ingridient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
