﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Исполнитель")]
        public string ImplementerFIO { get; set; }
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }
    }
}
