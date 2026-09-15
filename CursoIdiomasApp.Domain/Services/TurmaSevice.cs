using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Exceptions;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;


namespace CursoIdiomasApp.Domain.Services
{
    public class TurmaSevice(ITurmaRepository turmaRepository, IMatriculaRepository matriculaRepository) : ITurmaService
    {
        public TurmaResponse CreateTurma(TurmaRequest turmaRequest)
        {
            var turma = new Turma
            (
                turmaRequest.Numero,
                turmaRequest.AnoLetivo
            );

            turmaRepository.Create(turma);

            return new TurmaResponse(turma.Id, turma.Numero, turma.AnoLetivo);
        }

        public TurmaDeleteResponse DeleteTurma(Guid id)
        {
            var turma = turmaRepository.GetById(id);

            if(turma == null)
            {
                throw new ApplicationException("Turma não encontrada");
            }

            //Verificando se aluno está matriculado em uma turma
            var matriculas = matriculaRepository.GetByTurma(id);

            if (matriculas.Any())
            {
                throw new DomainException(
                    "A turma não pode ser excluída porque possui alunos matriculados"
                );
            }

            turmaRepository.Delete(turma);

            return new TurmaDeleteResponse("Turma deletada com sucesso");
        }

        public List<TurmaResponse> GetAllTurmas()
        {
            var turma = turmaRepository.GetAll();

            return turma.Select(t => new TurmaResponse(t.Id, t.Numero, t.AnoLetivo)).ToList();
        }

        public TurmaResponse UpdateTurma(Guid id, TurmaRequest turmaRequest)
        {
            var turma = turmaRepository.GetById(id);

            if(turma == null)
            {
                throw new ApplicationException("Turma não encontrada");
            }

            turma.AtualizarTurma(turmaRequest.Numero, turmaRequest.AnoLetivo);

            turmaRepository.Update(turma);

            return new TurmaResponse(turma.Id, turma.Numero, turma.AnoLetivo);
        }
    }
}
