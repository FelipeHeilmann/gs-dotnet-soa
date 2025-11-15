namespace UpSkillingPlatform.Domain.Entities;

public class TrilhaCompetencia
{
    public long TrilhaId { get; set; }
    public long CompetenciaId { get; set; }

    public Trilha Trilha { get; set; } = null!;
    public Competencia Competencia { get; set; } = null!;
}
