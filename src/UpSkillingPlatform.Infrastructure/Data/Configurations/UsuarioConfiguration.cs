using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.AreaAtuacao)
            .HasColumnName("area_atuacao")
            .HasMaxLength(100);

        builder.Property(u => u.NivelCarreira)
            .HasColumnName("nivel_carreira")
            .HasMaxLength(50);

        builder.Property(u => u.DataCadastro)
            .HasColumnName("data_cadastro")
            .IsRequired();

        builder.HasMany(u => u.Matriculas)
            .WithOne(m => m.Usuario)
            .HasForeignKey(m => m.UsuarioId);
    }
}
