using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp.Models;
using CompanyFleetManagerDesktopApp.ViewModels;
using Moq;

namespace CompanyFleetManagerDesktopAppTests
{
    public class RentalsViewModelTest
    {
        private List<Rental> GetSampleRentals()
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
            viewModel.PropertyChanged += (s, e) => {
                notificationSent = e.PropertyName == "RentalInfo";
            };

            viewModel.RentalsInfo = new System.Collections.ObjectModel.ObservableCollection<RentalInfo>();

            Assert.True(notificationSent);
        }

        [Fact]
        public void LoadRentals_PopulatesRentalCollection()
        {
            throw new NotImplementedException();
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