

using APIMatriculaAlunos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tropical.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) :base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
