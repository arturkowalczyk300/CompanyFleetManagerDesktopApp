using CompanyFleetManager.Models.Entities;
using CompanyFleetManager;
using CompanyFleetManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CompanyFleetManagerDesktopApp.ViewModels
{
    public class RentalsViewModel : INotifyPropertyChanged
    {
        public RentalsViewModel()
        {
            context = new FleetDatabaseContext();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _rentalsLoaded = false;
        private FleetDatabaseContext context;

        private ObservableCollection<RentalInfo> _rentalsInfo;

        //binded variables
        public RentalInfo SelectedRentalInfo { get; set; }

        public ObservableCollection<RentalInfo> RentalsInfo
        {
            get => _rentalsInfo;
            set
            {
                _rentalsInfo = value;
                OnPropertyChanged(nameof(RentalInfo));
            }
        }

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

            RentalsInfo = new ObservableCollection<RentalInfo>(rentalInfo);

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
            ModifyRental(SelectedRentalInfo.Rental);

            LoadRentals();
        }
        public void DeleteSelectedRental()
        {
            _rentalsLoaded = false;
            DeleteRental(SelectedRentalInfo.Rental);

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
