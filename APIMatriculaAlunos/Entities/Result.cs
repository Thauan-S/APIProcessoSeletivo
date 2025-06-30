namespace APIMatriculaAlunos.Entities
{
    public class Result<T>
    {
        // Criei esta classe de resultados para evitar o lançamento de exceções em casos de erro controlado.
        // A classe Result encapsula o resultado de uma operação, indicando se foi bem-sucedida, os dados retornados e, se aplicável, uma mensagem de erro.
        // Lançar exceções, por exemplo, quando um dado não é encontrado no banco de dados, pode gerar sobrecarga de processamento (overhead),
        // além de não representar necessariamente uma falha inesperada.
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