using PizzaShopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PizzaShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {

        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string FIO { get; set; }
        [Column(title: "Почта", width: 150)]
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FIO",
            "Login"
        };
    }
}
