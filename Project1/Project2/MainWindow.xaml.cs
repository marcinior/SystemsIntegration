using CsvDB;
using CsvHelper;
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
        private CsvHelper.CsvHelper csvHelper;
        private CsvDbHelper csvDbHelper;
        private bool IsDataLoadedFromDb;

        public MainWindow()
        {
            InitializeComponent();
            csvHelper = new CsvHelper.CsvHelper();
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            csvDbHelper = new CsvDbHelper(connectionString);
        }


        private void LoadDataFromCsvButton_Click(object sender, RoutedEventArgs e)
        {
            List<Computer> computers = csvHelper.LoadDataFromCsv(@"Files\katalog.txt");
            PriceList.ItemsSource = computers;
            IsDataLoadedFromDb = false;
        }

        private void SaveDataToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            csvHelper.SaveDataToCsv(@"Files\katalog.txt", PriceList.ItemsSource as IEnumerable<Computer>);
            MessageBox.Show("Pomyślnie zapisano dane do pliku");
        }

        private void LoadDataFromDbButton_Click(object sender, RoutedEventArgs e)
        {
            PriceList.ItemsSource = csvDbHelper.GetComputers();
            IsDataLoadedFromDb = true;
        }

        private void ExportToDbButton_Click(object sender, RoutedEventArgs e)
        {
            var computers = PriceList.ItemsSource as List<Computer>;
            csvDbHelper.UpdateComputers(computers);
        }

        private void DataGridCellEditEvent(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!IsDataLoadedFromDb)
                return;

            FrameworkElement element = e.Column.GetCellContent(PriceList.SelectedItem);
            (element.Parent as DataGridCell).Background = new SolidColorBrush(Colors.LightGreen);
        }
    }
}
