namespace APIMatriculaAlunos.Middlewares.Exceptions
{
    public class ErrorOnValidationException : MyApiException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        }
    }
}

