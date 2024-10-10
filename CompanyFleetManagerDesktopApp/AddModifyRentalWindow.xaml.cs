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

        public AddModifyRentalWindow()
        {
            InitializeComponent();
        }

        private void AddRental_Click(object sender, RoutedEventArgs e)
        {
            var rentedVehicleId = int.Parse(TextBoxRentedVehicleId.Text);
            var rentingEmployeeId = int.Parse(TextBoxRentingEmployeeId.Text);
            var rentalDate = DatePickerRentalDate.SelectedDate;
            var plannedReturningDate = DatePickerPlannedReturningDate.SelectedDate;
            var factualReturningDate = DatePickerFactualReturningDate.SelectedDate;

            RentalData = new Rental(
                0,
                rentedVehicleId,
                rentingEmployeeId,
                DateOnly.FromDateTime(rentalDate.Value),
                plannedReturningDate.Value,
                factualReturningDate.Value);

            this.DialogResult = true; //success
            this.Close();
        }
    }
}
