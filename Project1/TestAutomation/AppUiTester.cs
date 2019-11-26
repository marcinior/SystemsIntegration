using StorageHelper;
using System;
using System.Linq;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace TestAutomation
{
    internal static class AppUiTester
    {
        public static void TestServerApp(Computer computer, bool createXml, int rowId)
        {
            Application application = Application.Launch("Project2.exe");
            Window window = application.GetWindow("Integracja Systemów - Marcin Bobel", InitializeOption.NoCache);
            Button loadDataFromCsvButton = window.Get<Button>("LoadDataFromCsvButton");
            loadDataFromCsvButton.Click();

            ListView priceList = window.Get<ListView>("PriceList");

            if (priceList == null || priceList.Items.Count < rowId)
                throw new ArgumentException("RowId grather than avaiable rows");


            ListViewRow row = priceList.Rows[rowId];
            var properties = computer.GetType().GetProperties().ToList();

            for (int i = 0; i < properties.Count; i++)
            {
                var value = properties[i].GetValue(computer);
                if (value == null)
                    continue;

                row.Cells[i].Click();
                row.Cells[i].Enter("");
                row.Cells[i].Enter(value.ToString());
                row.Cells[i].Click();
            };

            if (createXml)
            {
                Button saveToXmlButton = window.Get<Button>("ExportToXmlButton");
                saveToXmlButton.Click();
            }

            Button saveToDbButton = window.Get<Button>("ExportToDbButton");
            saveToDbButton.Click();
        }

        public static void TestClientApp(string producer, string displayType)
        {
            Application application = Application.Launch("Project2.exe");
            Window window = application.GetWindow("Integracja Systemów - Marcin Bobel", InitializeOption.NoCache);
            Button openClientAppButton = window.Get<Button>("GoToClientAppButton");
            openClientAppButton.Click();

            Window clientWindow = application.GetWindow("ClientApp", InitializeOption.NoCache);
            if (producer != null)
            {
                ComboBox comboBox =  clientWindow.Get<ComboBox>("ProducersComboBox");
                if(comboBox.Items.Any(p => p.Text == producer)){
                    comboBox.Select(producer);
                }

            }

            Button getProducerLaptopsCountButton = clientWindow.Get<Button>("GetProducerLaptopsCountButton");
            getProducerLaptopsCountButton.Click();

            if (displayType != null)
            {
                ComboBox comboBox = clientWindow.Get<ComboBox>("DisplayType");
                if(comboBox.Items.Any(p => p.Text == displayType))
                {
                    comboBox.Select(displayType);
                }
            }

            Button getLabptopsByDisplayTypeButton = clientWindow.Get<Button>("GetLabptopsByDisplayTypeButton");
            getLabptopsByDisplayTypeButton.Click();
        }
    }
}
