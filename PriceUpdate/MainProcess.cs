using Models;
using OfficeWrapper;
using DbApi.api;

namespace PriceUpdate
{
    public class MainProcess
    {
        public void RunProcessing(string pathToExcelFile, Action<string> logger = null)
        {
            ProductsDb productsDb = new ProductsDb(logger);

            List<Product> excelProducts = ExcelWrapper.OpenReadExcel(pathToExcelFile).ReadProducts().ToList();
            List<Product> dbProducts = productsDb.GetProducts().ToList();

            List<Product> differenceProducts = CompareProducts.GetDifferenceProductsPrice(excelProducts, dbProducts);

            ExcelWrapper.CreateAndSaveFileWithProducts(differenceProducts);

            productsDb.LoadToProducts(excelProducts, true);
            logger?.Invoke("процесс успешно отработал");
        }

    }
}
