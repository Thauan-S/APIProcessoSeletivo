using APIMatriculaAlunos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMatriculaAlunos.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(PaginationParameters paginationParameters);
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
} 