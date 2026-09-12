using CursoIdiomasApp.Domain.Enums;
using CursoIdiomasApp.Domain.Exceptions;

namespace CursoIdiomasApp.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Numero { get; set; } = 0;
        public TurmaEnum AnoLetivo { get; set; } = TurmaEnum.Basico;

        private readonly List<Matricula> _matriculas = []; //conceito é chamado de encapsulamento das invariantes do domínio.

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas; //conceito é chamado de encapsulamento das invariantes do domínio.

        protected Turma() { }

        public Turma(int numero, TurmaEnum turma)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            AnoLetivo = turma;
        }

        public void MatricularAluno(Aluno aluno)
        {
            if (_matriculas.Count > 5)
            {
                throw new DomainException("A turma não pode possuir mais de 5 alunos");
            }

            if(_matriculas.Any(m => m.AlunoId == aluno.Id))
            {
                throw new DomainException("O aluno já está matriculado nessa turma");
            }

            var matricula = new Matricula(aluno.Id, Id);

            _matriculas.Add(matricula);
        }

        public void AtualizarTurma(int numero, TurmaEnum anoLetivo)
        {
            Numero = numero;
            AnoLetivo = anoLetivo;
        }
    }
}
