using Moq;
using SynelTask.Web.Brokers.Storages;
using SynelTask.Web.Models.Foundations.Employees;
using SynelTask.Web.Services.Foundations.Employees;
using Tynamix.ObjectFiller;

namespace SynelTask.Test.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTest
    {
        public readonly Mock<IStorageBroker> storageBrokerMock;
        public readonly IEmployeeService employeeServiceMock;

        public EmployeeServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.employeeServiceMock =
                new EmployeeService(this.storageBrokerMock.Object);
        }

        private static Employee CreateRandomEmployee() =>
           CreateEmployeeFiller(date: GetRandomDateTimeOffset()).Create();

        private static  IQueryable<Employee> CreateRandomEmployees() =>
            CreateEmployeeFiller(date: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
       
        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Employee> CreateEmployeeFiller(DateTimeOffset date)
        {
            var filler = new Filler<Employee>();
            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
