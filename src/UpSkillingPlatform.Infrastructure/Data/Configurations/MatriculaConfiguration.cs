using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Infrastructure.Data.Configurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("matriculas");

        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(m => m.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(m => m.TrilhaId)
            .HasColumnName("trilha_id")
            .IsRequired();

        builder.Property(m => m.DataInscricao)
            .HasColumnName("data_inscricao")
            .IsRequired();

        builder.Property(m => m.Status)
            .HasColumnName("status")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(m => m.Usuario)
            .WithMany(u => u.Matriculas)
            .HasForeignKey(m => m.UsuarioId)
            .HasConstraintName("fk_matricula_usuario");

        builder.HasOne(m => m.Trilha)
            .WithMany(t => t.Matriculas)
            .HasForeignKey(m => m.TrilhaId)
            .HasConstraintName("fk_matricula_trilha");
    }
}
