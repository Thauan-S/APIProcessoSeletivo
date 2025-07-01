using APIMatriculaAlunos.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIMatriculaAlunos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
    }
}
