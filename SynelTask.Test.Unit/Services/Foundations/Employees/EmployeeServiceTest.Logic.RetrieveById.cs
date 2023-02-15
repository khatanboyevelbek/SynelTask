using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Test.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTest
    {
        [Fact]
        public async Task ShoudRetrieveEmployeeByIdAsync()
        {
            // given
            Guid randomEmployeeId = Guid.NewGuid();
            Employee randomEmployee = CreateRandomEmployee();
            Employee storageEmployee = randomEmployee;
            Employee expectedEmployee = storageEmployee.DeepClone();

            this.storageBrokerMock.Setup(broker => 
                broker.SelectEmployeeByIdAsync(randomEmployeeId))
                    .ReturnsAsync(storageEmployee);

            // when
            Employee actualEmployee =
                await this.employeeServiceMock
                    .RetrieveEmployeeByIdAsync(randomEmployeeId);

            // then
            actualEmployee.Should().BeEquivalentTo(expectedEmployee);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEmployeeByIdAsync(It.IsAny<Guid>()), 
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
