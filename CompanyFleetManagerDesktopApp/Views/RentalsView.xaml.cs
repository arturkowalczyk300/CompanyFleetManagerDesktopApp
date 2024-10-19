using CompanyFleetManager;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyFleetManagerDesktopApp.Views
{
    public partial class RentalsView : UserControl
    {
        private bool _rentalsLoaded = false;
        private FleetDatabaseContext context;

        public RentalsView()
        {
            InitializeComponent();
            context = new FleetDatabaseContext();
        }

        private RentalInfo GetSelectedRental() => ListViewRentals.SelectedItem as RentalInfo;

        public void LoadRentals()
        {
            if (_rentalsLoaded)
                return;

            var rentals = context.Rentals.ToList();
            var rentalInfo = new List<RentalInfo>();

            foreach (var rental in rentals)
            {
                var employee = context.Employees.ToList().Find(x => x.EmployeeId == rental.RentingEmployeeId);
                var vehicle = context.Vehicles.ToList().Find(x => x.VehicleId == rental.RentedVehicleId);
                rentalInfo.Add(new RentalInfo(rental, employee, vehicle));
            }

            ListViewRentals.ItemsSource = rentalInfo;
            _rentalsLoaded = true;

        }
        public void AddRental()
        {
            _rentalsLoaded = false;
            var window = new AddModifyRentalWindow(context.Vehicles.ToList(), context.Employees.ToList(), null);
            if (window.ShowDialog() == true)
            {
                Rental rental = window.RentalData;

                context.Rentals.Add(rental);
                context.SaveChanges();
            }
            LoadRentals();
        }
        public void ModifySelectedRental()
        {
            _rentalsLoaded = false;
            ModifyRental(GetSelectedRental().Rental);

            LoadRentals();
        }
        public void DeleteSelectedRental()
        {
            _rentalsLoaded = false;
            DeleteRental(GetSelectedRental().Rental);

            LoadRentals();
        }

        private void DeleteRental(Rental r)
        {
            var rentalToRemove = context.Rentals.Find(r.RentalId);
            context.Rentals.Remove(rentalToRemove);
            context.SaveChanges();

        }

        private void ModifyRental(Rental rental)
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
}
