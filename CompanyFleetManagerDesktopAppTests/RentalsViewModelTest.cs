using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp.Models;
using CompanyFleetManagerDesktopApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CompanyFleetManagerDesktopAppTests
{
    public class RentalsViewModelTest
    {
        public List<Rental> GetSampleRentals()
        {
            return new List<Rental>()
            {
                new Rental(1, 1, 1, new DateOnly(2024, 8, 20), new DateTime(2024, 9, 24, 12, 01, 00), null), //not returned yet
                    new Rental(2, 2, 2, new DateOnly(2024, 8, 24), new DateTime(2024, 9, 28, 15, 21, 00), null),
                    new Rental(3, 3, 3, new DateOnly(2024, 9, 1), new DateTime(2024, 10, 15, 9, 5, 00), null)
            };
        }

        private static Mock<FleetDatabaseContext> GetConfiguredMockContextRentals(IQueryable<Employee> employees,
            IQueryable<Vehicle> vehicles,
            IQueryable<Rental> rentals)
        {
            Mock<DbSet<Employee>> employeesMockSet = GetConfiguredMockDbSetEmployees(employees);
            Mock<DbSet<Vehicle>> vehiclesMockSet = GetConfiguredMockDbSetVehicles(vehicles);
            Mock<DbSet<Rental>> rentalsMockSet = GetConfiguredMockDbSetRentals(rentals);

            var mockContext = new Mock<FleetDatabaseContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            mockContext.Setup(c => c.Rentals).Returns(rentalsMockSet.Object);
            return mockContext;
        }

        private static Mock<DbSet<Rental>> GetConfiguredMockDbSetRentals(IQueryable<Rental> rentals)
        {
            var rentalsMockSet = new Mock<DbSet<Rental>>();
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.Provider).Returns(rentals.Provider);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.Expression).Returns(rentals.Expression);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.ElementType).Returns(rentals.ElementType);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.GetEnumerator()).Returns(() => rentals.GetEnumerator());
            return rentalsMockSet;
        }

        private static Mock<DbSet<Vehicle>> GetConfiguredMockDbSetVehicles(IQueryable<Vehicle> vehicles)
        {
            var vehiclesMockSet = new Mock<DbSet<Vehicle>>();
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.Provider);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.Expression);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.ElementType);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(() => vehicles.GetEnumerator());
            return vehiclesMockSet;
        }

        private static Mock<DbSet<Employee>> GetConfiguredMockDbSetEmployees(IQueryable<Employee> employees)
        {
            var employeesMockSet = new Mock<DbSet<Employee>>();
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => employees.GetEnumerator());
            return employeesMockSet;
        }

        [Fact]
        public void Initialization_IsCorrect()
        {
            var viewModel = new RentalsViewModel();

            Assert.Null(viewModel.RentalsInfo);
            Assert.Null(viewModel.SelectedRentalInfo);
        }

        [Fact]
        public void PropertyChanged_IsNotificationSent()
        {
            var viewModel = new RentalsViewModel();
            bool notificationSent = false;
            viewModel.PropertyChanged += (s, e) =>
            {
                notificationSent = e.PropertyName == "RentalInfo";
            };

            viewModel.RentalsInfo = new System.Collections.ObjectModel.ObservableCollection<RentalInfo>();

            Assert.True(notificationSent);
        }

        [Fact]
        public void LoadRentals_PopulatesRentalCollection()
        {
            var employees = EmployeesViewModelTest.GetSampleEmployees().AsQueryable();
            var vehicles = VehiclesViewModelTest.GetSampleVehicles().AsQueryable();
            var rentals = GetSampleRentals().AsQueryable();


            Mock<FleetDatabaseContext> mockContext = GetConfiguredMockContextRentals(employees, vehicles, rentals);

            var viewModel = new RentalsViewModel(mockContext.Object);

            viewModel.LoadRentals();

            Assert.NotEmpty(viewModel.RentalsInfo);
            Assert.Equal(viewModel.RentalsInfo.Count, GetSampleRentals().Count);
        }

        [Fact]
        public void AddRental_IncreasesRentalsCount()
        {
            var employees = new List<Employee>().AsQueryable();
            var vehicles = new List<Vehicle>().AsQueryable();
            var rentals = new List<Rental>().AsQueryable();

            var mockContext = GetConfiguredMockContextRentals(employees, vehicles, rentals);
            var viewModel = new RentalsViewModel(mockContext.Object);

            var rentalToAdd = GetSampleRentals()[0];

            viewModel.AddRental(rentalToAdd);

            mockContext.Verify(c => c.Rentals.Add(It.Is<Rental>(r => r == rentalToAdd)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ModifySelectedRental_UpdatesRental()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteSelectedRental_RemovesRental()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ModifySelectedRental_NullRental_ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}