using Models;
using OfficeWrapper;
using DbApi.api;
using Interfaces;
using PriceUpdate.ConfigSettings;

namespace PriceUpdate
{
    public class MainProcess
    {
        /// <summary>
        /// Запустить основной процесс
        /// </summary>
        /// <param name="pathToExcelFile">Путь к excel файлу с новыми продуктами</param>
        /// <param name="logger">Логер</param>
        public void RunProcessing(ISettings settings, Action<string> logger = null)
        {
            string pathToExcelFile = settings.GetValue("pathToExcelFile")?.ToString();


            ProductsDb productsDb = new ProductsDb(logger);

            List<Product> excelProducts = new List<Product>();
            using (ExcelWrapper openRead = ExcelWrapper.OpenReadExcel(pathToExcelFile))
            {
                excelProducts = openRead.ReadProductsFromABColumns().ToList();
            }

            List<Product> dbProducts = productsDb.GetProducts().ToList();
            List<Product> differenceProducts = CompareProducts.GetDifferenceProductsPrice(excelProducts, dbProducts);

            string outputFolderPath = settings.GetValue("outputDirectory")?.ToString();
            Directory.CreateDirectory(outputFolderPath);
            DirectoryInfo outputDirectory = new DirectoryInfo(outputFolderPath);
            string fileName = "список обновлённых продуктов";
            //задаётся путь к файлу
            string pathToFile = Path.Combine(outputFolderPath, fileName);

            using(ExcelWrapper createFile = ExcelWrapper.CreateFileExcel(pathToFile))
            {
                createFile.SaveFileWithProducts(differenceProducts);
            }

            productsDb.LoadToProducts(excelProducts, true);
            logger?.Invoke("процесс успешно отработал");
        }

    }
}