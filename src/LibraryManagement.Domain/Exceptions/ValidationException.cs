namespace LibraryManagement.Domain.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors) 
            : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
