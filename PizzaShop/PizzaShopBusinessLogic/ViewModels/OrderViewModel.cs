using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.Enums;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        [DisplayName("Изделие")]
        public string PizzaName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
