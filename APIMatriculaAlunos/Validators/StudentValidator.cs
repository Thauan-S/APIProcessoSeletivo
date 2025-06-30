using FluentValidation;
using APIMatriculaAlunos.Entities;

namespace APIMatriculaAlunos.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do aluno  é obrigatório.");

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("A idade deve ser maior que zero.");

            RuleFor(x => x.ResponsibleName)
                .NotEmpty().WithMessage("O nome do responsável é obrigatório.");

            RuleFor(x => x.ClassId)
                .NotEmpty().WithMessage("A turma  do aluno deve ser informada");
        }
    }
}
