using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIMatriculaAlunos.Entities
{
    public class Student
    {
        [Key]
        [JsonIgnore]
        public int Id {get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string ResponsibleName { get; set; } = string.Empty;
        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        [JsonIgnore]
        public Class? Class { get; set; }
        // propriedades adicionais , não são obrigatórias no processo seletivo, mas quis dar um plus
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
