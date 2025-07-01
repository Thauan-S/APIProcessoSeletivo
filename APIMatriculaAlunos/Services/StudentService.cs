using APIMatriculaAlunos.Entities;
using APIMatriculaAlunos.Entities.Dtos;
using APIMatriculaAlunos.Repositories;
using APIMatriculaAlunos.Utils;
using BCrypt.Net;


namespace APIMatriculaAlunos.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IClassRepository _classRepository;
        public StudentService(IStudentRepository repository, IClassRepository classRepository = null)
        {
            _repository = repository;
            _classRepository = classRepository;
        }

        public async Task<Result<List<StudentDto>>> GetAllAsync(PaginationParameters paginationParameters)
        {
            
            var result = await _repository.GetAllAsync(paginationParameters);
            if (result == null)
                return Result<List<StudentDto>>.Fail("Nenhum aluno encontrado para os parâmetros informados.");

            var resultDto = StudentDto.EntityToDto(result);
            return Result<List<StudentDto>>.Ok(resultDto);
        }

        public async Task<Result<StudentDto>> GetByIdAsync(int id)
        {
            var repoResult = await _repository.GetByIdAsync(id);
            if (repoResult==null)
                return Result<StudentDto>.Fail("Aluno não existe na base de dados ");

           var resultDto= StudentDto.EntityToDto(repoResult);
            return Result<StudentDto>.Ok(resultDto);
        }

        public async Task<Result<StudentDto>> AddAsync(Student student)
        {
            var emailAlreadyExists = await _repository.GetByEmailAsync(student.Email);
            if (emailAlreadyExists != null)
                return Result<StudentDto>.Fail("Email já cadastrado");

            var classExists= await _classRepository.ClassExists(student.ClassId);
            if(!classExists)
                return Result < StudentDto>.Fail("Classe não existe na base de dados");

            student.Password= BCrypt.Net.BCrypt.HashPassword(student.Password);
            await _repository.AddAsync(student);
            return Result<StudentDto>.Ok(StudentDto.EntityToDto(student));
        }

        public async Task<Result<StudentDto>> UpdateAsync(int id,Student student)
        {
            var studentDb = await _repository.GetByIdAsync(id);
            if (studentDb ==null)
                return Result<StudentDto>.Fail("Aluno não cadastrado na base de dados ");
            SecurityUtils.VerifyOwnerShip(studentDb.Id.ToString());

            studentDb.Name = student.Name;
            studentDb.Age = student.Age;
            studentDb.ClassId = student.ClassId;
            studentDb.ResponsibleName = student.ResponsibleName;
            studentDb.Email = student.Email;
            studentDb.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);

            await _repository.UpdateAsync(studentDb);

            return Result<StudentDto>.Ok(new StudentDto { });
        }

        public async Task<Result<StudentDto>> DeleteAsync(int id)
        {
            var studentDb = await _repository.GetByIdAsync(id);
            if (studentDb == null)
                return Result<StudentDto>.Fail("Student not exists");

            SecurityUtils.VerifyOwnerShip(studentDb.Id.ToString());
           
            await _repository.DeleteAsync(id);
            return Result<StudentDto>.Ok(new StudentDto { });
        }

        public async Task<Result<Student>> GetByEmailAsync(string email)
        {
            
            var result = await _repository.GetByEmailAsync(email);
            if (result == null)
                return Result<Student>.Fail("Erro ao buscar email.");
            return Result<Student>.Ok(result);
        }
    }
} 