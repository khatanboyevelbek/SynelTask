using Microsoft.EntityFrameworkCore;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Employee> Employees { get; set; }

        public async ValueTask<Employee> InsertEmployeeAsync(Employee employee) =>
            await InsertAsync(employee);

        public IQueryable<Employee> SelectAllEmployees() =>
            SelectAll<Employee>();

        public async ValueTask<Employee> SelectEmployeeByIdAsync(Guid employeeId) =>
            await SelectAsync<Employee>(employeeId);

        public async ValueTask<Employee> UpdateEmployeeAsync(Employee employee) =>
          await UpdateAsync(employee);

        public async ValueTask<Employee> DeleteEmployeeAsync(Employee employee) =>
          await DeleteAsync(employee);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployee(modelBuilder.Entity<Employee>());
        }
    }
}
