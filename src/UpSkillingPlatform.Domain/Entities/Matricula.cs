namespace UpSkillingPlatform.Domain.Entities;

public class Matricula
{
    public long Id { get; set; }
    public long UsuarioId { get; set; }
    public long TrilhaId { get; set; }
    public DateTime DataInscricao { get; set; }
    public string Status { get; set; } = string.Empty;

    public Usuario Usuario { get; set; } = null!;
    public Trilha Trilha { get; set; } = null!;
}
