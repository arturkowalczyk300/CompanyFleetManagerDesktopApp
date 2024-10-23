using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerDesktopApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CompanyFleetManagerDesktopAppTests
{
    public class EmployeesViewModelTest
    {
        public static List<Employee> GetSampleEmployees()
        {
            return new List<Employee>() {
            new Employee(1, "Driver", "Wrocław Kwiatowa 1", "Janusz", "Mariusz", "Kowalski", 73213242123, new CompanyFleetManager.Models.PhoneNumber(48, 987654321),
                       null, new List<string> { "A", "B", "C", "T" }, new DateOnly(2030, 9, 1), new DateOnly(2027, 9, 9)),
                    new Employee(2, "Manager", "Trzebnica Milicka 34", "Andrzej", null, "Dobrzański", 67218242123, new CompanyFleetManager.Models.PhoneNumber(48, 527656421),
                        new CompanyFleetManager.Models.PhoneNumber(48, 537216421), new List<string> { "A2", "B" }, new DateOnly(2035, 2, 21), new DateOnly(2032, 3, 11)),
                    new Employee(3, "Security Guard", "Żórawina Dworcowa 23", "Aleksander", "Paweł", "Nowak", 83213452123, new CompanyFleetManager.Models.PhoneNumber(48, 327654321),
                        null, new List<string> { "A", "B", "C", "D" }, new DateOnly(2027, 6, 2), new DateOnly(2026, 2, 21))
                    };
        }

        private static Mock<FleetDatabaseContext> GetConfiguredMockContext(IQueryable<Employee> data)
        {
            Mock<DbSet<Employee>> mockSet = GetConfiguredMockDbSet(data);

            var mockContext = new Mock<FleetDatabaseContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);
            mockContext.Setup(c => c.Employees.Find(It.IsAny<object[]>())).Returns<object[]>(
                ids => mockSet.Object.FirstOrDefault(e => e.EmployeeId == (int)ids[0]));
            return mockContext;
        }

        private static Mock<DbSet<Employee>> GetConfiguredMockDbSet(IQueryable<Employee> data)
        {
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            return mockSet;
        }

        [Fact]
        public void Initialization_IsCorrect()
        {
            var viewModel = new EmployeesViewModel();

            Assert.Null(viewModel.Employees);
            Assert.Null(viewModel.SelectedEmployee);
        }

        [Fact]
        public void PropertyChanged_IsNotificationSent()
        {
            var viewModel = new EmployeesViewModel();
            bool notificationSent = false;
            viewModel.PropertyChanged += (s, e) => { notificationSent = e.PropertyName == "Employees"; };

            viewModel.Employees = new System.Collections.ObjectModel.ObservableCollection<CompanyFleetManager.Models.Entities.Employee>();

            Assert.True(notificationSent);
        }

        [Fact]
        public void LoadEmployees_PopulatesEmployeeCollection()
        {
            var data = GetSampleEmployees().AsQueryable();
            Mock<FleetDatabaseContext> mockContent = GetConfiguredMockContext(data);

            var viewModel = new EmployeesViewModel(mockContent.Object);

            viewModel.LoadEmployees();

            Assert.NotEmpty(viewModel.Employees);
            Assert.Equal(viewModel.Employees.Count, GetSampleEmployees().Count);
        }

        [Fact]
        public void AddEmployee_IncreasesEmployeesCount()
        {
            var data = new List<Employee>().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new EmployeesViewModel(mockContext.Object);

            var employeeToAdd = GetSampleEmployees()[0];

            viewModel.AddEmployee(employeeToAdd);

            mockContext.Verify(c => c.Employees.Add(It.Is<Employee>(e => e == employeeToAdd)), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ModifySelectedEmployee_UpdatesEmployee()
        {
            var data = GetSampleEmployees().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new EmployeesViewModel(mockContext.Object);

            var employeeToModify = GetSampleEmployees()[1];

            viewModel.SelectedEmployee = employeeToModify;
            viewModel.ModifySelectedEmployee(employeeToModify);

            mockContext.Verify(c => c.Employees.Update(It.Is<Employee>(e => e == employeeToModify)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteSelectedEmployee_RemovesEmployee()
        {
            var data = GetSampleEmployees().AsQueryable();
            var mockContext = GetConfiguredMockContext(data);
            var viewModel = new EmployeesViewModel(mockContext.Object);

            var employeeToDelete = GetSampleEmployees()[2];

            viewModel.SelectedEmployee = employeeToDelete;
            viewModel.DeleteSelectedEmployee();

            mockContext.Verify(c => c.Employees.Remove(It.Is<Employee>(e => e.EmployeeId == employeeToDelete.EmployeeId)), Times.Once());
            mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ModifySelectedEmployee_NullEmployee_ThrowException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteSelectedEmployee_NullEmployee_ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}