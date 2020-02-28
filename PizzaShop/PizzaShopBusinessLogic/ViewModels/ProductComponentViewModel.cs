using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class ProductComponentViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ComponentId { get; set; }
        [DisplayName("Компонент")]
        public string ComponentName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
