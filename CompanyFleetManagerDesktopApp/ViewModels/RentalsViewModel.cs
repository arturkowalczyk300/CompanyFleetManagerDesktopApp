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
        private FleetDatabaseContext _context;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _rentalsLoaded = false;

        private ObservableCollection<RentalInfo> _rentalsInfo;

        public RentalsViewModel()
        {
            _context = new FleetDatabaseContext();
        }

        public RentalsViewModel(FleetDatabaseContext context)
        {
            _context = context;
        }

        //binded variables
        public RentalInfo SelectedRentalInfo { get; set; }

        public ObservableCollection<RentalInfo> RentalsInfo
        {
            get => _rentalsInfo;
            set
            {
                _rentalsInfo = value;
                OnPropertyChanged(nameof(RentalsInfo));
            }
        }

        public void LoadRentals()
        {
            if (_rentalsLoaded)
                return;

            var rentals = _context.Rentals.ToList();
            var rentalInfo = new List<RentalInfo>();

            foreach (var rental in rentals)
            {
                var employee = _context.Employees.ToList().Find(x => x.EmployeeId == rental.RentingEmployeeId);
                var vehicle = _context.Vehicles.ToList().Find(x => x.VehicleId == rental.RentedVehicleId);
                rentalInfo.Add(new RentalInfo(rental, employee, vehicle));
            }

            RentalsInfo = new ObservableCollection<RentalInfo>(rentalInfo);

            _rentalsLoaded = true;

        }
        public void AddRental(Rental rental)
        {
            _rentalsLoaded = false;
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            LoadRentals();
        }
        public void ModifySelectedRental(Rental rental)
        {
            _rentalsLoaded = false;
            ModifyRental(rental);

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
            var rentalToRemove = _context.Rentals.Find(r.RentalId);
            _context.Rentals.Remove(rentalToRemove);
            _context.SaveChanges();

        }

        private void ModifyRental(Rental modifiedRental)
        {
            _context.Rentals.Update(modifiedRental);
            _context.SaveChanges();
        }
    }
}
