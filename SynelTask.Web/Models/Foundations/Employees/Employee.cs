namespace SynelTask.Web.Models.Foundations.Employees
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string PayrollNumber { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public string EmailHome { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
