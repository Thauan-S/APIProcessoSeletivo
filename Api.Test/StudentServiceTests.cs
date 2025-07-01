using APIMatriculaAlunos.Entities;
using APIMatriculaAlunos.Repositories;
using APIMatriculaAlunos.Services;
using APIMatriculaAlunos.Utils;
using Moq;


namespace Api.Test
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _studentRepoMock;
        private readonly Mock<IClassRepository> _classRepoMock;
        private readonly Mock<ISecurityUtils> _securityUtilsMock;
        private readonly StudentService _service;
        private readonly Class Classe = new Class
        {
            Id = 1,
            Name = "Class A"
        };
        private Student student = new Student
        {
            Id = 1,
            Name = "Test",
            Email = "test@email.com",
            ClassId = 1,
            Class= new Class
            {
                Id = 1,
                Name = "Class A"
            }
        };
      
        public StudentServiceTests()
        {
            _studentRepoMock = new Mock<IStudentRepository>();
            _classRepoMock = new Mock<IClassRepository>();
            _securityUtilsMock = new Mock<ISecurityUtils>();
            _service = new StudentService(_studentRepoMock.Object, _classRepoMock.Object, _securityUtilsMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsStudents_WhenStudentsExist()
        {
            // Arrange
            var students = new List<Student> { student };
            var pagination = new PaginationParameters();
            _studentRepoMock.Setup(r => r.GetAllAsync(pagination)).ReturnsAsync(students);
            _classRepoMock.Setup(r => r.GetByIdAsync(student.ClassId)).ReturnsAsync(Classe);
            // Act
            var result = await _service.GetAllAsync(pagination);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Test", result.Data[0].Name);
            Assert.Equal(1, result.Data[0].Id);
            Assert.Equal("test@email.com", result.Data[0].Email);
            Assert.Equal(1, result.Data[0].ClassId);
            _studentRepoMock.Verify(r => r.GetAllAsync(pagination), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsFail_WhenNoStudentsFound()
        {
            // Arrange
            var pagination = new PaginationParameters();
            _studentRepoMock.Setup(r => r.GetAllAsync(pagination)).ReturnsAsync((List<Student>)null);

            // Act
            var result = await _service.GetAllAsync(pagination);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal("Nenhum aluno encontrado para os parâmetros informados.",result.Error);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsStudent_WhenExists()
        {
            // Arrange
 
            _studentRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Test", result.Data.Name);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("test@email.com", result.Data.Email);
            Assert.Equal(1, result.Data.ClassId);

            _studentRepoMock.Verify(r => r.GetByIdAsync(student.Id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsFail_WhenNotExists()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Student)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal("Aluno não existe na base de dados ", result.Error);
        }

        [Fact]
        public async Task AddAsync_ReturnsFail_WhenEmailAlreadyExists()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByEmailAsync(student.Email)).ReturnsAsync(student);

            // Act
            var result = await _service.AddAsync(student);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email já cadastrado", result.Error);
            _studentRepoMock.Verify(r => r.GetByEmailAsync(student.Email), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsFail_WhenClassDoesNotExist()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByEmailAsync(student.Email)).ReturnsAsync((Student)null);
            _classRepoMock.Setup(r => r.ClassExists(student.ClassId)).ReturnsAsync(false);

            // Act
            var result = await _service.AddAsync(student);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Classe não existe na base de dados", result.Error);
            _studentRepoMock.Verify(r => r.GetByEmailAsync(student.Email), Times.Once);
            _classRepoMock.Verify(r => r.ClassExists(student.ClassId), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsOk_WhenStudentIsAdded()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByEmailAsync(student.Email)).ReturnsAsync((Student)null);
            _classRepoMock.Setup(r => r.ClassExists(student.ClassId)).ReturnsAsync(true);
            _studentRepoMock.Setup(r => r.AddAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddAsync(student);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(student.Name, result.Data.Name);
            Assert.Equal(student.Email, result.Data.Email);
            _studentRepoMock.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Once);
            _classRepoMock.Verify(r => r.ClassExists(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFail_WhenStudentNotExists()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByIdAsync(student.Id)).ReturnsAsync((Student)null);

            // Act
            var result = await _service.DeleteAsync(student.Id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Student not exists", result.Error);
            _studentRepoMock.Verify(r => r.GetByIdAsync(student.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOk_WhenStudentDeleted()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByIdAsync(student.Id)).ReturnsAsync(student);
            _studentRepoMock.Setup(r => r.DeleteAsync(student.Id)).Returns(Task.CompletedTask);
            _securityUtilsMock.Setup(s => s.VerifyOwnerShip(student.Id.ToString()));

            // Act
            var result = await _service.DeleteAsync(student.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            _studentRepoMock.Verify(r => r.GetByIdAsync(student.Id), Times.Once);
            _studentRepoMock.Verify(r => r.DeleteAsync(student.Id), Times.Once);
            _securityUtilsMock.Verify(s => s.VerifyOwnerShip(student.Id.ToString()), Times.Once);
        }

        [Fact]
        public async Task GetByEmailAsync_ReturnsOk_WhenStudentExists()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByEmailAsync(student.Email)).ReturnsAsync(student);

            // Act
            var result = await _service.GetByEmailAsync(student.Email);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(student.Email, result.Data.Email);
            _studentRepoMock.Verify(r => r.GetByEmailAsync(student.Email), Times.Once);
        }

        [Fact]
        public async Task GetByEmailAsync_ReturnsFail_WhenStudentNotExists()
        {
            // Arrange
            _studentRepoMock.Setup(r => r.GetByEmailAsync(student.Email)).ReturnsAsync((Student)null);

            // Act
            var result = await _service.GetByEmailAsync(student.Email);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao buscar email.", result.Error);
            _studentRepoMock.Verify(r => r.GetByEmailAsync(student.Email), Times.Once);
        }
    }
} 