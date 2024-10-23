using CompanyFleetManager.Models.Entities;
using CompanyFleetManager;
using System.ComponentModel;
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

        public void AddEmployee(Employee employee)
        {
            _employeesLoaded = false;
            _context.Employees.Add(employee);
            _context.SaveChanges();

            LoadEmployees();
        }

        public void ModifySelectedEmployee(Employee employee)
        {
            if (SelectedEmployee == null)
                throw new InvalidOperationException();

            _employeesLoaded = false;
            ModifyEmployee(employee);

            LoadEmployees();
        }


        public void DeleteSelectedEmployee()
        {
            if (SelectedEmployee == null)
                throw new InvalidOperationException();

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


        private void ModifyEmployee(Employee modifiedEmployee)
        {
            _context.Employees.Update(modifiedEmployee);
            _context.SaveChanges();
        }
    }
}
