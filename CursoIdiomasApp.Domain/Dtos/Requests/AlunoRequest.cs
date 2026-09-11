using CursoIdiomasApp.Domain.Value_Objects;
using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record AlunoRequest(
        [Required(ErrorMessage = "O Nome é obrigatório")]
        string nome,

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
        [MinLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
        Cpf cpf,

        [Required(ErrorMessage = "O Email é obrigatório")]
        Email email
        );
}
