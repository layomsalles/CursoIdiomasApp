using CursoIdiomasApp.Domain.Entities;

namespace CursoIdiomasApp.Domain.Interfaces.Repositories
{
    public interface ITurmaRepository
    {
        void Create(Turma turma);
        List<Turma> GetAll();
        Turma? GetById(Guid id);
        void Update(Turma turma);
        void Delete(Turma turma);
    }
}
