﻿using OfficeWrapper;
using Models;
using PriceUpdate;
using Logger;
using Interfaces;
using DbApi;
using System.Globalization;
//этапы выполнения программы
/*приходит файл на выполнение
 * считываем с него данные +
 * обрабатываем данные из файла и из бд+
 * генерируем файл только с обновлёнными ценниками и новыми продуктами+
 * записываем новые данные в бд+
 */

public static class Program
{
    public static void Main(string[] args)
    {
        Action<string> logger;
        ILogger consoleLogger = new ConsoleLogger();
        logger = consoleLogger.Log;
        string pathToExcelFile = @"C:\Users\User\Documents\mveuC#\testExcel\testUpdate.xlsx";

        if(args.Length > 0)
            if (!string.IsNullOrEmpty(args[0]))
                pathToExcelFile = args[0];
            
        MainProcess mainProcess = new();
        mainProcess.RunProcessing(pathToExcelFile, logger);
    }
}