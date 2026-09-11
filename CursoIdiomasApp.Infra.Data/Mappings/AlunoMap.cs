using CursoIdiomasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Infra.Data.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).IsRequired().HasMaxLength(30);

            builder.Property(a => a.Cpf).HasConversion(cpf => cpf.Valor, valor => new Cpf(valor)).IsRequired().HasMaxLength(11);
            builder.HasIndex(a => a.Cpf).IsUnique();

            builder.Property(a => a.Email).HasConversion(email => email.Valor, valor => new Email(valor)).IsRequired();
            builder.HasIndex(a => a.Email).IsUnique();

            builder.HasMany(a => a.Matriculas).WithOne().HasForeignKey(a => a.AlunoId);
        }
    }
}
