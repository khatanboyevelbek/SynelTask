using SynelTask.Web.Brokers.Loggings;
using SynelTask.Web.Services.Processings.Employees;

namespace SynelTask.Web.Services.Orchestrations.Employee
{
    public class EmployeeOrchestrationService : IEmployeeOrchestrationService
    {
        private readonly ILogginBroker loggingBroker;
        private readonly IEmployeeProcessingService employeeProcessingService;

        public EmployeeOrchestrationService(ILogginBroker loggingBroker, 
            IEmployeeProcessingService employeeProcessingService)
        {
            this.loggingBroker = loggingBroker;
            this.employeeProcessingService = employeeProcessingService;
        }

        public Task<int> ImportExternalFileToTable(IFormFile postedFile) => 
            this.employeeProcessingService.ImportExternalFileToTable(postedFile);
    }
}
