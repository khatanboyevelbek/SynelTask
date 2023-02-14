namespace SynelTask.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
    }
}
