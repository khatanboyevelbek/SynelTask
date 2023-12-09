using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Test.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTest
    {
        [Fact]
        public void ShouldRetrieveAllEmployees()
        {
            // given
            IQueryable<Employee> randomEmployees = CreateRandomEmployees();
            IQueryable<Employee> storageEmployees = randomEmployees;

            IQueryable<Employee> expectedEmployees = 
                storageEmployees.DeepClone();

            this.storageBrokerMock.Setup(broker => 
                broker.SelectAllEmployees()).Returns(storageEmployees);

            // when
            IQueryable<Employee> actualEmployees =
                this.employeeServiceMock.RetrieveAllEmployees();

            // then
            actualEmployees.Should().BeEquivalentTo(expectedEmployees);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEmployees(), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
