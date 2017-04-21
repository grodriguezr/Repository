using Modelo;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork Repository = new Modelo.RepositoryUoW();
            var Categories = Repository.FindEntitySet<Category>(c => true);
            var Category1 = Repository.Create(new Category
            {
                CategoryName = "CatDemoB"
            });//no se coloca la entidad ya que .NET hace una inferencia de la entidad que se les está pasando al método
            var Customer1 = Repository.Create(new Customer
            {
                CustomerID = "ABCDb",
                CompanyName = "CNABCD"
            });

            var Changes = Repository.Save();
            Console.Write("Presiona <enter> para finalizar, cambios:" + Changes);
            Console.ReadLine();

        }

        private static void SinTrans()
        {
            IRepository Repository = new Modelo.Repository();
            var Categories = Repository.FindEntitySet<Category>(c => true);
            var Category1 = Repository.Create(new Category
            {
                CategoryName = "CatDemoA"
            });
            var Customer1 = Repository.Create(new Customer
            {
                CustomerID = "CA",
                CompanyName = "CNABC"
            });
            
        }
    }
}
