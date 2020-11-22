using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager(new UnitOfWork(new DataContext("Production")));

            //manager.AddCustomer("Plop de Plopper", "Le Rue Du PlopMelk 101 France");
            //manager.AddCustomer("Klus de Klusser", "Le Rue Du Fromage 85 France");

            //manager.AddOrder(1, ProductType.Duvel, 5);
            //manager.AddOrder(1, ProductType.Leffe, 50);

            //manager.AddOrder(2, ProductType.Duvel, 5);
            ////manager.AddOrder(2, ProductType.Duvel, 20);
            
            foreach(Order item in manager.GetOrdersFromCustomer(2))
            {
                Console.WriteLine(item.Amount);
            }
        }
    }
}
