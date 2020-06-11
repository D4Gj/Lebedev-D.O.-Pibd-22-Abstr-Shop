using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using PizzaShopDatabaseImplement.Implements;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.BusinessLogic;


namespace PizzaAbstractShopView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IIngridientLogic, IngridientLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaShopLogic, PizzaLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
