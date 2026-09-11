using CursoIdiomasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoIdiomasApp.Infra.Data.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Numero).IsRequired();
            builder.HasIndex(t => t.Numero).IsUnique();

            builder.Property(t => t.AnoLetivo).IsRequired();

            builder.HasMany(t => t.Matriculas).WithOne().HasForeignKey(t => t.TurmaId);
        }
    }
}
