using CursoIdiomasApp.Domain.Value_Objects;
using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasApp.Domain.Dtos.Requests
{
    public record AlunoUpdateRequest(
        [Required]
        Guid id,
        [Required]
        Email email
        );
}
