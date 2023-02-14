using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using SynelTask.Web.Brokers.Loggings;
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
                    var employee = new Employee()
                    {
                        Id = record.Id,
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
                        CreatedDate = record.CreatedDate,
                        UpdatedDate = record.UpdatedDate,
                    };

                    await this.employeeService.AddEmployeeAsync(employee);
                }

                return records.Count();
            }
        }

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

            throw new NotSupportedFileException();
        }
    }
}
