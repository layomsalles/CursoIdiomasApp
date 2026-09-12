using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;

namespace CursoIdiomasApp.Domain.Interfaces.Services
{
    public interface IMatriculaService
    {
        MatriculaResponse CreateMatricula(MatriculaRequest matriculaRequest);
        List<MatriculaResponse> ConsultarPorTurma(Guid TurmaId);
        List<MatriculaResponse> ConsultarPorAluno(Guid AlunoId);
        MatriculaDeleteResponse CancelarMatricula(Guid Id);
    }
}
