using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Infrastructure.Data.Configurations;

public class CompetenciaConfiguration : IEntityTypeConfiguration<Competencia>
{
    public void Configure(EntityTypeBuilder<Competencia> builder)
    {
        builder.ToTable("competencias");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Categoria)
            .HasColumnName("categoria")
            .HasMaxLength(100);

        builder.Property(c => c.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("TEXT");

        builder.HasMany(c => c.TrilhaCompetencias)
            .WithOne(tc => tc.Competencia)
            .HasForeignKey(tc => tc.CompetenciaId);
    }
}
