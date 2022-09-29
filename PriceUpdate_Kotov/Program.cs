using OfficeWrapper;
using Models;
using PriceUpdate;
using Logger;
using Interfaces;

Action<string> loggerDelegate;
ILogger consoleLogger = new ConsoleLogger();
loggerDelegate = consoleLogger.Log;

PriceUpdater priceUpdater = new PriceUpdater(@"C:\Users\User\Documents\mveuC#\testExcel\test.xlsx", loggerDelegate);
var diffProducts = priceUpdater.GetDifferenceProductsPrice(@"C:\Users\User\Documents\mveuC#\testExcel\testUpdate.xlsx");

if(diffProducts != null)
{
    foreach (var diffProd in diffProducts)
    {
        Console.WriteLine(diffProd.ToString());
    }
    ExcelWrapper.CreateAndSaveFileWithProducts(diffProducts);
}
    


