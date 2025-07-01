using APIMatriculaAlunos.Entities;
using Microsoft.AspNetCore.Mvc;
using APIMatriculaAlunos.Services;
using APIMatriculaAlunos.Validators;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using APIMatriculaAlunos.Entities.Dtos;
using APIMatriculaAlunos.Middlewares.Exceptions;

namespace APIMatriculaAlunos.Controllers
{
    /// <summary>
    /// Controller responsável por operações de CRUD de alunos.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista paginada de alunos.
        /// </summary>
        /// <param name="paginationParameters">Parâmetros de paginação.</param>
        /// <returns>Lista de alunos.</returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _service.GetAllAsync(paginationParameters);
            if (!result.Success)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Retorna um aluno pelo ID.
        /// </summary>
        /// <param name="id">ID do aluno.</param>
        /// <returns>Aluno correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
                return NotFound( new ResponseErrorJson( result.Error!));
            return Ok(result.Data);
        }

        /// <summary>
        /// Cria um novo aluno.
        /// </summary>
        /// <param name="student">Dados do aluno a ser criado.</param>
        /// <returns>Aluno criado.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(Student student)
        {
           Validate(student);

            var result =await _service.AddAsync(student);
            if (!result.Success)
                return BadRequest(result.Error);
            var resultDto = StudentDto.EntityToDto(student);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, result);
        }

        /// <summary>
        /// Atualiza um aluno existente.
        /// </summary>
        /// <param name="id">ID do aluno a ser atualizado.</param>
        /// <param name="student">Novos dados do aluno.</param>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromQuery]int id, Student student)
        {
            Validate(student);

            var result =await _service.UpdateAsync(id,student);
            if (!result.Success)
                return BadRequest(new ResponseErrorJson(result.Error!));
            return NoContent();
        }

        /// <summary>
        /// Remove um aluno pelo ID.
        /// </summary>
        /// <param name="id">ID do aluno a ser removido.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var result=await _service.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(new ResponseErrorJson(result.Error!));
            return NoContent();
        }
        private  void Validate(Student student)
        {
            var validator = new StudentValidator();

            var result = validator.Validate(student);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
} 