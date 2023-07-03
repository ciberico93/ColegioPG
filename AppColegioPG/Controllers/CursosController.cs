using AppColegioPG.Models;
using AppColegioPG.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AppColegioPG.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : Controller
    {

        private readonly IMetodos<Cursos> _CursoServices;

        public CursosController(IMetodos<Cursos> cursoServices)
        {
            _CursoServices = cursoServices;
        }

        [HttpGet]
      
        public  async Task<IActionResult> ListaCursos()
        {
            var cursos = await _CursoServices.GetAll();
            return Ok(cursos);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCursosById(int id)
        {
            var result = await _CursoServices.GetById(id);
            return Ok(result);
        }
    }
}
