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
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        public EmployeesViewModel()
        {
            context = new FleetDatabaseContext();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _employeesLoaded = false;
        private FleetDatabaseContext context;


        private ObservableCollection<Employee> _employees;

        //binded variables
        public Employee SelectedEmployee { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public void LoadEmployees()
        {
            if (_employeesLoaded)
                return;
            Employees = new ObservableCollection<Employee>(context.Employees.ToList());
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
            ModifyEmployee(SelectedEmployee);

            LoadEmployees();
        }


        public void DeleteSelectedEmployee()
        {
            _employeesLoaded = false;
            DeleteEmployee(SelectedEmployee);

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
