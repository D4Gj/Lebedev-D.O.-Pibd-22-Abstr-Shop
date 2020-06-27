using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using PizzaShopBusinessLogic.Attributes;

namespace PizzaShopBusinessLogic.ViewModels
{
    [DataContract]
    public class PizzaViewModel:BaseViewModel
    {
        [Column(title: "Название пиццы", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("Название изделия")]
        public string PizzaName { get; set; }
        [Column(title: "Цена", width: 50)]
        [DataMember]        
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> PizzaIngridients { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "PizzaName",
            "Price"
        };
    }
}
