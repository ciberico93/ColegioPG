using AppColegioPG.DTOs;
using AppColegioPG.Models;
using AppColegioPG.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppColegioPG.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : Controller
    {

        private readonly IMetodos<Cursos> _CursoServices;
        private readonly IMapper _mapper;

        public CursosController(IMetodos<Cursos> cursoServices, IMapper mapper)
        {
            _CursoServices = cursoServices;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CursosDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListaCursos()
        {
            try
            {
                var cursos = await _CursoServices.GetAll();
                var cursosDTO = _mapper.Map<Cursos>(cursos);
                return Ok(cursosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la lista de cursos.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CursosDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCursoById(int id)
        {
            try
            {
                var result = await _CursoServices.GetById(id);
                if (result == null)
                {
                    return NotFound($"Curso con ID = {id} no encontrado.");
                }
                var cursoDTO = _mapper.Map<Cursos>(result);
                return Ok(cursoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el curso.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CursosDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarCurso([FromBody] CursosDTO cursoDTO)
        {
            if (cursoDTO == null)
            {
                return BadRequest("Datos del curso inválidos.");
            }

            try
            {
                var curso = _mapper.Map<Cursos>(cursoDTO);
                var result = await _CursoServices.Create(curso);
                var createdCursoDTO = _mapper.Map<CursosDTO>(result);
                return CreatedAtAction(nameof(GetCursoById), new { id = createdCursoDTO.Id_cursos }, createdCursoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar el curso.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            try
            {
                var result = await _CursoServices.Delete(id);
                if (!result)
                {
                    return NotFound($"Curso con ID = {id} no encontrado.");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el curso.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CursosDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCurso([FromBody] CursosDTO cursoDTO)
        {
            if (cursoDTO == null)
            {
                return BadRequest("Datos del curso inválidos.");
            }

            try
            {
                var curso = _mapper.Map<Cursos>(cursoDTO);
                var result = await _CursoServices.Update(curso);
                if (result == null)
                {
                    return NotFound($"Curso con ID = {cursoDTO.Id_cursos} no encontrado.");
                }
                var updatedCursoDTO = _mapper.Map<CursosDTO>(result);
                return Ok(updatedCursoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el curso.");
            }
        }
    }
}
