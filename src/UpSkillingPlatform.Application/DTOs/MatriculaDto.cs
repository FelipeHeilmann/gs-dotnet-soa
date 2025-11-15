namespace UpSkillingPlatform.Application.DTOs;

public class CreateMatriculaDto
{
    public long UsuarioId { get; set; }
    public long TrilhaId { get; set; }
}

public class MatriculaResponseDto
{
    public long Id { get; set; }
    public long UsuarioId { get; set; }
    public string NomeUsuario { get; set; } = string.Empty;
    public string EmailUsuario { get; set; } = string.Empty;
    public long TrilhaId { get; set; }
    public string NomeTrilha { get; set; } = string.Empty;
    public string NivelTrilha { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public DateTime DataInscricao { get; set; }
    public string Status { get; set; } = string.Empty;
}
