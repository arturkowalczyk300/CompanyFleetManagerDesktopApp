using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp;
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
        private bool _rentalsLoaded = false;
        private bool _employeesLoaded = false;
        private bool _vehiclesLoaded = false;

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
                    DeleteRental(GetSelectedRental());
                    _rentalsLoaded = false;
                    LoadRentals();
                    break;
                case "Employees":
                    DeleteEmployee(GetSelectedEmployee());
                    _employeesLoaded = false;
                    LoadEmployees();
                    break;
                case "Vehicles":
                    DeleteVehicle(GetSelectedVehicle());
                    _vehiclesLoaded = false;
                    LoadVehicles();
                    break;
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteVehicle(Vehicle v)
        {
            using (var context = new FleetDatabaseContext())
            {
                var vehicleToRemove = context.Vehicles.Find(v.VehicleId);
                context.Vehicles.Remove(vehicleToRemove);
                context.SaveChanges();
            }
        }

        private void DeleteEmployee(Employee e)
        {
            using (var context = new FleetDatabaseContext())
            {
                var employeeToRemove = context.Employees.Find(e.EmployeeId);
                context.Employees.Remove(employeeToRemove);
                context.SaveChanges();
            }
        }

        private void DeleteRental(Rental r)
        {
            using (var context = new FleetDatabaseContext())
            {
                var rentalToRemove = context.Rentals.Find(r.RentalId);
                context.Rentals.Remove(rentalToRemove);
                context.SaveChanges();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (GetSelectedTab())
            {
                case "Rentals":
                    AddRental();
                    _rentalsLoaded = false;
                    LoadRentals();
                    break;
                case "Employees":
                    AddEmployee();
                    _employeesLoaded = false;
                    LoadEmployees();
                    break;
                case "Vehicles":
                    AddVehicle();
                    _vehiclesLoaded = false;
                    LoadVehicles();
                    break;
            }
        }

        private void AddVehicle()
        {
            var window = new AddModifyVehicleWindow();
            if(window.ShowDialog() == true)
            {
                Vehicle vehicle = window.VehicleData;

                var context = new FleetDatabaseContext();
                context.Vehicles.Add(vehicle);
                context.SaveChanges();
            }
        }

        private void AddEmployee()
        {
            var window = new AddModifyEmployeeWindow();
            if (window.ShowDialog() == true)
            {
                Employee employee = window.EmployeeData;

                var context = new FleetDatabaseContext();
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        private void AddRental()
        {
            var window = new AddModifyRentalWindow();
            if (window.ShowDialog() == true)
            {
                Rental rental = window.RentalData;

                var context = new FleetDatabaseContext();
                context.Rentals.Add(rental);
                context.SaveChanges();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = GetSelectedTab();

            switch (selectedTab)
            {
                case "Rentals":
                    if (!_rentalsLoaded)
                        LoadRentals();
                    break;

                case "Employees":
                    if (!_employeesLoaded)
                        LoadEmployees();
                    break;

                case "Vehicles":
                    if (!_vehiclesLoaded)
                        LoadVehicles();
                    break;
            }
        }

        private string GetSelectedTab() => (TabControl.SelectedItem as TabItem).Header.ToString();

        private Vehicle GetSelectedVehicle() => ListViewVehicles.SelectedItem as Vehicle;

        private Employee GetSelectedEmployee() => ListViewEmployees.SelectedItem as Employee;

        private Rental GetSelectedRental() => ListViewRentals.SelectedItem as Rental;

        private void LoadEmployees()
        {
            using (var context = new FleetDatabaseContext())
            {
                var employees = context.Employees.ToList();
                ListViewEmployees.ItemsSource = employees;
                _employeesLoaded = true;
            }
        }

        private void LoadVehicles()
        {
            using (var context = new FleetDatabaseContext())
            {
                var vehicles = context.Vehicles.ToList();
                ListViewVehicles.ItemsSource = vehicles;
                _vehiclesLoaded = true;
            }
        }

        private void LoadRentals()
        {
            using (var context = new FleetDatabaseContext())
            {
                var rentals = context.Rentals.ToList();
                ListViewRentals.ItemsSource = rentals;
                _rentalsLoaded = true;
            }
        }
    }
}