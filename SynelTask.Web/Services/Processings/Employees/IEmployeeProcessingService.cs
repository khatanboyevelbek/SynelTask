using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
        IQueryable<Employee> RetrieveAllEmployees(string orderBy);
        ValueTask<Employee> RetrieveEmployeeById(Guid employeeId);
        ValueTask<Employee> ModifyEmployeeAsync(Employee employee);
    }
}
