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
                DatePickerDrivingLicenseValidity.SelectedDate = employee.DrivingLicenseValidity.ToDateTime(new TimeOnly(0, 0));
                DatePickerHiredUntil.SelectedDate = employee.HiredUntil.ToDateTime(new TimeOnly(0, 0));
            }
        }

        private void ButtonSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeData.Occupation = TextBoxOccupation.Text;
            EmployeeData.Address = TextBoxAddress.Text;
            EmployeeData.Forename = TextBoxForename.Text;
            EmployeeData.Middlename = TextBoxMiddlename.Text;
            EmployeeData.Surname = TextBoxSurname.Text;
            EmployeeData.NationalIdentityNumber = long.Parse(TextBoxNationalIdentityNumber.Text);
            EmployeeData.WorkPhoneNumber = CompanyFleetManager.Models.PhoneNumber.ParseString(TextBoxWorkPhoneNumber.Text);
            EmployeeData.PrivatePhoneNumber = CompanyFleetManager.Models.PhoneNumber.ParseString(TextBoxPrivatePhoneNumber.Text);
            EmployeeData.DrivingLicenseCategories = TextBoxDrivingLicenseCategories.Text.ToCharArray().Select(v => v.ToString()).ToList();
            EmployeeData.DrivingLicenseValidity = DateOnly.FromDateTime(DatePickerDrivingLicenseValidity.SelectedDate.Value);
            EmployeeData.HiredUntil = DateOnly.FromDateTime(DatePickerHiredUntil.SelectedDate.Value);

            this.DialogResult = true;
            this.Close();
        }
    }
}
