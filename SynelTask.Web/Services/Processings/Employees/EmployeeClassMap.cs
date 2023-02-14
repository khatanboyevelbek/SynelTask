using CsvHelper.Configuration;
using SynelTask.Web.Models.Foundations.Employees;

namespace SynelTask.Web.Services.Processings.Employees
{
    public class EmployeeClassMap : ClassMap<Employee>
    {
        public EmployeeClassMap()
        {
            Map(e => e.PayrollNumber).Name("Personnel_Records.Payroll_Number");
            Map(e => e.Forename).Name("Personnel_Records.Forenames");
            Map(e => e.Surname).Name("Personnel_Records.Surname");
            Map(e => e.DateOfBirth).Name("Personnel_Records.Date_of_Birth");
            Map(e => e.Telephone).Name("Personnel_Records.Telephone");
            Map(e => e.Mobile).Name("Personnel_Records.Mobile");
            Map(e => e.Address1).Name("Personnel_Records.Address");
            Map(e => e.Address2).Name("Personnel_Records.Address_2");
            Map(e => e.Postcode).Name("Personnel_Records.Postcode");
            Map(e => e.EmailHome).Name("Personnel_Records.EMail_Home");
            Map(e => e.StartDate).Name("Personnel_Records.Start_Date");
        }
    }
}
