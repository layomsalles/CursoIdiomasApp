namespace CursoIdiomasApp.Domain.Dtos.Responses
{
    public record MatriculaResponse(
        Guid id,
        Guid alunoId,
        Guid turmaId,
        DateTime dataMatricula
        );
}
