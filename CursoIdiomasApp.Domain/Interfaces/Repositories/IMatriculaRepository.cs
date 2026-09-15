using CursoIdiomasApp.Domain.Entities;

namespace CursoIdiomasApp.Domain.Interfaces.Repositories
{
    public interface IMatriculaRepository
    {
        void Create(Matricula matricula);
        void Delete(Matricula matricula);
        Matricula? GetById(Guid id);

        List<Matricula> GetByAluno(Guid alunoId);

        List<Matricula> GetByTurma(Guid turmaId);
    }
}
