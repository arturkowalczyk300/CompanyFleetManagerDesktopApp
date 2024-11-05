using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManagerDesktopAppTestsE2E
{
    public class VehiclesTests
    {
        private const string AppPath = @"C:\Users\artur\source\repos\CompanyFleetManagerDesktopApp\CompanyFleetManagerDesktopApp\bin\Debug\net8.0-windows\CompanyFleetManagerDesktopApp.exe";
        private WindowsDriver<WindowsElement> _driver;

        public VehiclesTests()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }
        private void SelectVehiclesTab()
        {
            _driver.Manage().Window.Maximize();

            var vehiclesTab = _driver.FindElementByAccessibilityId("TabItemVehicles");

            vehiclesTab.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => vehiclesTab.Selected);
        }

        [Fact]
        public void VehiclesTest_OnLoad_ShouldDisplayVehicles()
        {
            SelectVehiclesTab();

            var listView = _driver.FindElementByAccessibilityId("ListViewVehicles");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            var model = listItems.First().FindElementByAccessibilityId("Model").Text;
            var licencePlate = listItems.First().FindElementByAccessibilityId("LicencePlateNumber").Text;

            Assert.NotEmpty(listItems);
            Assert.Equal("A6", model);
            Assert.Equal("DW 321AB", licencePlate);
        }

        [Fact]
        public void VehiclesTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            SelectVehiclesTab();

            var mainWindowHandle = _driver.CurrentWindowHandle;

            _driver.FindElementByAccessibilityId("ButtonAdd").Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));

            var windowsHandles = _driver.WindowHandles;
            wait.Until(d =>
            {
                return windowsHandles.Count > 1;
            });

            var addWindowHandle = windowsHandles.Where(x => x != mainWindowHandle).First();

            _driver.SwitchTo().Window(addWindowHandle);

            _driver.FindElementByAccessibilityId("TextBoxBrand").SendKeys("Fiat");
            _driver.FindElementByAccessibilityId("TextBoxModel").SendKeys("Ducato");
            _driver.FindElementByAccessibilityId("TextBoxLicencePlateNumber").SendKeys("WE XA023");
            _driver.FindElementByAccessibilityId("TextBoxProductionYear").SendKeys("2021");
            _driver.FindElementByAccessibilityId("TextBoxMileage").SendKeys("123016");
            _driver.FindElementByAccessibilityId("DatePickerVehicleInspectionValidity").SendKeys("11.01.2025");
            _driver.FindElementByAccessibilityId("CheckBoxIsDamaged").Click();

            _driver.FindElementByAccessibilityId("ButtonSave").Click();

            _driver.SwitchTo().Window(mainWindowHandle);

            var listView = _driver.FindElementByAccessibilityId("ListViewVehicles");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            Assert.NotEmpty(listItems);
            Assert.Contains(listItems, l =>
            l.FindElementByAccessibilityId("Model").Text == "Ducato" &&
            l.FindElementByAccessibilityId("LicencePlateNumber").Text == "WE XA023");
        }

        [Fact]
        public void VehiclesTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}
