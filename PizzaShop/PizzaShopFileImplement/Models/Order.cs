using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.Enums;

namespace PizzaShopFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int? ImplementerId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public int ClientId { set; get; }
    }
}
