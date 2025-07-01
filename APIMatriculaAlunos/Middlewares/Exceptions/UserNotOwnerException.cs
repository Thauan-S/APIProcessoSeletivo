namespace APIMatriculaAlunos.Middlewares.Exceptions
{
    public class UserNotOwnerException : MyApiException
    {
        public UserNotOwnerException() : base("Usuário não pode realizar essa operação") 
        {
           
        }
    }
}

