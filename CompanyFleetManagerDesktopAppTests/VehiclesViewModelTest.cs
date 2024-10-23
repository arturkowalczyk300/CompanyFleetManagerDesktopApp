using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Runtime.CompilerServices;

namespace CompanyFleetManagerDesktopAppTests
{
    public class VehiclesViewModelTest
    {
        public static List<Vehicle> GetSampleVehicles()
        {
            return new List<Vehicle>() {
                    new Vehicle(1, "Audi", "A6", "DW 321AB", 2024, 24002, new DateOnly(2026, 9, 11), false),
                    new Vehicle(2, "BMW", "530D", "DW BX341", 2023, 44032, new DateOnly(2025, 1, 6), false),
                    new Vehicle(3, "Skoda", "Fabia", "WE 31DA3", 2020, 124202, new DateOnly(2025, 2, 1), false)
                    };
        }
        private static Mock<DbSet<Vehicle>> GetConfiguredMockDbSet(IQueryable<Vehicle> data)
        {
            var mockSet = new Mock<DbSet<Vehicle>>();
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            return mockSet;
        }

        private static Mock<FleetDatabaseContext> GetConfiguredMockContext(IQueryable<Vehicle> data)
        {
            Mock<DbSet<Vehicle>> mockSet = GetConfiguredMockDbSet(data);

            var mockContext = new Mock<FleetDatabaseContext>();
            mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);
            mockContext.Setup(c => c.Vehicles.Find(It.IsAny<object[]>())).Returns<object[]>(
                ids => mockSet.Object.FirstOrDefault(v => v.VehicleId == (int)ids[0]));
            return mockContext;
        }

        [Fact]
        public void Initialization_IsCorrect()
        {
            var viewModel = new VehiclesViewModel();

            Assert.Null(viewModel.Vehicles);
            Assert.Null(viewModel.SelectedVehicle);
        }

        [Fact]
        public void PropertyChanged_IsNotificationSent()
        {
            var viewModel = new VehiclesViewModel();
            bool notificationSent = false;
            viewModel.PropertyChanged += (s, e) => { notificationSent = e.PropertyName == "Vehicles"; };

            viewModel.Vehicles = new System.Collections.ObjectModel.ObservableCollection<Vehicle>();

            Assert.True(notificationSent);
        }

        [Fact]
        public void LoadVehicles_PopulatesVehicleCollection()
        {
            var data = GetSampleVehicles().AsQueryable();
            Mock<FleetDatabaseContext> mockContext = GetConfiguredMockContext(data);

            var viewModel = new VehiclesViewModel(mockContext.Object);

            viewModel.LoadVehicles();

            Assert.NotEmpty(viewModel.Vehicles);
            Assert.Equal(viewModel.Vehicles.Count, GetSampleVehicles().Count);
        }

        [Fact]
        public void AddVehicle_IncreasesVehiclesCount()
        {
            var data = new List<Vehicle>().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new VehiclesViewModel(mockContext.Object);

            var vehicleToAdd = GetSampleVehicles()[0];

            viewModel.AddVehicle(vehicleToAdd);

            mockContext.Verify(c => c.Vehicles.Add(It.Is<Vehicle>(v => v == vehicleToAdd)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ModifySelectedVehicle_UpdatesVehicle()
        {
            var data = GetSampleVehicles().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new VehiclesViewModel(mockContext.Object);

            var vehicleToModify = GetSampleVehicles()[1];

            viewModel.SelectedVehicle = vehicleToModify;
            viewModel.ModifySelectedVehicle(vehicleToModify);

            mockContext.Verify(c => c.Vehicles.Update(It.Is<Vehicle>(v => v == vehicleToModify)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteSelectedVehicle_RemovesVehicle()
        {
            var data = GetSampleVehicles().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new VehiclesViewModel(mockContext.Object);

            var vehicleToDelete = GetSampleVehicles()[2];

            viewModel.SelectedVehicle = vehicleToDelete;
            viewModel.DeleteSelectedVehicle();

            mockContext.Verify(c => c.Vehicles.Remove(It.Is<Vehicle>(v => v.VehicleId == vehicleToDelete.VehicleId)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ModifySelectedVehicle_NullVehicle_ThrowException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteSelectedVehicle_NullVehicle_ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}