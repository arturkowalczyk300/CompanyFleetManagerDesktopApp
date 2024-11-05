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
    public class EmployeesTests
    {
        private const string AppPath = @"C:\Users\artur\source\repos\CompanyFleetManagerDesktopApp\CompanyFleetManagerDesktopApp\bin\Debug\net8.0-windows\CompanyFleetManagerDesktopApp.exe";
        private WindowsDriver<WindowsElement> _driver;

        public EmployeesTests()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }

        private void SelectEmployeesTab()
        {
            _driver.Manage().Window.Maximize();

            var EmployeesTab = _driver.FindElementByAccessibilityId("TabItemEmployees");

            EmployeesTab.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wait.Until(d => EmployeesTab.Selected);
        }

        [Fact]
        public void EmployeesTest_OnLoad_ShouldDisplayEmployees()
        {
            SelectEmployeesTab();

            var listView = _driver.FindElementByAccessibilityId("ListViewEmployees");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            var occupation = listItems.First().FindElementByAccessibilityId("TextBlockOccupation").Text;
            var forename = listItems.First().FindElementByAccessibilityId("TextBlockForename").Text;

            Assert.NotEmpty(listItems);
            Assert.Equal("Driver", occupation);
            Assert.Equal("Janusz", forename);
        }

        [Fact]
        public void EmployeesTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            SelectEmployeesTab();

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

            _driver.FindElementByAccessibilityId("TextBoxOccupation").SendKeys("Technician");
            _driver.FindElementByAccessibilityId("TextBoxAddress").SendKeys("Oława ul. Wiśniowa 12/3");
            _driver.FindElementByAccessibilityId("TextBoxForename").SendKeys("Jakub");
            _driver.FindElementByAccessibilityId("TextBoxMiddlename").SendKeys("Mariusz");
            _driver.FindElementByAccessibilityId("TextBoxSurname").SendKeys("Kowalski");
            _driver.FindElementByAccessibilityId("TextBoxNationalIdentityNumber").SendKeys("837174628");
            _driver.FindElementByAccessibilityId("TextBoxWorkPhoneNumber").SendKeys("874123084");
            _driver.FindElementByAccessibilityId("TextBoxPrivatePhoneNumber").SendKeys("432503931");
            _driver.FindElementByAccessibilityId("TextBoxDrivingLicenseCategories").SendKeys("B");
            _driver.FindElementByAccessibilityId("DatePickerDrivingLicenseValidity").SendKeys("3.10.2035");
            _driver.FindElementByAccessibilityId("DatePickerHiredUntil").SendKeys("31.10.2025");

            _driver.FindElementByAccessibilityId("ButtonSave").Click();

            _driver.SwitchTo().Window(mainWindowHandle);

            var listView = _driver.FindElementByAccessibilityId("ListViewEmployees");

            var listItems = listView.FindElements(By.ClassName("ListViewItem"));

            Assert.NotEmpty(listItems);
            Assert.Contains(listItems, l =>
            l.FindElementByAccessibilityId("TextBlockOccupation").Text == "Technician" &&
            l.FindElementByAccessibilityId("TextBlockForename").Text == "Jakub");
        }

        [Fact]
        public void EmployeesTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}
