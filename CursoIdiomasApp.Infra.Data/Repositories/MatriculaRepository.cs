using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Infra.Data.Contexts;

namespace CursoIdiomasApp.Infra.Data.Repositories
{
    public class MatriculaRepository(DataContext dataContext) : IMatriculaRepository
    {
        public int CountByTurma(Guid turmaId)
        {
            return dataContext.Set<Matricula>().Count(m => m.TurmaId == turmaId);
        }

        public void Create(Matricula matricula)
        {
            dataContext.Set<Matricula>().Add(matricula);
            dataContext.SaveChanges();
        }

        public void Delete(Matricula matricula)
        {
            dataContext.Set<Matricula>().Remove(matricula);
            dataContext.SaveChanges();
        }

        public bool Exists(Guid alunoId, Guid turmaId)
        {
            return dataContext.Set<Matricula>().Any(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }

        public List<Matricula> GetByAluno(Guid alunoId)
        {
            return dataContext.Set<Matricula>().Where(m => m.AlunoId == alunoId).ToList();
        }

        public Matricula? GetById(Guid id)
        {
            return dataContext.Set<Matricula>().FirstOrDefault(m => m.Id == id);
        }

        public List<Matricula> GetByTurma(Guid turmaId)
        {
            return dataContext.Set<Matricula>().Where(m => m.TurmaId == turmaId).ToList();
        }
    }
    
}
