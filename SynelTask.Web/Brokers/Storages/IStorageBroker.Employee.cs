using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Employee> InsertEmployeeAsync(Employee employee);
        IQueryable<Employee> SelectAllEmployees();
        ValueTask<Employee> SelectEmployeeByIdAsync(Guid employeeId);
        ValueTask<Employee> UpdateEmployeeAsync(Employee employee);
        ValueTask<Employee> DeleteEmployeeAsync(Employee employee);
    }
}
