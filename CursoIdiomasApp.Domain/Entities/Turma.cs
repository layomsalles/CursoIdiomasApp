using CursoIdiomasApp.Domain.Enums;
using CursoIdiomasApp.Domain.Exceptions;

namespace CursoIdiomasApp.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; private set; }
        public int Numero { get; private set; } = 0;
        public TurmaEnum AnoLetivo { get; private set; }

        private readonly List<Matricula> _matriculas = []; //conceito chamado de encapsulamento das invariantes do domínio.

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas; //conceito chamado de encapsulamento das invariantes do domínio.

        protected Turma() { }

        public Turma(int numero, TurmaEnum turma)
        {
            if (numero <= 0)
            {
                throw new DomainException(
                    "Número da turma deve ser maior que zero"
                );
            }

            Id = Guid.NewGuid();
            Numero = numero;
            AnoLetivo = turma;
        }

        //Método para validar as regras:
        public Matricula MatricularAluno(Aluno aluno)
        {
            //regra: Turma não pode ter mais de 5 alunos
            if (_matriculas.Count >= 5)
            {
                throw new DomainException("A turma não pode possuir mais de 5 alunos");
            }

            //regra: Aluno não pode se matricular duas vezes na mesma turma
            if (_matriculas.Any(m => m.AlunoId == aluno.Id))
            {
                throw new DomainException("O aluno já está matriculado nessa turma");
            }

            //Cria matricula
            var matricula = new Matricula(aluno.Id, Id);

            _matriculas.Add(matricula);

            return matricula;
        }

        public void CancelarMatricula(Guid matriculaId)
        {
            var matricula = _matriculas
                .FirstOrDefault(m => m.Id == matriculaId);

            if (matricula == null)
            {
                throw new DomainException(
                    "Matrícula não encontrada"
                );
            }

            _matriculas.Remove(matricula);
        }

        public void AtualizarTurma(int numero, TurmaEnum anoLetivo)
        {
            if (numero <= 0)
            {
                throw new DomainException(
                    "Número da turma deve ser maior que zero"
                );
            }

            Numero = numero;
            AnoLetivo = anoLetivo;
        }
    }
}
