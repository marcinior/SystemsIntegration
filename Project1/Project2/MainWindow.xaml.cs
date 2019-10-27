using StorageHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CsvHelper csvHelper;
        private DbHelper dbHelper;
        private XmlHelper xmlHelper;
        private bool IsDataLoadedFromDb;

        public MainWindow()
        {
            InitializeComponent();
            csvHelper = new CsvHelper();
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            dbHelper = new DbHelper(connectionString);
            xmlHelper = new XmlHelper();
        }


        private void LoadDataFromCsvButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Computer> computers = csvHelper.LoadDataFromCsv(@"Files\katalog.txt");
                PriceList.ItemsSource = computers;
                IsDataLoadedFromDb = false;
                Log.Text = "Pobrano dane z pliku CSV";
            }
            catch(Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }
        }

        private void SaveDataToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                csvHelper.SaveDataToCsv(@"Files\katalog.txt", PriceList.ItemsSource as IEnumerable<Computer>);
                Log.Text = "Zapisano dane do pliku CSV";
            }
            catch (Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }
        }

        private void LoadDataFromDbButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PriceList.ItemsSource = dbHelper.GetComputers();
                IsDataLoadedFromDb = true;
                Log.Text = "Pobrano dane z bazy danych";
            }
            catch (Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }

        }

        private void ExportToDbButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var computers = PriceList.ItemsSource as List<Computer>;
                dbHelper.UpdateComputers(computers);
                Log.Text = "Zapisano dane do bazy danych";
            }
            catch (Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }        
        }

        private void DataGridCellEditEvent(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!IsDataLoadedFromDb)
                return;

            FrameworkElement element = e.Column.GetCellContent(PriceList.SelectedItem);
            (element.Parent as DataGridCell).Background = new SolidColorBrush(Colors.LightGreen);
        }

        private void LoadDataFromXmlButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PriceList.ItemsSource = xmlHelper.LoadComputersFromXml(@"Files\katalog.xml");
                Log.Text = "Pobrano dane z pliku XML";
            }
            catch (Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }
            
        }

        private void ExportToXmlButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                xmlHelper.SaveDataToXml(@"Files\katalog.xml", PriceList.ItemsSource as List<Computer>);
                Log.Text = "Zapisano dane do pliku XML";
            }
            catch (Exception ex)
            {
                Log.Text = "Error occured: " + ex.Message;
            }
        }
    }
}
