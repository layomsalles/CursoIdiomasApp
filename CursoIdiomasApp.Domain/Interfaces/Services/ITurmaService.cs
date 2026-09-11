using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;

namespace CursoIdiomasApp.Domain.Interfaces.Services
{
    public interface ITurmaService
    {
        TurmaResponse CreateTurma(TurmaRequest turmaRequest);
        List<TurmaResponse> GetAllTurmas();
        TurmaDeleteResponse DeleteTurma();
        TurmaResponse UpdateTurma(TurmaRequest turmaRequest);
    }
}
