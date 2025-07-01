using APIMatriculaAlunos.Entities;

namespace APIMatriculaAlunos.Repositories
{
    public interface IClassRepository
    {
        public Task<bool> ClassExists(int id);
        public Task<Class?> GetByIdAsync(int classId);
    }
}
