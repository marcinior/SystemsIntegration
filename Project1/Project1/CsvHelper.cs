using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Project1
{
    public class CsvHelper
    {
        internal IList<Computer> LoadDataFromCsv(string csvPath)
        {
            CsvFileDescription csvFileDescription = new CsvFileDescription()
            {
                SeparatorChar = ';',
                EnforceCsvColumnAttribute = true,
                IgnoreUnknownColumns = true,
                UseFieldIndexForReadingData = true,
                FirstLineHasColumnNames = false
            };

            CsvContext csvContext = new CsvContext();
            IList<Computer> results = null;

            try
            {
                results = csvContext.Read<Computer>(csvPath, csvFileDescription).ToList();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error occured: {ex.Message}");
                Console.ResetColor();
            }

            return results;
        }
    }
}
