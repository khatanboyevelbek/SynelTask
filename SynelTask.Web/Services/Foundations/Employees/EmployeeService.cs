using SynelTask.Web.Brokers.Storages;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Services.Foundations.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IStorageBroker storageBroker;

        public EmployeeService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Employee> AddEmployeeAsync(Employee employee) =>
            await this.storageBroker.InsertEmployeeAsync(employee);
    }
}
