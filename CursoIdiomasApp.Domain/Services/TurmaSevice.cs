using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;


namespace CursoIdiomasApp.Domain.Services
{
    internal class TurmaSevice(ITurmaRepository turmaRepository) : ITurmaService
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

            turmaRepository.Delete(turma);

            return new TurmaDeleteResponse("Turma deletada com sucesso");
        }

        public List<TurmaResponse> GetAllTurmas()
        {
            var turma = turmaRepository.GetAll();

            return turma.Select(t => new TurmaResponse(t.Id, t.Numero, t.AnoLetivo)).ToList();
        }

        public TurmaResponse UpdateTurma(TurmaRequest turmaRequest)
        {
            var turma = turmaRepository.GetById(turmaRequest.id);

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
