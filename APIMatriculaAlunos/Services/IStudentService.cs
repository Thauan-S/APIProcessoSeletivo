using APIMatriculaAlunos.Entities;
using APIMatriculaAlunos.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMatriculaAlunos.Services
{
    public interface IStudentService
    {
        Task<Result<List<StudentDto>>> GetAllAsync(PaginationParameters paginationParameters);
        Task<Result<StudentDto>> GetByIdAsync(int id);
        Task<Result<Student>> GetByEmailAsync(string  email);
        Task<Result<StudentDto>> AddAsync(Student student);
        Task<Result<StudentDto>> UpdateAsync(int id,Student student);
        Task<Result<StudentDto>> DeleteAsync(int id);
    }
} 