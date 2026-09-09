namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record MatriculaRequest(
        Guid AlunoId,
        Guid TurmaId
        );
}
