using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Infra.Data.Contexts;

namespace CursoIdiomasApp.Infra.Data.Repositories
{
    public class AlunoRepository(DataContext dataContext) : IAlunoRepository
    {
        public void Create(Aluno aluno)
        {
            dataContext.Set<Aluno>().Add(aluno);
            dataContext.SaveChanges();
        }

        public void Delete(Aluno aluno)
        {
            dataContext.Set<Aluno>().Remove(aluno);
            dataContext.SaveChanges();
        }

        public List<Aluno> GetAll()
        {
            return dataContext.Set<Aluno>().OrderBy(a => a.Nome).ToList();
        }

        public Aluno? GetById(Guid id)
        {
            return dataContext.Set<Aluno>().FirstOrDefault(aluno => aluno.Id == id);
        }

        public void Update(Aluno aluno)
        {
            dataContext.Set<Aluno>().Update(aluno);
            dataContext.SaveChanges();
        }
    }
}
