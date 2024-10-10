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
    /// Interaction logic for AddModifyEmployeeWindow.xaml
    /// </summary>
    public partial class AddModifyEmployeeWindow : Window
    {
        public Employee EmployeeData { get; private set; }
        public AddModifyEmployeeWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeData = new Employee
            {
                Occupation = TextBoxOccupation.Text,
                Address = TextBoxAddress.Text,
                Forename = TextBoxForename.Text,
                Middlename = TextBoxMiddlename.Text,
                Surname = TextBoxSurname.Text,
                NationalIdentityNumber = long.Parse(TextBoxNationalIdentityNumber.Text),
                WorkPhoneNumber = CompanyFleetManager.Models.PhoneNumber.ParseString(TextBoxWorkPhoneNumber.Text),
                PrivatePhoneNumber = CompanyFleetManager.Models.PhoneNumber.ParseString(TextBoxPrivatePhoneNumber.Text),
                DrivingLicenseCategories = TextBoxDrivingLicenseCategories.Text.ToCharArray().Select(v => v.ToString()).ToList(),
                DrivingLicenseValidity = DateOnly.FromDateTime(DatePickerDrivingLicenseValidity.SelectedDate.Value),
                HiredUntil = DateOnly.FromDateTime(DatePickerHiredUntil.SelectedDate.Value)
            };

            this.DialogResult = true;
            this.Close();
        }
    }
}
