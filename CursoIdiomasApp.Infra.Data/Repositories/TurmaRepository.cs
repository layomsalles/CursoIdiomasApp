using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasApp.Infra.Data.Repositories
{
    public class TurmaRepository(DataContext dataContext) : ITurmaRepository
    {
        public void Create(Turma turma)
        {
            dataContext.Set<Turma>().Add(turma);
            dataContext.SaveChanges();
        }

        public void Delete(Turma turma)
        {
            dataContext.Set<Turma>().Remove(turma);
            dataContext.SaveChanges();
        }

        public List<Turma> GetAll()
        {
            return dataContext.Set<Turma>().OrderBy(t => t.AnoLetivo).ToList();
        }

        public Turma? GetById(Guid id)
        {
            return dataContext.Set<Turma>().FirstOrDefault(turma => turma.Id == id);
        }

        public Turma? GetByIdComMatriculas(Guid id)
        {
            return dataContext.Set<Turma>().Include(t => t.Matriculas).FirstOrDefault(t => t.Id == id);
        }

        public void Update(Turma turma)
        {
            dataContext.Set<Turma>().Update(turma);
            dataContext.SaveChanges();
        }
    }
}
