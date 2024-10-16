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
    /// Interaction logic for AddModifyVehicleWindow.xaml
    /// </summary>
    public partial class AddModifyVehicleWindow : Window
    {
        public Vehicle VehicleData { get; private set; }

        public AddModifyVehicleWindow(Vehicle vehicle = null)
        {
            InitializeComponent();

            if (vehicle != null)
            {
                VehicleData = vehicle;
                TextBoxBrand.Text = vehicle.Brand;
                TextBoxModel.Text = vehicle.Model;
                TextBoxLicencePlateNumber.Text = vehicle.LicencePlateNumber;
                TextBoxProductionYear.Text = vehicle.ProductionYear.ToString();
                TextBoxMileage.Text = vehicle.Mileage.ToString();
                DatePickerVehicleInspectionValidity.SelectedDate = vehicle.VehicleInspectionValidity.ToDateTime(new TimeOnly(0, 0));
                CheckBoxIsDamaged.IsChecked = vehicle.IsDamaged;
            }
        }

        private void ButtonSaveVehicle_Click(object sender, RoutedEventArgs e)
        {

            VehicleData.Brand = TextBoxBrand.Text;
            VehicleData.Model = TextBoxModel.Text;
            VehicleData.LicencePlateNumber = TextBoxLicencePlateNumber.Text;
            VehicleData.ProductionYear = int.Parse(TextBoxProductionYear.Text);
            VehicleData.Mileage = int.Parse(TextBoxMileage.Text);
            VehicleData.VehicleInspectionValidity = DateOnly.FromDateTime(DatePickerVehicleInspectionValidity.SelectedDate.Value);
            VehicleData.IsDamaged = CheckBoxIsDamaged.IsChecked.Value;

            this.DialogResult = true;
            this.Close();
        }
    }
}
