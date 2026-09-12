namespace CursoIdiomasApp.Domain.Entities
{
    public class Matricula
    {
        public Guid Id { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid TurmaId { get; private set; }
        public DateTime DataMatricula { get; private set; }

        //Esse construtor existe principalmente para permitir que o Entity Framework reconstrua a entidade quando carregar uma matrícula do banco.
        protected Matricula() { }

        //Construtor da classe
        public Matricula(Guid alunoId, Guid turmaId) {
            Id = Guid.NewGuid();
            AlunoId = alunoId;
            TurmaId = turmaId;
            DataMatricula = DateTime.UtcNow;
        }
    }
}
