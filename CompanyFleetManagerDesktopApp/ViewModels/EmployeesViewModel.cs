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
        private FleetDatabaseContext _context;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _employeesLoaded = false;
        


        private ObservableCollection<Employee> _employees;

        public EmployeesViewModel()
        {
            _context = new FleetDatabaseContext();
        }

        public EmployeesViewModel(FleetDatabaseContext context)
        {
            _context = context;
        }

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
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
            _employeesLoaded = true;
        }

        public void AddEmployee()
        {
            _employeesLoaded = false;

            var window = new AddModifyEmployeeWindow();
            if (window.ShowDialog() == true)
            {
                Employee employee = window.EmployeeData;

                _context.Employees.Add(employee);
                _context.SaveChanges();
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
            var employeeToRemove = _context.Employees.Find(e.EmployeeId);
            _context.Employees.Remove(employeeToRemove);
            _context.SaveChanges();

        }


        private void ModifyEmployee(Employee employee)
        {
            var window = new AddModifyEmployeeWindow(employee);
            if (window.ShowDialog() == true)
            {
                Employee modifiedEmployee = window.EmployeeData;

                _context.Employees.Update(modifiedEmployee);
                _context.SaveChanges();
            }
        }
    }
}
