using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DbApi
{
    public static class ProductsDbApi
    {
        //•добавить метод loadToDb(IEnumerable<Product> ...)

        public static void LoadToProducts(IEnumerable<Product> products, bool isDeleteDataFromProducts)
        {
            using(ApplicationContext dbContext = new ApplicationContext())
            {
                if(products.Distinct().Count() > products.Count())
                {
                    //логгировать
                }

                if (isDeleteDataFromProducts)
                    dbContext.Products.RemoveRange(dbContext.Products);

                //записываем только уникальные
                dbContext.Products.AddRange(products.Distinct());
               
                dbContext.SaveChanges();
                //логгировать
            }
        }

        public static IEnumerable<Product> GetProducts()
        {
            using(ApplicationContext dbContext = new ApplicationContext())
            {
                return dbContext.Products.Select(x => x).ToList();
            }
        }


    }
}
