using Project2.ComputerWebService;
using StorageHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;


namespace Project2
{
    /// <summary>
    /// Interaction logic for ClientApp.xaml
    /// </summary>
    public partial class ClientApp : Window
    {
        ComputerWebService.ComputerWebServiceSoapClient client;

        public ClientApp()
        {
            InitializeComponent();
            SetupProducerList();
            client = new ComputerWebService.ComputerWebServiceSoapClient();
        }

        private void SetupProducerList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            DbHelper dbHelper = new DbHelper(connectionString);
            ProducersComboBox.ItemsSource = dbHelper.GetProducers();
        }

        private void GetProducerLaptopsCountButton_Click(object sender, RoutedEventArgs e)
        {
            string producer = ProducersComboBox.Text;
            int result = client.ComputersCount(producer);
            computersCount.Text = result.ToString();
        }

        private void GetLabptopsByDisplayTypeButton_Click(object sender, RoutedEventArgs e)
        {
            string displayType = DisplayType.Text;
            var computers = client.GetComputersByDisplayType(displayType);
            ComputersDataGrid.ItemsSource = computers;
            ComputersDataGrid.Columns[0].Visibility = Visibility.Hidden;
            ComputersDataGrid.Columns[4].Visibility = Visibility.Hidden;
        }
    }
}
