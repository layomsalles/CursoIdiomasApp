using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomasApp.Api.Controllers
{
    [Route("api/v1/aluno")]
    [ApiController]
    public class AlunoController(IAlunoService alunoService) : ControllerBase
    {
        [HttpPost("CriarAluno")]
        public IActionResult CriarAluno([FromBody] AlunoRequest alunoRequest) {
            try
            {
                var response = alunoService.CreateAluno(alunoRequest);

                return StatusCode(201, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        [HttpDelete("DeletarAluno/{id}")]
        public IActionResult DeletarAluno(Guid id)
        {
            try
            {
                var response = alunoService.DeleteAluno(id);

                return StatusCode(200, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ListarAluno")]
        public IActionResult ListarAluno()
        {
            try
            {
                var response = alunoService.GetAllAlunos();

                return StatusCode(200, response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("AtualizarAluno/{id}")]
        public IActionResult AtualizarAluno([FromRoute] Guid id, [FromBody] AlunoUpdateRequest alunoUpdateRequest) {

            try
            {
                var response = alunoService.UpdateAluno(id, alunoUpdateRequest);

                return StatusCode(200, response);
            }
            catch (ApplicationException ex) {
                return StatusCode(400, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
