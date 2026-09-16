using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository
    {
        bool ExistePorCpf(Cpf cpf);
        bool ExistePorEmail(Email email);
        void Create(Aluno aluno);
        List<Aluno> GetAll();
        Aluno? GetById(Guid id);
        void Update(Aluno aluno);
        void Delete(Aluno aluno);
    }
}
