using System.ComponentModel.DataAnnotations;


namespace CursoIdiomasApp.Domain.Value_Objects
{
    public class Email
    {
        public string Valor { get; private set; }

        private Email() { }

        public Email(string valor) { 
            if(string.IsNullOrEmpty(valor))
            {
                throw new ArgumentException("E-mail é obrigátorio");
            }

            var atributo = new EmailAddressAttribute();

            if (!atributo.IsValid(valor))
            {
                throw new ArgumentException("E-mail inválido");
            }

            Valor = valor;
        }
    }
}
