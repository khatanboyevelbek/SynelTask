using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void ConfigureEmployee(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e =>
                new { e.Id, e.EmailHome }).IsUnique();
        }
    }
}
