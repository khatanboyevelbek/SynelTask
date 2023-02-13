using Microsoft.EntityFrameworkCore;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
