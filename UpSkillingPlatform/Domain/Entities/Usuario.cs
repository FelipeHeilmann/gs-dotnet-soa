namespace UpSkillingPlatform.Domain.Entities;

public class Usuario
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AreaAtuacao { get; set; }
    public string? NivelCarreira { get; set; }
    public DateTime DataCadastro { get; set; }

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
