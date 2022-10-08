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

            List<Product> excelProducts = ExcelWrapper.OpenReadExcel(pathToExcelFile).ReadProductsFromABColumns().ToList();
            List<Product> dbProducts = productsDb.GetProducts().ToList();

            List<Product> differenceProducts = CompareProducts.GetDifferenceProductsPrice(excelProducts, dbProducts);

            //создаётся директория
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(folderPath, "PriceUpdate"));

            //задаётся путь к файлу
            string pathToFile = Path.Combine(directoryInfo.FullName,  "cписок обновлённых продуктов"
                + ((int)directoryInfo.GetFiles().Length + 1));

            ExcelWrapper.CreateFileExcel(pathToFile).SaveFileWithProducts(differenceProducts);

            productsDb.LoadToProducts(excelProducts, true);
            logger?.Invoke("процесс успешно отработал");
        }

    }
}
