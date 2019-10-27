using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageHelper
{
    class Program
    {
        private static readonly int[] tableCelWidths = { 12, 10, 15, 15, 0, 15, 8, 9, 9, 12, 10, 31, 10, 31, 14 };

        static void Main(string[] args)
        {
            CsvHelper csvHelper = new CsvHelper();
            IList<Computer> computers = csvHelper.LoadDataFromCsv(@"Files\katalog.txt");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("{0,-10} {1,-10}{2,-15}{3,-15}{4,-10}{5,-10}{6,-12}{7,-7}{8,-14}{9,-12}{10,-25}{11,-13}{12,-31}{13,-10}",
                "Producent", "Matryca", "Rozdzielczość", "Typ matrycy", "Procesor", "Rdzenie", "Taktowanie", "RAM", "Rozmiar dysku", "Typ dysku", "GPU", "GPU Memory", "System operacyjny", "Napęd optyczny"));
            Console.ResetColor();

            foreach (var computer in computers)
            {
                var properties = computer.GetType().GetProperties().ToArray();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].Name == nameof(Computer.TouchScreen))
                        continue;

                    Console.Write(string.Format("{0,-" + tableCelWidths[i] + "}", properties[i].GetValue(computer)));
                }
                Console.Write("\n");
            }
        }
    }
}
