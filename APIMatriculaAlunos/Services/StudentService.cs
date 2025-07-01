using APIMatriculaAlunos.Entities;
using APIMatriculaAlunos.Repositories;
using APIMatriculaAlunos.Utils;
using BCrypt.Net;


namespace APIMatriculaAlunos.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<Student>>> GetAllAsync(PaginationParameters paginationParameters)
        {
            var result = await _repository.GetAllAsync(paginationParameters);
            if (result == null)
                return Result<List<Student>>.Fail("Nenhum aluno encontrado para os parâmetros informados.");
            return Result<List<Student>>.Ok(result);
        }

        public async Task<Result<Student>> GetByIdAsync(int id)
        {
            var repoResult = await _repository.GetByIdAsync(id);
            if (repoResult==null)
                return Result<Student>.Fail("Aluno não existe ou foi removido.");
            return Result<Student>.Ok(repoResult);
        }

        public async Task AddAsync(Student student)
        {
            student.Password= BCrypt.Net.BCrypt.HashPassword(student.Password);
            await _repository.AddAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            var studentDb = await _repository.GetByIdAsync(student.Id);
            SecurityUtils.VerifyOwnerShip(studentDb.Id.ToString());
            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Result<Student>> GetByEmailAsync(string email)
        {
            //return await _repository.GetByEmailAsync(email);
            var result = await _repository.GetByEmailAsync(email);
            if (result == null)
                return Result<Student>.Fail("Erro ao buscar email.");
            return Result<Student>.Ok(result);
        }
    }
} 