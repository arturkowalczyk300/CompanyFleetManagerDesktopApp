using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CompanyFleetManagerDesktopAppTestsE2E
{
    public class MainWindowTests
    {
        private const string AppPath = @"C:\Users\artur\source\repos\CompanyFleetManagerDesktopApp\CompanyFleetManagerDesktopApp\bin\Debug\net8.0-windows\CompanyFleetManagerDesktopApp.exe";
        private WindowsDriver<WindowsElement> _driver;

        public MainWindowTests()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }

        [Fact]
        public void MainWindowTest_OnLoad_ShouldRentalTabBeActive()
        {
            var rentalTab = _driver.FindElementByAccessibilityId("TabItemRentals");

            Assert.True(rentalTab.Selected, "The Rental tab should be active on MainWindow load.");
        }

        [Fact]
        public void MainWindowTest_OnClickVehiclesTab_ShouldDisplayVehiclesTab()
        {
            _driver.Manage().Window.Maximize();

            var vehiclesTab = _driver.FindElementByAccessibilityId("TabItemVehicles");

            vehiclesTab.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => vehiclesTab.Selected);

            Assert.True(vehiclesTab.Selected, "The Vehicles tab should be active after click Vehicles tab switch button");
        }

        [Fact]
        public void MainWindowTest_OnClickEmployeesTab_ShouldDisplayEmployeesTab()
        {
            _driver.Manage().Window.Maximize();

            var employeesTab = _driver.FindElementByAccessibilityId("TabItemEmployees");

            employeesTab.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => employeesTab.Selected);

            Assert.True(employeesTab.Selected, "The Employees tab should be active after click Employees tab switch button");
        }

        [Fact]
        public void MainWindowTest_OnClickRentalsTab_ShouldDisplayRentalsTab()
        {
            _driver.Manage().Window.Maximize();

            var vehiclesTab = _driver.FindElementByAccessibilityId("TabItemVehicles");
            vehiclesTab.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => vehiclesTab.Selected);
            Assert.True(vehiclesTab.Selected, "The Vehicles tab should be active after click Vehicles tab switch button");


            var rentalsTab = _driver.FindElementByAccessibilityId("TabItemRentals");

            rentalsTab.Click();

            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => rentalsTab.Selected);

            Assert.True(rentalsTab.Selected, "The Rentals tab should be active after click Rentals tab switch button");
        }    
    }
}