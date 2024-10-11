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

        public AddModifyEmployeeWindow(Employee employee = null)
        {
            InitializeComponent();

            if (employee != null)
            {
                EmployeeData = employee;
                TextBoxOccupation.Text = employee.Occupation;
                TextBoxAddress.Text = employee.Address;
                TextBoxForename.Text = employee.Forename;
                TextBoxMiddlename.Text = employee.Middlename;
                TextBoxSurname.Text = employee.Surname;
                TextBoxNationalIdentityNumber.Text = employee.NationalIdentityNumber.ToString();
                TextBoxWorkPhoneNumber.Text = employee.WorkPhoneNumber.ToString();
                TextBoxPrivatePhoneNumber.Text = employee.PrivatePhoneNumber.ToString();
                TextBoxDrivingLicenseCategories.Text = employee.DrivingLicenseCategories.ToString();
                DatePickerDrivingLicenseValidity.SelectedDate = employee.DrivingLicenseValidity.ToDateTime(new TimeOnly(0,0));
                DatePickerHiredUntil.SelectedDate = employee.HiredUntil.ToDateTime(new TimeOnly(0, 0));
            }
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
