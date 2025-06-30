﻿using System.ComponentModel.DataAnnotations;

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
    }
}
