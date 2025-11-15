using System.ComponentModel.DataAnnotations;

namespace UpSkillingPlatform.Application.DTOs;

public class TrilhaCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O nível é obrigatório")]
    [RegularExpression("^(INICIANTE|INTERMEDIARIO|AVANCADO)$", 
        ErrorMessage = "O nível deve ser INICIANTE, INTERMEDIARIO ou AVANCADO")]
    public string Nivel { get; set; } = string.Empty;

    [Required(ErrorMessage = "A carga horária é obrigatória")]
    [Range(1, 10000, ErrorMessage = "A carga horária deve ser maior que zero")]
    public int CargaHoraria { get; set; }

    [MaxLength(100, ErrorMessage = "O foco principal deve ter no máximo 100 caracteres")]
    public string? FocoPrincipal { get; set; }
}

public class TrilhaUpdateDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O nível é obrigatório")]
    [RegularExpression("^(INICIANTE|INTERMEDIARIO|AVANCADO)$", 
        ErrorMessage = "O nível deve ser INICIANTE, INTERMEDIARIO ou AVANCADO")]
    public string Nivel { get; set; } = string.Empty;

    [Required(ErrorMessage = "A carga horária é obrigatória")]
    [Range(1, 10000, ErrorMessage = "A carga horária deve ser maior que zero")]
    public int CargaHoraria { get; set; }

    [MaxLength(100, ErrorMessage = "O foco principal deve ter no máximo 100 caracteres")]
    public string? FocoPrincipal { get; set; }
}

public class TrilhaResponseDto
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Nivel { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public string? FocoPrincipal { get; set; }
}
