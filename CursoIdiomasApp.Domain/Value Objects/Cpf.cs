namespace CursoIdiomasApp.Domain.Value_Objects
{
    public class Cpf
    {
        public string Valor { get; private set; }

        private Cpf() { }

        public Cpf(string valor) { 
            if(!string.IsNullOrEmpty(Valor))
            {
                throw new ArgumentException("Cpf inválido");
            }

            Valor = valor;
        }
    }
}
