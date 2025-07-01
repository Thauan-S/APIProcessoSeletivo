using FluentValidation;
using APIMatriculaAlunos.Entities;
using Microsoft.AspNetCore.Identity;

namespace APIMatriculaAlunos.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("O nome do aluno  é obrigatório.");

            RuleFor(student => student.Age)
                .GreaterThan(0).WithMessage("A idade deve ser maior que zero.");

            RuleFor(student => student.ResponsibleName)
                .NotEmpty().WithMessage("O nome do responsável é obrigatório.");

            RuleFor(student => student.ClassId)
                .NotEmpty().WithMessage("A turma  do aluno deve ser informada");

            RuleFor(student => student.Email).NotEmpty().WithMessage("insira um email");
            RuleFor(student => student.Password).NotEmpty().WithMessage("insira uma senha");

            RuleFor(student => student.Password)
            .NotEmpty().WithMessage("Insira uma senha.")
            .MinimumLength(7).WithMessage("A senha deve ter no mínimo 7 caracteres.");

            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            { 
                RuleFor(user => user.Email).EmailAddress().WithMessage("Email inválido");
            });

        }
    }
}
