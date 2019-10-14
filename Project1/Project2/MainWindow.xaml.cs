using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CsvHelper csvHelper;

        public MainWindow()
        {
            InitializeComponent();
            csvHelper = new CsvHelper();
        }


        private void LoadDataFromCsvButton_Click(object sender, RoutedEventArgs e)
        {
            IList<Computer> computers = csvHelper.LoadDataFromCsv(@"Files\katalog.txt");
            PriceList.ItemsSource = computers;
        }

        private void SaveDataToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            csvHelper.SaveDataToCsv(@"Files\katalog.txt", PriceList.ItemsSource as IEnumerable<Computer>);
            MessageBox.Show("Pomyślnie zapisano dane do pliku");
        }
    }
}
