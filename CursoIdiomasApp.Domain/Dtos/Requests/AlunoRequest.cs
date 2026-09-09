using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record AlunoRequest(
        string nome,
        Cpf cpf,
        Email email
        );
}
