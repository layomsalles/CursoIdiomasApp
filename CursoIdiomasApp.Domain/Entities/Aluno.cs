using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }

        private readonly List<Matricula> _matriculas = [];

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        protected Aluno() { }

        public Aluno(string nome, Cpf cpf, Email email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome é obrigatório");
            }

            Id = Guid.NewGuid();
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public void AtualizarAluno(Email email)
        {
            Email = email;
        }
    }
}
