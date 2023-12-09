using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SynelTask.Web.Brokers.Loggins;
using SynelTask.Web.Models;
using SynelTask.Web.Models.Foundations.Employees;
using SynelTask.Web.Models.Foundations.Employees.Exceptions;
using SynelTask.Web.Services.Processings.Employees;

namespace SynelTask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogginBroker loggingBroker;
        private readonly IEmployeeProcessingService employeeProcessingService;

        public HomeController(ILogginBroker loggingBroker,
            IEmployeeProcessingService employeeProcessingService)
        {
            this.loggingBroker = loggingBroker;
            this.employeeProcessingService = employeeProcessingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ValueTask<IActionResult> Index(IFormFile postedFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rows =
                        await this.employeeProcessingService.ImportExternalFileToTable(postedFile);

                    TempData["success"] = string.Format("{0} rows added successfuly", rows);

                    return RedirectToAction("Index","Employee");
                }

                this.loggingBroker.LogError("File is required");
                TempData["error"] = "File is required";

                return View();
            }
            catch (NotSupportedFileException exception)
            {
                this.loggingBroker.LogError(exception.Message);
                TempData["error"] = $"{exception.Message}";
                return View();
            }
            catch (InvalidCastException exception)
            {
                this.loggingBroker.LogError("Invalid cast error occured");
                return RedirectToAction("Error");
            }
            catch (FormatException exception)
            {
                this.loggingBroker.LogError("Format error occured");
                return RedirectToAction("Error");
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError("Internal server error");
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}