using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PizzaShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ProductComponentViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PizzaId { get; set; }
        [DataMember]
        public int IngridientId { get; set; }
        [DataMember]
        [DisplayName("Ингридиент")]
        public string IngridientName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
