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
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        private bool _employeesLoaded = false;
        private FleetDatabaseContext context;

        public EmployeesView()
        {
            InitializeComponent();
            context = new FleetDatabaseContext();
        }

        private Employee GetSelectedEmployee() => ListViewEmployees.SelectedItem as Employee;
        public void LoadEmployees()
        {
            if (_employeesLoaded)
                return;
            var employees = context.Employees.ToList();
            ListViewEmployees.ItemsSource = employees;
            _employeesLoaded = true;
        }

        public void AddEmployee()
        {
            _employeesLoaded = false;

            var window = new AddModifyEmployeeWindow();
            if (window.ShowDialog() == true)
            {
                Employee employee = window.EmployeeData;

                context.Employees.Add(employee);
                context.SaveChanges();
            }

            LoadEmployees();
        }

        public void ModifySelectedEmployee()
        {
            _employeesLoaded = false;
            ModifyEmployee(GetSelectedEmployee());

            LoadEmployees();
        }


        public void DeleteSelectedEmployee()
        {
            _employeesLoaded = false;
            DeleteEmployee(GetSelectedEmployee());

            LoadEmployees();
        }

        private void DeleteEmployee(Employee e)
        {
            var employeeToRemove = context.Employees.Find(e.EmployeeId);
            context.Employees.Remove(employeeToRemove);
            context.SaveChanges();

        }


        private void ModifyEmployee(Employee employee)
        {
            var window = new AddModifyEmployeeWindow(employee);
            if (window.ShowDialog() == true)
            {
                Employee modifiedEmployee = window.EmployeeData;

                context.Employees.Update(modifiedEmployee);
                context.SaveChanges();
            }
        }
    }
}
