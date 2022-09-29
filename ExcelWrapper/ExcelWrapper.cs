using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Models;
using Microsoft.Office.Interop.Excel;

namespace OfficeWrapper
{
    public class ExcelWrapper : IDisposable
    {
        //поля
        Excel.Application application = null;
        Excel.Workbook workbook = null;
        Excel.Worksheet worksheet = null;

        private ExcelWrapper(Application application, Workbook workbook, Worksheet worksheet)
        {
            this.application = application;
            this.workbook = workbook;
            this.worksheet = worksheet;
        }

        
        public static ExcelWrapper OpenReadExcel(string filePath)
        {

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                application = new Excel.Application();
                workbook = application.Workbooks.Open(Filename: filePath, ReadOnly: true); ;
                worksheet = workbook.ActiveSheet;
            }
            catch
            {
                RealeseComObjects(worksheet, workbook, application);
                throw;
            }
            return new ExcelWrapper(application, workbook, worksheet);
        }

        /// <summary>
        /// Прочитать список продуктов из excel файла
        /// </summary>
        /// <returns>Список продуктов</returns>
        public IEnumerable<Product> ReadProducts()
        {
            int i = 1;
            var productName = ((Excel.Range)worksheet.Cells[i, "A"]).Value2;
            var productPrice = ((Excel.Range)worksheet.Cells[i, "B"]).Value2;


            while (productName != null && productPrice != null)
            {
                yield return new Product(productName, decimal.Parse(productPrice.ToString()));
                i++;
                productName = ((Excel.Range)worksheet.Cells[i, "A"]).Value2;
                productPrice = ((Excel.Range)worksheet.Cells[i, "B"]).Value2;
            }
        }

        /// <summary>
        /// Создает и заполняет excel-файл заданным перечислением
        /// </summary>
        /// <param name="products"></param>
        /// <returns>Путь к файлу</returns>
        public static string CreateAndSaveFileWithProducts(IEnumerable<Product> products)
        {

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                application = new Excel.Application();
                workbook = application.Workbooks.Add(1);
                worksheet = workbook.Sheets[1];
            }
            catch
            {
                RealeseComObjects(worksheet, workbook, application);
                throw;
            }
             worksheet.Name = "Список обновлённых товаров";
            
            int i = 1;
            foreach(Product product in products)
            {
                worksheet.Cells[i, "A"] = product.Position.ToString();
                worksheet.Cells[i, "B"] = product.Price.ToString();
                i++;
            }
            //создаётся директория
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            DirectoryInfo directoryInfo =  Directory.CreateDirectory(Path.Combine(folderPath, "PriceUpdate"));
            
            //задаётся путь к файлу
            string pathToFile = Path.Combine(directoryInfo.FullName, worksheet.Name + ((int)directoryInfo.GetFiles().Length + 1));
            workbook.SaveAs(pathToFile);

            RealeseComObjects(worksheet, workbook, application);
            return pathToFile;
        }

        /// <summary>
        /// Очищает ссылки заданных com объектов
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="workbook"></param>
        /// <param name="application"></param>
        private static void RealeseComObjects(Excel.Worksheet worksheet, Excel.Workbook workbook, Excel.Application application)
        {
            if (worksheet != null)
            {
                while (Marshal.FinalReleaseComObject(worksheet) != 0);
                worksheet = null;
            }

            if (workbook != null)
            {
                workbook.Close();
                while (Marshal.FinalReleaseComObject(workbook) != 0) ;
                workbook = null;
            }

            if (application.Workbooks != null)
            {
                application.Workbooks.Close();
                while (Marshal.FinalReleaseComObject(application.Workbooks) != 0) ;
            }

            if (application != null)
            {
                application.Quit();
                while (Marshal.FinalReleaseComObject(application) != 0) ;
                application = null;
            }
        }

        public void Dispose()
        {
            RealeseComObjects(this.worksheet, this.workbook, this.application);
        }

    }
}
