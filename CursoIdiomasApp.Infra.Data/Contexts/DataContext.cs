using CursoIdiomasApp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new TurmaMap());
            modelBuilder.ApplyConfiguration(new MatriculaMap());
        }
    }
}
