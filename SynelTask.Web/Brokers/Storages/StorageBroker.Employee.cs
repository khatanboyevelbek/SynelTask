using Microsoft.EntityFrameworkCore;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Employee> Employees { get; set; }

        public async ValueTask<Employee> InsertEmployeeAsync(Employee employee) =>
            await InsertAsync(employee);

        public IQueryable<Employee> SelectEmployeesAsync() =>
            SelectAll<Employee>();
    }
}
