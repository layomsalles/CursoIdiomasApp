namespace CursoIdiomasApp.Domain.Value_Objects
{
    public class Cpf
    {
        public string Valor { get; private set; }

        private Cpf() { }

        public Cpf(string valor) { 
            if(!string.IsNullOrEmpty(valor))
            {
                throw new ArgumentException("Cpf inválido");
            }

            if(valor.Length != 11)
            {
                throw new ArgumentException("Cpf deve possuir 11 dígitos");
            }

            Valor = valor;
        }
    }
}
