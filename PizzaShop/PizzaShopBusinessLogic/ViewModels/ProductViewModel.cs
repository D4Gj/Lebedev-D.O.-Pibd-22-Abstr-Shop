using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace PizzaShopBusinessLogic.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<ProductComponentViewModel> ProductComponents { get; set; }
    }
}
