using Microsoft.EntityFrameworkCore;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Infrastructure.Data.Configurations;

namespace UpSkillingPlatform.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
    public DbSet<Competencia> Competencias { get; set; }
    public DbSet<TrilhaCompetencia> TrilhaCompetencias { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new TrilhaConfiguration());
        modelBuilder.ApplyConfiguration(new CompetenciaConfiguration());
        modelBuilder.ApplyConfiguration(new TrilhaCompetenciaConfiguration());
        modelBuilder.ApplyConfiguration(new MatriculaConfiguration());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competencia>().HasData(
            new Competencia { Id = 1, Nome = "Inteligência Artificial", Categoria = "Tecnologia", Descricao = "Machine Learning, Deep Learning, NLP" },
            new Competencia { Id = 2, Nome = "Análise de Dados", Categoria = "Tecnologia", Descricao = "Data Science, Business Intelligence, Big Data" },
            new Competencia { Id = 3, Nome = "Cloud Computing", Categoria = "Tecnologia", Descricao = "AWS, Azure, Google Cloud" },
            new Competencia { Id = 4, Nome = "Desenvolvimento Web", Categoria = "Tecnologia", Descricao = "Frontend e Backend moderno" },
            new Competencia { Id = 5, Nome = "Comunicação Efetiva", Categoria = "Humana", Descricao = "Comunicação clara e assertiva" },
            new Competencia { Id = 6, Nome = "Trabalho em Equipe", Categoria = "Humana", Descricao = "Colaboração e cooperação" },
            new Competencia { Id = 7, Nome = "Pensamento Crítico", Categoria = "Humana", Descricao = "Análise e resolução de problemas" },
            new Competencia { Id = 8, Nome = "Gestão de Projetos", Categoria = "Gestão", Descricao = "Metodologias ágeis e tradicionais" }
        );

        modelBuilder.Entity<Trilha>().HasData(
            new Trilha 
            { 
                Id = 1, 
                Nome = "Fundamentos de IA e Machine Learning", 
                Descricao = "Aprenda os conceitos fundamentais de IA e como aplicar algoritmos de ML", 
                Nivel = "INICIANTE", 
                CargaHoraria = 40, 
                FocoPrincipal = "IA" 
            },
            new Trilha 
            { 
                Id = 2, 
                Nome = "Cientista de Dados Profissional", 
                Descricao = "Torne-se um cientista de dados completo com Python e ferramentas modernas", 
                Nivel = "INTERMEDIARIO", 
                CargaHoraria = 120, 
                FocoPrincipal = "Dados" 
            },
            new Trilha 
            { 
                Id = 3, 
                Nome = "Arquitetura Cloud Avançada", 
                Descricao = "Domine arquiteturas cloud escaláveis e resilientes", 
                Nivel = "AVANCADO", 
                CargaHoraria = 80, 
                FocoPrincipal = "Cloud" 
            },
            new Trilha 
            { 
                Id = 4, 
                Nome = "Soft Skills para o Futuro", 
                Descricao = "Desenvolva habilidades humanas essenciais para liderança e colaboração", 
                Nivel = "INICIANTE", 
                CargaHoraria = 30, 
                FocoPrincipal = "Soft Skills" 
            }
        );

        modelBuilder.Entity<TrilhaCompetencia>().HasData(
            new TrilhaCompetencia { TrilhaId = 1, CompetenciaId = 1 },
            new TrilhaCompetencia { TrilhaId = 1, CompetenciaId = 7 },
            new TrilhaCompetencia { TrilhaId = 2, CompetenciaId = 2 },
            new TrilhaCompetencia { TrilhaId = 2, CompetenciaId = 1 },
            new TrilhaCompetencia { TrilhaId = 3, CompetenciaId = 3 },
            new TrilhaCompetencia { TrilhaId = 3, CompetenciaId = 8 },
            new TrilhaCompetencia { TrilhaId = 4, CompetenciaId = 5 },
            new TrilhaCompetencia { TrilhaId = 4, CompetenciaId = 6 },
            new TrilhaCompetencia { TrilhaId = 4, CompetenciaId = 7 }
        );
    }
}
