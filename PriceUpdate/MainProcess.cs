using Models;
using OfficeWrapper;
using DbApi;

namespace PriceUpdate
{
    public class MainProcess
    {

        public void RunProcessing(string pathToExcelFile)
        {

            List<Product> excelProducts = ExcelWrapper.OpenReadExcel(pathToExcelFile).ReadProducts().ToList();
            List<Product> dbProducts = ProductsDbApi.GetProducts().ToList();

            List<Product> differenceProducts = PriceUpdater.GetDifferenceProductsPrice(excelProducts, dbProducts);

            ExcelWrapper.CreateAndSaveFileWithProducts(differenceProducts);

            ProductsDbApi.LoadToProducts(excelProducts, true);


            //проверка
            Console.WriteLine("данные которые были в базе данных");

            foreach (var product in dbProducts)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("данные которые были в excel файле");
            foreach (var product in excelProducts)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("данные которые были обновлены");
            foreach (var product in differenceProducts)
            {
                Console.WriteLine(product);
            }
        }

    }
}
