using CompanyFleetManager.Models.Entities;

namespace CompanyFleetManagerDesktopApp
{
    public class RentalInfo
    {
        public Rental Rental { get; set; }

        private Employee? _employee;
        private Employee? Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                EmployeeShortenedInfo = GetShortenedEmployeeInformation(value);
            }
        }

        private Vehicle? _vehicle;

        private Vehicle? Vehicle
        {
            get
            {
                return _vehicle;
            }
            set
            {
                _vehicle = value;
                VehicleShortenedInfo = GetShortenedVehicleInformation(value);
            }
        }
        private string GetShortenedEmployeeInformation(Employee? employee) => $"{employee.Forename} {employee.Middlename} {employee.Surname}, {employee.Occupation}";

        private string GetShortenedVehicleInformation(Vehicle? vehicle) => $"{vehicle.Brand} {vehicle.Model}, {vehicle.LicencePlateNumber}";

        public string EmployeeShortenedInfo { get; set; }

        public string VehicleShortenedInfo { get; set; }
        public RentalInfo(Rental rental, Employee employee, Vehicle vehicle)
        {
            Rental = rental;
            Employee = employee;
            Vehicle = vehicle;
        }
    }
}