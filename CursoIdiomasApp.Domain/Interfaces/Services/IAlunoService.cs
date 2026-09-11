using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;

namespace CursoIdiomasApp.Domain.Interfaces.Services
{
    public interface IAlunoService
    {
        AlunoResponse CreateAluno(AlunoRequest alunoRequest);
        List<AlunoResponse> GetAllAlunos();
        AlunoUpdateRequest DeleteAluno();
        AlunoResponse UpdateAluno(AlunoUpdateRequest alunoUpdateRequest);
    }
}
