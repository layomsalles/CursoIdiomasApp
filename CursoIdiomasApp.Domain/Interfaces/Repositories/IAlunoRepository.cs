using CursoIdiomasApp.Domain.Entities;

namespace CursoIdiomasApp.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository
    {
        void Create(Aluno aluno);
        List<Aluno> GetAll();
        Aluno? GetById(Guid id);
        void Update(Aluno aluno);
        void Delete(Aluno aluno);
    }
}
