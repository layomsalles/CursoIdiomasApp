using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomasApp.Api.Controllers
{
    [Route("api/v1/matricula")]
    [ApiController]
    public class MatriculaController(IMatriculaService matriculaService) : ControllerBase
    {
        [HttpPost("CriarMatricula")]
        public IActionResult CriarMatricula([FromBody] MatriculaRequest matriculaRequest) {
            try
            {
                var response = matriculaService.CreateMatricula(matriculaRequest);

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

        [HttpDelete("CancelarMatricula/{id}")]
        public IActionResult CancelarMatricula([FromRoute] Guid id)
        {
            try
            {
                var response = matriculaService.CancelarMatricula(id);
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

        [HttpGet("ListarPorTurma/{turmaId}")]
        public IActionResult ListarPorTurma([FromRoute] Guid turmaId)
        {
            try
            {

                var response = matriculaService.ConsultarPorTurma(turmaId);

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


        [HttpGet("ListarPorAluno/{alunoId}")]
        public IActionResult ListarPorAluno([FromRoute] Guid alunoId) {
            try
            {
                var response = matriculaService.ConsultarPorAluno(alunoId);

                return StatusCode(200, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500 , ex.Message);
            }
        }
    }
}
