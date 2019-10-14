﻿using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Project1
{
    public class CsvHelper
    {
        private CsvFileDescription csvFileDescription;

        public CsvHelper()
        {
            csvFileDescription = new CsvFileDescription()
            {
                SeparatorChar = ';',
                EnforceCsvColumnAttribute = true,
                IgnoreUnknownColumns = true,
                UseFieldIndexForReadingData = true,
                FirstLineHasColumnNames = false,
                QuoteAllFields = false
            };
        }
        public IList<Computer> LoadDataFromCsv(string csvPath)
        {
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

        public void SaveDataToCsv(string path, IEnumerable<Computer> computers)
        {
            CsvContext csvContext = new CsvContext();
            try
            {
                csvContext.Write<Computer>(computers, path, csvFileDescription);
  
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error occured: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
