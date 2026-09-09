using CursoIdiomasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoIdiomasApp.Infra.Data.Mappings
{
    public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matricula");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.AlunoId).IsRequired();
            builder.Property(m => m.TurmaId).IsRequired();

            builder.Property(m => m.DataMatricula);
        }
    }
}
