using APIMatriculaAlunos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMatriculaAlunos.Services
{
    public interface IStudentService
    {
        Task<Result<List<Student>>> GetAllAsync(PaginationParameters paginationParameters);
        Task<Result<Student>> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
} 