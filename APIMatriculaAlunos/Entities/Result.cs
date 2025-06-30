namespace APIMatriculaAlunos.Entities
{
    public class Result<T>
    {
        // Criei esta classe de resultados para evitar o lan�amento de exce��es em casos de erro controlado.
        // A classe Result encapsula o resultado de uma opera��o, indicando se foi bem-sucedida, os dados retornados e, se aplic�vel, uma mensagem de erro.
        // Lan�ar exce��es, por exemplo, quando um dado n�o � encontrado no banco de dados, pode gerar sobrecarga de processamento (overhead),
        // al�m de n�o representar necessariamente uma falha inesperada.
        public bool Success { get; }
        public T? Data { get; }
        public string? Error { get; }

        private Result(bool success, T? data, string? error)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public static Result<T> Ok(T data) => new Result<T>(true, data, null);
        public static Result<T> Fail(string error) => new Result<T>(false, default, error);
    }
} 