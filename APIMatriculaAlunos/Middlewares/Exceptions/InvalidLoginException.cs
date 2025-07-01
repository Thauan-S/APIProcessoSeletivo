namespace APIMatriculaAlunos.Middlewares.Exceptions
{
    public class InvalidLoginException : MyApiException
    {

        public InvalidLoginException() : base("Email and or password is invalid")
        {

        }
    }
}
