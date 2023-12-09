using Microsoft.AspNetCore.Mvc;
using SynelTask.Web.Brokers.Loggins;
using SynelTask.Web.Models.Foundations.Employees;
using SynelTask.Web.Services.Processings.Employees;

namespace SynelTask.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly ILogginBroker loggingBroker;
        private readonly IEmployeeProcessingService employeeProcessingService;

        public EmployeeController(ILogginBroker loggingBroker,
            IEmployeeProcessingService employeeProcessingService)
        {
            this.loggingBroker = loggingBroker;
            this.employeeProcessingService = employeeProcessingService;
        }
        public IActionResult Index(string orderBy = null)
        {
            IQueryable<Employee> employees =
                this.employeeProcessingService.RetrieveAllEmployees(orderBy);

            return View(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Employee employee = 
                await this.employeeProcessingService.RetrieveEmployeeById(id);

            return View(employee);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                await this.employeeProcessingService.ModifyEmployeeAsync(employee);
                TempData["success"] = "Successfuly updated";

                return RedirectToAction("Index", "Employee");
            }

            return View(employee);
        }
    }
}
