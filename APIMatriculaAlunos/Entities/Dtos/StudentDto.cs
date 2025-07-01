

namespace APIMatriculaAlunos.Entities.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string ResponsibleName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public static List<StudentDto> EntityToDto(List<Student> students) {
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                ResponsibleName = s.ResponsibleName,
                ClassId = s.ClassId,
                ClassName=s.Class.Name,
                Email = s.Email
                
            }).ToList();
        }
        public static StudentDto EntityToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                ResponsibleName = student.ResponsibleName,
                ClassId = student.ClassId,
                Email = student.Email
            };
        }
    }
}
