
using APIMatriculaAlunos.Middlewares.Exceptions;

namespace Tropical.Exceptions.Exceptions
{
    public class ErrorOnValidationException : MyApiException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages):base(string.Empty) // passa uma string vazia para mytropical exception
        {
            ErrorMessages = errorMessages;
        }
    }
}

