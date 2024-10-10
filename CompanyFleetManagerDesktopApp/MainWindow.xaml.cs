using CompanyFleetManager;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyFleetManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TabControl.SelectionChanged += TabControl_SelectionChanged;
            ButtonAdd.Click += ButtonAdd_Click;
            ButtonModify.Click += ButtonModify_Click;
            ButtonDelete.Click += ButtonDelete_Click;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = GetSelectedTab();

            switch (selectedTab)
            {
                case "Rentals":
                    LoadRentals();
                    break;

                case "Employees":
                    LoadEmployees();
                    break;

                case "Vehicles":
                    LoadVehicles();
                    break;
            }
        }

        private string GetSelectedTab() => (TabControl.SelectedItem as TabItem).Header.ToString();

        private void LoadEmployees()
        {
            using (var context = new FleetDatabaseContext())
            {
                var employees = context.Employees.ToList();
                ListViewEmployees.ItemsSource = employees;
            }
        }

        private void LoadVehicles()
        {
            using (var context = new FleetDatabaseContext())
            {
                var vehicles = context.Vehicles.ToList();
                ListViewVehicles.ItemsSource = vehicles;
            }
        }

        private void LoadRentals()
        {
            using (var context = new FleetDatabaseContext())
            {
                var rentals = context.Rentals.ToList();
                ListViewRentals.ItemsSource = rentals;
            }
        }
    }
}