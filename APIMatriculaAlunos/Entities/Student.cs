using System.ComponentModel.DataAnnotations;

namespace APIMatriculaAlunos.Entities
{
    public class Student
    {
        [Key]
        public int Id {get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string ResponsibleName { get; set; } = string.Empty;
        public int ClassId { get; set; }

        // propriedades adicionais , não são obrigatórias no processo seletivo, mas quis dar um plus
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
