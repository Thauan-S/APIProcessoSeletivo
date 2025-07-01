namespace APIMatriculaAlunos.Middlewares.Exceptions
{
    public class MyApiException : SystemException
    {
        public MyApiException(string message) : base(message) { }
    }
}
