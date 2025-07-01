using APIMatriculaAlunos.Entities;
using Microsoft.AspNetCore.Mvc;
using APIMatriculaAlunos.Services;
using APIMatriculaAlunos.Validators;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APIMatriculaAlunos.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly StudentValidator _validator = new StudentValidator();

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _service.GetAllAsync(paginationParameters);
            if (!result.Success)
                return NoContent();
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Error);
            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(Student student)
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            await _service.AddAsync(student);

            return Created();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Student student)
        {
            var validationResult = _validator.Validate(student);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            await _service.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 