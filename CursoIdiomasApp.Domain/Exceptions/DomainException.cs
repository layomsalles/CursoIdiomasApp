using System.Runtime.Intrinsics.X86;

namespace CursoIdiomasApp.Domain.Exceptions
{
    public class DomainException : Exception
    {
        //recebendo uma mensagem e enviando-a para o construtor da classe Exception. Com:
        public DomainException(string message) : base(message) { }
        //estamos dizendo explicitamente: O programa encontrou uma violação de uma regra do domínio.
    }
}
