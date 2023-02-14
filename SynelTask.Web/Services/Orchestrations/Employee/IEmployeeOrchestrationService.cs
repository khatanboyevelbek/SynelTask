namespace SynelTask.Web.Services.Orchestrations.Employee
{
    public interface IEmployeeOrchestrationService
    {
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
    }
}
