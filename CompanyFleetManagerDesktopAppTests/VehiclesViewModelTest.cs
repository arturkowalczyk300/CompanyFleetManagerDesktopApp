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
        private List<Vehicle> GetSampleVehicles()
        {
            return new List<Vehicle>() {
                    new Vehicle(1, "Audi", "A6", "DW 321AB", 2024, 24002, new DateOnly(2026, 9, 11), false),
                    new Vehicle(2, "BMW", "530D", "DW BX341", 2023, 44032, new DateOnly(2025, 1, 6), false),
                    new Vehicle(3, "Skoda", "Fabia", "WE 31DA3", 2020, 124202, new DateOnly(2025, 2, 1), false)
                    };
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
            throw new NotImplementedException();
        }

        [Fact]
        public void AddVehicle_IncreasesVehiclesCount()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ModifySelectedVehicle_UpdatesVehicle()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteSelectedVehicle_RemovesVehicle()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ModifySelectedVehicle_NullVehicle_ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}