using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using SynelTask.Web.Brokers.Loggins;
using SynelTask.Web.Models.Foundations.Employees.Exceptions;
using SynelTask.Web.Services.Foundations.Employees;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Services.Processings.Employees
{
    public class EmployeeProcessingService : IEmployeeProcessingService
    {
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogginBroker loggingBroker;

        public EmployeeProcessingService(IEmployeeService employeeService,
            IWebHostEnvironment hostingEnvironment, ILogginBroker loggingBroker)
        {
            this.employeeService = employeeService;
            this.hostingEnvironment = hostingEnvironment;
            this.loggingBroker = loggingBroker;
        }

        public async Task<int> ImportExternalFileToTable(IFormFile postedFile)
        {
            string uploadedFilePath = await UploadFileAndGetFilePath(postedFile);
            int rows = 0;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(uploadedFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<EmployeeClassMap>();
                var records = csv.GetRecords<Employee>();

                foreach (Employee record in records)
                {
                    rows++;

                    var employee = new Employee()
                    {
                        Id = Guid.NewGuid(),
                        PayrollNumber = record.PayrollNumber,
                        Forename = record.Forename,
                        Surname = record.Surname,
                        DateOfBirth = record.DateOfBirth,
                        Telephone = record.Telephone,
                        Mobile = record.Mobile,
                        Address1 = record.Address1,
                        Address2 = record.Address2,
                        Postcode = record.Postcode,
                        EmailHome = record.EmailHome,
                        StartDate = record.StartDate,
                        CreatedDate = DateTimeOffset.Now,
                        UpdatedDate = DateTimeOffset.Now,
                    };

                    await this.employeeService.AddEmployeeAsync(employee);
                }

                return rows;
            }
        }

        public IQueryable<Employee> RetrieveAllEmployees(string orderBy)
        {
            IQueryable<Employee> employees = 
                this.employeeService.RetrieveAllEmployees();

            return orderBy switch
            {
                "forename" => employees.OrderBy(e => e.Forename),
                "surname" => employees.OrderBy(e => e.Surname),
                _ => employees
            };
        }

        public async ValueTask<Employee> RetrieveEmployeeById(Guid employeeId) =>
            await this.employeeService.RetrieveEmployeeByIdAsync(employeeId);

        public async ValueTask<Employee> ModifyEmployeeAsync(Employee employee) =>
            await this.employeeService.ModifyEmployeeAsync(employee);

        private async Task<string> UploadFileAndGetFilePath(IFormFile postedFile)
        {
            string pathOfFile = string.Empty;
            List<string> supportedTypes = new() { ".csv" };
            FileInfo fileInfo = new FileInfo(postedFile.FileName);

            if (supportedTypes.Contains(fileInfo.Extension))
            {
                string fileName = $"{Guid.NewGuid()}{postedFile.FileName}";

                string uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                pathOfFile = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(pathOfFile, FileMode.CreateNew, FileAccess.Write))
                {
                    await postedFile.CopyToAsync(stream);
                }

                return pathOfFile;
            }

            this.loggingBroker.LogError("File type is not supported. Try again");
            throw new NotSupportedFileException();
        }
    }
}
