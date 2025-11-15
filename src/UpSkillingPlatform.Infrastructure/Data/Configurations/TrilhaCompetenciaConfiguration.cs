using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Infrastructure.Data.Configurations;

public class TrilhaCompetenciaConfiguration : IEntityTypeConfiguration<TrilhaCompetencia>
{
    public void Configure(EntityTypeBuilder<TrilhaCompetencia> builder)
    {
        builder.ToTable("trilha_competencia");

        builder.HasKey(tc => new { tc.TrilhaId, tc.CompetenciaId });

        builder.Property(tc => tc.TrilhaId)
            .HasColumnName("trilha_id");

        builder.Property(tc => tc.CompetenciaId)
            .HasColumnName("competencia_id");

        builder.HasOne(tc => tc.Trilha)
            .WithMany(t => t.TrilhaCompetencias)
            .HasForeignKey(tc => tc.TrilhaId)
            .HasConstraintName("fk_trilha_competencia_trilha");

        builder.HasOne(tc => tc.Competencia)
            .WithMany(c => c.TrilhaCompetencias)
            .HasForeignKey(tc => tc.CompetenciaId)
            .HasConstraintName("fk_trilha_competencia_competencia");
    }
}
