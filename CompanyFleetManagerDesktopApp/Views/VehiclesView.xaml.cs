using CompanyFleetManager.Models.Entities;
using CompanyFleetManager;
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
    /// <summary>
    /// Interaction logic for VehiclesView.xaml
    /// </summary>
    public partial class VehiclesView : UserControl
    {
        private bool _vehiclesLoaded = false;

        public VehiclesView()
        {
            InitializeComponent();
        }

        private Vehicle GetSelectedVehicle() => ListViewVehicles.SelectedItem as Vehicle;
        public void LoadVehicles()
        {
            if (_vehiclesLoaded)
                return;

            using (var context = new FleetDatabaseContext())
            {
                var vehicles = context.Vehicles.ToList();
                ListViewVehicles.ItemsSource = vehicles;
                _vehiclesLoaded = true;
            }
        }

        public void AddVehicle()
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

        public void ModifySelectedVehicle()
        {
            _vehiclesLoaded = false;
            ModifyVehicle(GetSelectedVehicle());

            LoadVehicles();
        }

        public void DeleteSelectedVehicle()
        {
            _vehiclesLoaded = false;
            DeleteVehicle(GetSelectedVehicle());

            LoadVehicles();
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
    }
}