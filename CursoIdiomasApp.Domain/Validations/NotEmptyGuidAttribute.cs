using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasApp.Domain.Validations
{
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is Guid guid && guid != Guid.Empty;
        }
    }
}
