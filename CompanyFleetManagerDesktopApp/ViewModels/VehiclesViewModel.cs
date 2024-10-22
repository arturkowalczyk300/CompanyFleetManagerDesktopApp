using CompanyFleetManager.Models.Entities;
using CompanyFleetManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CompanyFleetManagerDesktopApp.ViewModels
{
    public class VehiclesViewModel : INotifyPropertyChanged
    {
        private FleetDatabaseContext _context;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _vehiclesLoaded = false;

        private ObservableCollection<Vehicle> _vehicles;

        public VehiclesViewModel()
        {
            _context = new FleetDatabaseContext();
        }

        public VehiclesViewModel(FleetDatabaseContext context)
        {
            _context = context;
        }


        //binded variables
        public Vehicle SelectedVehicle { get; set; }

        public ObservableCollection<Vehicle> Vehicles
        {
            get => _vehicles;
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        public void LoadVehicles()
        {
            if (_vehiclesLoaded)
                return;

            Vehicles = new ObservableCollection<Vehicle>(_context.Vehicles.ToList());
            _vehiclesLoaded = true;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _vehiclesLoaded = false;
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            LoadVehicles();
        }

        public void ModifySelectedVehicle(Vehicle vehicle)
        {
            _vehiclesLoaded = false;
            ModifyVehicle(vehicle);

            LoadVehicles();
        }

        public void DeleteSelectedVehicle()
        {
            _vehiclesLoaded = false;
            DeleteVehicle(SelectedVehicle);

            LoadVehicles();
        }

        private void DeleteVehicle(Vehicle v)
        {
            var vehicleToRemove = _context.Vehicles.Find(v.VehicleId);
            _context.Vehicles.Remove(vehicleToRemove);
            _context.SaveChanges();
        }

        private void ModifyVehicle(Vehicle modifiedVehicle)
        {
            _context.Vehicles.Update(modifiedVehicle);
            _context.SaveChanges();
        }
    }
}
