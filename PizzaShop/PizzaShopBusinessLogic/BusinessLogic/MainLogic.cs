using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Enums;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.HelperModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IClientLogic clientLogic;
		private readonly IStorageLogic storageLogic;
        private readonly object locker = new object();
        public MainLogic(IOrderLogic orderLogic,IClientLogic clientLogic,IStorageLogic storageLogic)
        {
            this.orderLogic = orderLogic;
            this.clientLogic = clientLogic;
			this.storageLogic = storageLogic;
        }
        
        
        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                PizzaId = model.PizzaId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = model.ClientId })?[0]?.Login,
                Subject = $"Новый заказ",
                Text = $"Заказ принят."
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = orderLogic.Read(new OrderBindingModel
                {
                    Id = model.OrderId
                })?[0];
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят)
                {
                    throw new Exception("Заказ не в статусе \"Принят\"");
                }
                if (order.ImplementerId.HasValue)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }
                orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    Id = order.Id,
                    PizzaId = order.PizzaId,
                    ClientId = order.ClientId,
                    ImplementerId = model.ImplementerId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = DateTime.Now,
                    Status = OrderStatus.Выполняется
                });
                MailLogic.MailSendAsync(new MailSendInfo
                {
                    MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                    Subject = $"Заказ №{order.Id}",
                    Text = $"Заказ №{order.Id} передан в работу."
                });
            }
            try
            {
                storageLogic.RemoveFromStorage(order.PizzaId, order.Count);
                orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    Id = order.Id,
                    PizzaId = order.PizzaId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = DateTime.Now,
                    Status = OrderStatus.Выполняется
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} готов."
            });
        }
        public void ReplenishStorage(StorageIngridientBindingModel model)
        {
            storageLogic.ReplenishStorage(model);
        }
    }
}
