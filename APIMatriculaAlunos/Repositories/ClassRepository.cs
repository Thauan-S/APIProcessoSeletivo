using APIMatriculaAlunos.Context;
using APIMatriculaAlunos.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIMatriculaAlunos.Repositories
{
    public class ClassRepository:IClassRepository
    {
        private readonly AppDbContext _context;
        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClassExists(int id)
        {
            return await _context.Classes.AnyAsync(c => c.Id == id);
        }

        public async Task<Class?> GetByIdAsync(int classId)
        {
            return await _context.Classes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == classId);
        }
    }
}
