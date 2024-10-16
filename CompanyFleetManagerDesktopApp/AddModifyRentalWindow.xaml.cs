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
        public Rental RentalData { get; set; }

        public AddModifyRentalWindow(Rental rental = null)
        {
            InitializeComponent();

            RentalData = rental;
            TextBoxRentedVehicleId.Text = rental.RentedVehicleId.ToString();
            TextBoxRentingEmployeeId.Text = rental.RentingEmployeeId.ToString();
            DatePickerRentalDate.SelectedDate = rental.RentalDate.ToDateTime(new TimeOnly(0, 0));
            DatePickerPlannedReturningDate.SelectedDate = rental.PlannedReturningDate;
            DatePickerFactualReturningDate.SelectedDate = rental.FactualReturningDate;
        }

        private void ButtonSaveRental_Click(object sender, RoutedEventArgs e)
        {
            var rentedVehicleId = int.Parse(TextBoxRentedVehicleId.Text);
            var rentingEmployeeId = int.Parse(TextBoxRentingEmployeeId.Text);
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
