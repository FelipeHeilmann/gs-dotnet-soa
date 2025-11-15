namespace UpSkillingPlatform.Domain.Entities;

public class Trilha
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Nivel { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public string? FocoPrincipal { get; set; }

    public ICollection<TrilhaCompetencia> TrilhaCompetencias { get; set; } = new List<TrilhaCompetencia>();
    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
