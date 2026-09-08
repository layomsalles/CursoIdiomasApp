namespace CursoIdiomasApp.Domain.Entities
{
    public class Matricula
    {
        public Guid Id { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid TurmaId { get; private set; }
        public DateTime DataMatricula { get; private set; }

        protected Matricula() { }

        public Matricula(Guid alunoId, Guid turmaId) {
            Id = Guid.NewGuid();
            AlunoId = alunoId;
            TurmaId = turmaId;
            DataMatricula = DateTime.UtcNow;
        }
    }
}
