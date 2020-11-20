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

            manager.AddOrder(1, ProductType.Duvel, 5);
            manager.AddOrder(1, ProductType.Leffe, 50);

            manager.AddOrder(2, ProductType.Duvel, 5);
            manager.AddOrder(2, ProductType.Duvel, 20);
        }
    }
}
