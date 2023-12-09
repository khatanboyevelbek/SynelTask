using Xeptions;

namespace SynelTask.Web.Models.Foundations.Employees.Exceptions
{
    public class NotSupportedFileException : Xeption
    {
        public NotSupportedFileException()
           : base("File type is not supported. Try again")
        { }
    }
}
