using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using OfficeWrapper;
using Logger;

namespace PriceUpdate
{
    //? вопрос где хранится наш исходный файл с ценниками
    public class PriceUpdater
    {
        private string pathToFileWithOldPrices;
        private Action<string> exceprionLogger;

        public PriceUpdater(string pathToFileWithOldPrices, Action<string> exceprionLogger = null)
        {
            this.pathToFileWithOldPrices = pathToFileWithOldPrices;
            this.exceprionLogger = exceprionLogger;
        }



        //сравнивает данные двух excel файлов и выводит разницу
        public List<Product> GetDifferenceProductsPrice(string pathToFileWithNewPrices)
        {
            //пути к файлам не те
            if (!File.Exists(pathToFileWithNewPrices))
            {
                exceprionLogger?.Invoke("file path to newPricesFile is not exist");
                //выкидывать исключение
                return null;
            }
                
            if (!File.Exists(pathToFileWithOldPrices))
            {
                exceprionLogger?.Invoke("file path to oldPricesFile is not exist");
                return null;
            }
            
            using ExcelWrapper newExcel = ExcelWrapper.OpenReadExcel(pathToFileWithNewPrices);
            using ExcelWrapper oldExcel = ExcelWrapper.OpenReadExcel(this.pathToFileWithOldPrices);
            
            List<Product> oldProducts = oldExcel.ReadProducts().ToList();
            List<Product> newProducts = newExcel.ReadProducts().ToList();
        
            //выбираем только те продурты на которых изменилась цена

            return newProducts.Except(oldProducts).ToList();


            //foreach(var oldProduct in oldProducts)
            //{
            //    foreach(var newProduct in newProducts)
            //    {
            //        if(oldProduct.Position == newProduct.Position && 
            //            oldProduct.Price != newProduct.Price)
            //        {
            //            yield return newProduct;
            //        }
            //    }
            //}
           // return oldProduct.Union(newProduct).Except(newProduct.Intersect(oldProduct));
        }   

        //печатает эту разницу



    }
}
