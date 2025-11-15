using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Infrastructure.Data.Configurations;

public class TrilhaConfiguration : IEntityTypeConfiguration<Trilha>
{
    public void Configure(EntityTypeBuilder<Trilha> builder)
    {
        builder.ToTable("trilhas");

        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("TEXT");

        builder.Property(t => t.Nivel)
            .HasColumnName("nivel")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.CargaHoraria)
            .HasColumnName("carga_horaria")
            .IsRequired();

        builder.Property(t => t.FocoPrincipal)
            .HasColumnName("foco_principal")
            .HasMaxLength(100);

        builder.HasMany(t => t.TrilhaCompetencias)
            .WithOne(tc => tc.Trilha)
            .HasForeignKey(tc => tc.TrilhaId);

        builder.HasMany(t => t.Matriculas)
            .WithOne(m => m.Trilha)
            .HasForeignKey(m => m.TrilhaId);
    }
}
