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
    public class RentalsTests
    {
        private const string AppPath = @"C:\Users\artur\source\repos\CompanyFleetManagerDesktopApp\CompanyFleetManagerDesktopApp\bin\Debug\net8.0-windows\CompanyFleetManagerDesktopApp.exe";
        private WindowsDriver<WindowsElement> _driver;

        public RentalsTests()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }

        private void SelectRentalsTab()
        {
            _driver.Manage().Window.Maximize();

            var RentalsTab = _driver.FindElementByAccessibilityId("TabItemRentals");

            RentalsTab.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => RentalsTab.Selected);
        }

        [Fact]
        public void RentalsTest_OnLoad_ShouldDisplayRentals()
        {
            SelectRentalsTab();

            var listView = _driver.FindElementByAccessibilityId("ListViewRentals");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            var rentedVehicle = listItems.First().FindElementByAccessibilityId("TextBlockRentedVehicle").Text;
            var rentingEmployee = listItems.First().FindElementByAccessibilityId("TextBlockRentingEmployee").Text;

            Assert.NotEmpty(listItems);
            Assert.Equal("Skoda Fabia, WE 31DA3", rentedVehicle);
            Assert.Equal("Andrzej Roman Dobrzański, Manager", rentingEmployee);
        }

        [Fact]
        public void RentalsTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            SelectRentalsTab();

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

            var combobox = _driver.FindElementByAccessibilityId("ComboBoxRentedVehicle");
            combobox.Click();
            var item = combobox.FindElementsByClassName("TextBlock");
            item.Where(x => x.Text == "Skoda Fabia, WE 31DA3").First().Click();

            var combobox2 = _driver.FindElementByAccessibilityId("ComboBoxRentingEmployee");
            combobox2.Click();
            var item2 = combobox2.FindElementsByClassName("TextBlock");
            item2.Where(x => x.Text == "Andrzej Roman Dobrzański, Manager").First().Click();

            _driver.FindElementByAccessibilityId("DatePickerRentalDate").SendKeys("01.11.2024");
            _driver.FindElementByAccessibilityId("DatePickerPlannedReturningDate").SendKeys("11.11.2024");
            _driver.FindElementByAccessibilityId("DatePickerFactualReturningDate").SendKeys("");

            _driver.FindElementByAccessibilityId("ButtonSave").Click();

            _driver.SwitchTo().Window(mainWindowHandle);

            var listView = _driver.FindElementByAccessibilityId("ListViewRentals");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            Assert.NotEmpty(listItems);
            Assert.Contains(listItems, l =>
            l.FindElementByAccessibilityId("TextBlockRentedVehicle").Text == "Skoda Fabia, WE 31DA3" &&
            l.FindElementByAccessibilityId("TextBlockRentingEmployee").Text == "Andrzej Roman Dobrzański, Manager");
        }

        [Fact]
        public void RentalsTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}
