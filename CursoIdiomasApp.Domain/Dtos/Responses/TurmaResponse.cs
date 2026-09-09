using CursoIdiomasApp.Domain.Enums;

namespace CursoIdiomasApp.Domain.Dtos.Responses
{
    public record TurmaResponse(
        Guid id,
        int numero,
        TurmaEnum anoLetivo
        );
}
