using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PizzaShopBusinessLogic.Enums;

namespace PizzaShopDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }

        public int ClientId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public Pizza Pizza { get; set; }
        public Client Client { get; set; }
    }
}
