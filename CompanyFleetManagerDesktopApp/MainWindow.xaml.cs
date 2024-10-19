using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
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

namespace CompanyFleetManagerDesktopApp
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
            switch (GetSelectedTab())
            {
                case "Rentals":
                    RentalsView.DeleteSelectedRental();               
                    break;
                case "Employees":
                    EmployeesView.DeleteSelectedEmployee();
                    break;
                case "Vehicles":
                    VehiclesView.DeleteSelectedVehicle();
                    break;
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    RentalsView.ModifySelectedRental();
                    break;
                case "Employees":
                    EmployeesView.ModifySelectedEmployee();
                    break;
                case "Vehicles":
                    VehiclesView.ModifySelectedVehicle();
                    break;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    RentalsView.AddRental();
                    break;
                case "Employees":
                    EmployeesView.AddEmployee();
                    break;
                case "Vehicles":
                    VehiclesView.AddVehicle();
                    break;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = GetSelectedTab();

            switch (selectedTab)
            {
                case "Rentals":
                    RentalsView.LoadRentals();
                    break;

                case "Employees":
                    EmployeesView.LoadEmployees();
                    break;

                case "Vehicles":
                    VehiclesView.LoadVehicles();
                    break;
            }
        }

        private string GetSelectedTab() => (TabControl.SelectedItem as TabItem).Header.ToString();
    }
}