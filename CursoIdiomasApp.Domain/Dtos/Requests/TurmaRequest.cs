using CursoIdiomasApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record TurmaRequest(
        [Required]
        Guid id,

        [Required(ErrorMessage = "A Número da Turma é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "O Número da Turma deve ser uma valor positivo")]
        int Numero,

        [Required(ErrorMessage = "O Ano Letivo é obrigatório")]
        [EnumDataType(typeof(TurmaRequest), ErrorMessage = "O Ano Letivo deve ser um tipo válido")]
        TurmaEnum AnoLetivo
        );
}
