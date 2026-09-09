using CursoIdiomasApp.Domain.Enums;

namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record TurmaRequest(
        int Numero,
        TurmaEnum AnoLetivo
        );
}
