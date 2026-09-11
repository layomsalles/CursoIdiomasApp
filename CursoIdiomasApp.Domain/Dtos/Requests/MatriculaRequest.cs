using CursoIdiomasApp.Domain.Validations;


namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record MatriculaRequest(
        [NotEmptyGuid(ErrorMessage = "Id do Aluno é necessário para a matricula")]
        Guid AlunoId,

        [NotEmptyGuid(ErrorMessage = "Id da Turma é necessário para a matricula")]
        Guid TurmaId
        );
}
