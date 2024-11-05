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
                    OpenAddModifyRentalsWindow(modify: true);
                    break;
                case "Employees":
                    OpenAddModifyEmployeesWindow(modify: true);
                    break;
                case "Vehicles":
                    OpenAddModifyVehiclesWindow(modify: true);
                    break;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    OpenAddModifyRentalsWindow(modify: false);
                    break;
                case "Employees":
                    OpenAddModifyEmployeesWindow(modify: false);
                    break;
                case "Vehicles":
                    OpenAddModifyVehiclesWindow(modify: false);
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


        private void OpenAddModifyRentalsWindow(Boolean modify)
        {
            _vehiclesViewModel.LoadVehicles();
            _employeesViewModel.LoadEmployees();
            _rentalsViewModel.LoadRentals();
            List<Vehicle> vehicles = _vehiclesViewModel.Vehicles.ToList();
            List<Employee> employees = _employeesViewModel.Employees.ToList();
            var window = new AddModifyRentalWindow(vehicles, employees, _rentalsViewModel.SelectedRentalInfo?.Rental);
            if (window.ShowDialog() == true)
            {
                if (modify)
                    _rentalsViewModel.ModifySelectedRental(window.RentalData);
                else
                    _rentalsViewModel.AddRental(window.RentalData);
            }
        }

        private void OpenAddModifyEmployeesWindow(Boolean modify)
        {
            var window = new AddModifyEmployeeWindow(_employeesViewModel.SelectedEmployee);
            if (window.ShowDialog() == true)
            {
                if (modify)
                    _employeesViewModel.ModifySelectedEmployee(window.EmployeeData);
                else
                    _employeesViewModel.AddEmployee(window.EmployeeData);
            }
        }

        private void OpenAddModifyVehiclesWindow(Boolean modify)
        {
            var window = new AddModifyVehicleWindow(_vehiclesViewModel.SelectedVehicle);
            if (window.ShowDialog() == true)
            {
                if (modify)
                    _vehiclesViewModel.ModifySelectedVehicle(window.VehicleData);
                else
                    _vehiclesViewModel.AddVehicle(window.VehicleData);
            }
        }
    }
}