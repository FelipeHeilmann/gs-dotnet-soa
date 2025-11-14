namespace UpSkillingPlatform.Domain.Entities;

public class Competencia
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public string? Descricao { get; set; }

    public ICollection<TrilhaCompetencia> TrilhaCompetencias { get; set; } = new List<TrilhaCompetencia>();
}
