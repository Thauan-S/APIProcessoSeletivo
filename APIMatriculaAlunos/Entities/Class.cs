using System.ComponentModel.DataAnnotations;

namespace APIMatriculaAlunos.Entities
{
    public class Class
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;

    }
}
