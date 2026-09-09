using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Dtos.Responses
{
    public record AlunoResponse(
        Guid id,
        string nome,
        Cpf cpf,
        Email email
        );
}
