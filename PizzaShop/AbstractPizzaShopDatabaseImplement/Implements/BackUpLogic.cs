using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BusinessLogic;
using System.Linq;
using System.Reflection;

namespace PizzaShopDatabaseImplement.Implements
{
    public class BackUpLogic : BackUpAbstractLogic
    {
        protected override Assembly GetAssembly()
        {
            return typeof(BackUpLogic).Assembly;
        }
        protected override List<PropertyInfo> GetFullList()
        {
            using (var context = new PizzaShopDatabase())
            {
                Type type = context.GetType();
                return type.GetProperties().Where(x =>
               x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
            }
        }
        protected override List<T> GetList<T>()
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
