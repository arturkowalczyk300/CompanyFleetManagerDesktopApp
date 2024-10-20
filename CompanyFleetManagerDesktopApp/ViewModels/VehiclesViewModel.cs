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
        public VehiclesViewModel()
        {
            context = new FleetDatabaseContext();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _vehiclesLoaded = false;

        private FleetDatabaseContext context;

        private ObservableCollection<Vehicle> _vehicles;


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

            Vehicles = new ObservableCollection<Vehicle>(context.Vehicles.ToList());
            _vehiclesLoaded = true;
        }

        public void AddVehicle()
        {
            var window = new AddModifyVehicleWindow();
            if (window.ShowDialog() == true)
            {
                Vehicle vehicle = window.VehicleData;

                context.Vehicles.Add(vehicle);
                context.SaveChanges();
            }
        }

        public void ModifySelectedVehicle()
        {
            _vehiclesLoaded = false;
            ModifyVehicle(SelectedVehicle);

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
            var vehicleToRemove = context.Vehicles.Find(v.VehicleId);
            context.Vehicles.Remove(vehicleToRemove);
            context.SaveChanges();
        }

        private void ModifyVehicle(Vehicle vehicle)
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
}
