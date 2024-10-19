using CompanyFleetManager.Models.Entities;
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
using System.Windows.Shapes;

namespace CompanyFleetManagerDesktopApp
{
    /// <summary>
    /// Interaction logic for AddModifyRentalWindow.xaml
    /// </summary>
    public partial class AddModifyRentalWindow : Window
    {
        public Rental RentalData { get; set; } = new Rental();

        public AddModifyRentalWindow(List<Vehicle> vehicles, List<Employee> employees, Rental? rentalToModify = null)
        {
            InitializeComponent();

            ComboBoxRentedVehicle.ItemsSource = vehicles;
            ComboBoxRentingEmployeeId.ItemsSource = employees;
            if (rentalToModify != null)
            {
                RentalData = rentalToModify;
                //ComboBoxRentedVehicle.SelectedItem = vehicles.Find(x => x.VehicleId == rentalToModify.RentedVehicleId);
                //ComboBoxRentingEmployeeId.SelectedItem = employees.Find(x => x.EmployeeId == rentalToModify.RentingEmployeeId);
                DatePickerRentalDate.SelectedDate = rentalToModify.RentalDate.ToDateTime(new TimeOnly(0, 0));
                DatePickerPlannedReturningDate.SelectedDate = rentalToModify.PlannedReturningDate;
                DatePickerFactualReturningDate.SelectedDate = rentalToModify.FactualReturningDate;
            }
        }

        private void ButtonSaveRental_Click(object sender, RoutedEventArgs e)
        {
            var rentedVehicleId = (ComboBoxRentedVehicle.SelectedItem as Vehicle).VehicleId;
            var rentingEmployeeId = (ComboBoxRentingEmployeeId.SelectedItem as Employee).EmployeeId;
            var rentalDate = DatePickerRentalDate.SelectedDate;
            var plannedReturningDate = DatePickerPlannedReturningDate.SelectedDate;
            var factualReturningDate = DatePickerFactualReturningDate.SelectedDate;

            RentalData.RentalId = 0;
            RentalData.RentedVehicleId = rentedVehicleId;
            RentalData.RentingEmployeeId = rentingEmployeeId;
            RentalData.RentalDate = DateOnly.FromDateTime(rentalDate.Value);
            RentalData.PlannedReturningDate = plannedReturningDate.Value;
            RentalData.FactualReturningDate = factualReturningDate.Value;

            this.DialogResult = true; //success
            this.Close();
        }
    }
}
