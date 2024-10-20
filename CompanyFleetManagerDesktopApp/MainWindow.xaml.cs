using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp.ViewModels;
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
    public partial class MainWindow : Window
    {
        private RentalsViewModel _rentalsViewModel = new RentalsViewModel();
        private EmployeesViewModel _employeesViewModel = new EmployeesViewModel();
        private VehiclesViewModel _vehiclesViewModel = new VehiclesViewModel();

        public MainWindow()
        {
            InitializeComponent();

            TabControl.SelectionChanged += TabControl_SelectionChanged;
            ButtonAdd.Click += ButtonAdd_Click;
            ButtonModify.Click += ButtonModify_Click;
            ButtonDelete.Click += ButtonDelete_Click;

            EmployeesView.DataContext = _employeesViewModel;
            VehiclesView.DataContext = _vehiclesViewModel;
            RentalsView.DataContext = _rentalsViewModel;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    _rentalsViewModel.DeleteSelectedRental();
                    break;
                case "Employees":
                    _employeesViewModel.DeleteSelectedEmployee();
                    break;
                case "Vehicles":
                    _vehiclesViewModel.DeleteSelectedVehicle();
                    break;
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    _rentalsViewModel.ModifySelectedRental();
                    break;
                case "Employees":
                    _employeesViewModel.ModifySelectedEmployee();
                    break;
                case "Vehicles":
                    _vehiclesViewModel.ModifySelectedVehicle();
                    break;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    _rentalsViewModel.AddRental();
                    break;
                case "Employees":
                    _employeesViewModel.AddEmployee();
                    break;
                case "Vehicles":
                    _vehiclesViewModel.AddVehicle();
                    break;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = GetSelectedTab();

            switch (selectedTab)
            {
                case "Rentals":
                    _rentalsViewModel.LoadRentals();
                    break;

                case "Employees":
                    _employeesViewModel.LoadEmployees();
                    break;

                case "Vehicles":
                    _vehiclesViewModel.LoadVehicles();
                    break;
            }
        }

        private string GetSelectedTab() => (TabControl.SelectedItem as TabItem).Header.ToString();
    }
}