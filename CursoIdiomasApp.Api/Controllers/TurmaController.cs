using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomasApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController(ITurmaService turmaService) : ControllerBase
    {
        [HttpPost("CriarTurma")]
        public IActionResult CriarTurma([FromBody] TurmaRequest turmaRequest)
        {
            try
            {
                var response = turmaService.CreateTurma(turmaRequest);

                return StatusCode(201, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ListarTurma")]
        public IActionResult ListarTurma()
        {
            try
            {
                var response = turmaService.GetAllTurmas();

                return StatusCode(200, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeletarTurma/{id}")]
        public IActionResult DeletarTurma(Guid id) { 
            try
            {
                var response = turmaService.DeleteTurma(id);

                return StatusCode(200, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("AtualizarTurma/{id}")]
        public IActionResult AtualizarTurma([FromRoute] Guid id, [FromBody] TurmaRequest turmaRequest)
        {
            try
            {
                var response = turmaService.UpdateTurma(id, turmaRequest);

                return StatusCode(200, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
