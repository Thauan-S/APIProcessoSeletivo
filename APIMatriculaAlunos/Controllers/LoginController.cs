using APIMatriculaAlunos.Entities;
using APIMatriculaAlunos.Entities.Dtos;
using APIMatriculaAlunos.Middlewares.Exceptions;
using APIMatriculaAlunos.Services;
using Microsoft.AspNetCore.Mvc;


namespace APIMatriculaAlunos.Controllers
{
    [ApiController]
    [Route("/api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokenService;

        public LoginController(IStudentService studentService, ITokenService tokenService)
        {
            _studentService = studentService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User userlogin)
        {
            var student = await _studentService.GetByEmailAsync(userlogin.Email);

            if (student == null)
            {
                return BadRequest(new { message = "Invalid credentials: User not found" });
            }
            if (!BCrypt.Net.BCrypt.Verify(userlogin.Password, student.Data.Password))
            {
                throw new InvalidLoginException();
            }

            var token = _tokenService.GenerateToken(student.Data.Id.ToString());
            return Ok(new TokenResponse { Token = token });

        }
    }
}
