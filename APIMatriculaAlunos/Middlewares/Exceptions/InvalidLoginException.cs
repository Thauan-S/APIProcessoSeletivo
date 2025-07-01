using APIMatriculaAlunos.Middlewares.Exceptions;

namespace Tropical.Exceptions.Exceptions
{
    public class InvalidLoginException:MyApiException
    {

        public InvalidLoginException() : base("Email and or password is invalid")
        { 
            
        }
    }
}
