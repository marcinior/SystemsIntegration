using StorageHelper;
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
using TestStack.White.Factory;
using White = TestStack.White;

namespace TestAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Computer computer;
        public MainWindow()
        {
            InitializeComponent();
            computer = new Computer();
            this.DataContext = computer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Computer testCaseComputer = new Computer()
            {
                Producer = ProducerCheckbox.IsChecked.Value ? computer.Producer : null,
                ScreenSize = ScreenSizeCheckbox.IsChecked.Value ? computer.ScreenSize : null,
                ScreenResolution = ResolutionCheckbox.IsChecked.Value ? computer.ScreenResolution : null,
                DisplayType = DisplayTypeCheckbox.IsChecked.Value ? computer.DisplayType : null,
                Processor = ProcessorCheckbox.IsChecked.Value ? computer.Processor : null,
                PhysicalCores = PhysicalCoresCheckbox.IsChecked.Value ? computer.PhysicalCores : null,
                CpuClockSpeed = CpuClockSpeedCheckbox.IsChecked.Value ? computer.CpuClockSpeed : null,
                Ram = RamCheckbox.IsChecked.Value ? computer.Ram : null,
                DiscType = DiscTypeCheckbox.IsChecked.Value ? computer.DiscType : null,
                DiscSize = DiscSizeCheckbox.IsChecked.Value ? computer.DiscSize : null,
                Gpu = GpuCheckbox.IsChecked.Value ? computer.Gpu : null,
                GpuMemory = GpuCheckbox.IsChecked.Value ? computer.GpuMemory : null,
                OperatingSystem = OSCheckbox.IsChecked.Value ? computer.OperatingSystem : null,
                OpticalDrive = OpticalDriveCheckbox.IsChecked.Value ? computer.OpticalDrive : null
            };

            if (int.TryParse(RowId.Text, out int parsedId))
            {
                AppUiTester.TestServerApp(testCaseComputer, SaveToXmlCheckbox.IsChecked.Value, parsedId);
            }
            else
            {
                MessageBox.Show("Cannot parse rowId");
            }

            AppUiTester.TestClientApp(computer.Producer, computer.DisplayType);
        }
    }
}
