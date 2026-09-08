using CursoIdiomasApp.Domain.Enums;

namespace CursoIdiomasApp.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Numero { get; set; } = 0;
        public TurmaEnum AnoLetivo { get; set; } = TurmaEnum.Basico;

        private readonly List<Matricula> _matriculas = [];

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        protected Turma() { }

        public Turma(int numero, TurmaEnum turma)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            AnoLetivo = turma;
        }
    }
}
