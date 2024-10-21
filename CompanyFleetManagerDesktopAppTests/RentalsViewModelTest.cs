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
            var employeesMockSet = new Mock<DbSet<Employee>>();
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            employeesMockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => employees.GetEnumerator());

            var vehicles = VehiclesViewModelTest.GetSampleVehicles().AsQueryable();
            var vehiclesMockSet = new Mock<DbSet<Vehicle>>();
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.Provider);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.Expression);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.ElementType);
            vehiclesMockSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(() => vehicles.GetEnumerator());

            var rentals = GetSampleRentals().AsQueryable();
            var rentalsMockSet = new Mock<DbSet<Rental>>();
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.Provider).Returns(rentals.Provider);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.Expression).Returns(rentals.Expression);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.ElementType).Returns(rentals.ElementType);
            rentalsMockSet.As<IQueryable<Rental>>().Setup(m => m.GetEnumerator()).Returns(() => rentals.GetEnumerator());


            var mockContext = new Mock<FleetDatabaseContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            mockContext.Setup(c => c.Rentals).Returns(rentalsMockSet.Object);

            var viewModel = new RentalsViewModel(mockContext.Object);

            viewModel.LoadRentals();

            Assert.NotEmpty(viewModel.RentalsInfo);
            Assert.Equal(viewModel.RentalsInfo.Count, GetSampleRentals().Count);
        }

        [Fact]
        public void AddRental_IncreasesRentalsCount()
        {
            throw new NotImplementedException();
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