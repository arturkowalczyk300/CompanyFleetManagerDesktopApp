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
                    DeleteRental(GetSelectedRental().Rental);
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
            switch (GetSelectedTab())
            {
                case "Rentals":
                    ModifyRental(GetSelectedRental().Rental);
                    _rentalsLoaded = false;
                    LoadRentals();
                    break;
                case "Employees":
                    ModifyEmployee(GetSelectedEmployee());
                    _employeesLoaded = false;
                    LoadEmployees();
                    break;
                case "Vehicles":
                    ModifyVehicle(GetSelectedVehicle());
                    _vehiclesLoaded = false;
                    LoadVehicles();
                    break;
            }
        }

        private void ModifyVehicle(Vehicle vehicle)
        {
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyVehicleWindow(vehicle);
                if (window.ShowDialog() == true)
                {
                    Vehicle modifiedVehicle = window.VehicleData;

                    context.Vehicles.Update(modifiedVehicle);
                    context.SaveChanges();
                }
            }
        }

        private void ModifyEmployee(Employee employee)
        {
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyEmployeeWindow(employee);
                if (window.ShowDialog() == true)
                {
                    Employee modifiedEmployee = window.EmployeeData;

                    context.Employees.Update(modifiedEmployee);
                    context.SaveChanges();
                }
            }
        }

        private void ModifyRental(Rental rental)
        {
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyRentalWindow(context.Vehicles.ToList(), context.Employees.ToList(), rental);
                if (window.ShowDialog() == true)
                {
                    Rental modifiedRental = window.RentalData;


                    context.Rentals.Update(modifiedRental);
                    context.SaveChanges();
                }
            }
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
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyVehicleWindow();
                if (window.ShowDialog() == true)
                {
                    Vehicle vehicle = window.VehicleData;

                    context.Vehicles.Add(vehicle);
                    context.SaveChanges();
                }
            }
        }

        private void AddEmployee()
        {
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyEmployeeWindow();
                if (window.ShowDialog() == true)
                {
                    Employee employee = window.EmployeeData;

                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }
        }

        private void AddRental()
        {
            using (var context = new FleetDatabaseContext())
            {
                var window = new AddModifyRentalWindow(context.Vehicles.ToList(), context.Employees.ToList(), null);
                if (window.ShowDialog() == true)
                {
                    Rental rental = window.RentalData;

                    context.Rentals.Add(rental);
                    context.SaveChanges();
                }
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

        private RentalInfo GetSelectedRental() => ListViewRentals.SelectedItem as RentalInfo;

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
                var rentalInfo = new List<RentalInfo>();

                foreach(var rental in rentals)
                {
                    var employee= context.Employees.ToList().Find(x => x.EmployeeId == rental.RentingEmployeeId);
                    var vehicle = context.Vehicles.ToList().Find(x => x.VehicleId == rental.RentedVehicleId);
                    rentalInfo.Add(new RentalInfo(rental, employee, vehicle));
                }

                ListViewRentals.ItemsSource = rentalInfo;
                _rentalsLoaded = true;
            }
        }
    }
}