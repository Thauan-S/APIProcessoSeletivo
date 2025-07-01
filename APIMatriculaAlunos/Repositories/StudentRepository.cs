using APIMatriculaAlunos.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tropical.Infrastructure.Data;

namespace APIMatriculaAlunos.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync(PaginationParameters paginationParameters)
        {
            return await _context.Students.AsNoTracking()
                .Skip((paginationParameters.Page - 1) * paginationParameters.PageSize)
                .OrderByDescending(student => student.Id)
                .Take(paginationParameters.PageSize)
                .ToListAsync();
            
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Student?> GetByEmailAsync(string email)
        {
            return _context.Students.FirstOrDefaultAsync(s => s.Email.Equals(email));
        }
    }
} 