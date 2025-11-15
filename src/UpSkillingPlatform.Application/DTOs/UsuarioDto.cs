using System.ComponentModel.DataAnnotations;

namespace UpSkillingPlatform.Application.DTOs;

public class UsuarioCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email deve ter um formato válido")]
    [MaxLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "A área de atuação deve ter no máximo 100 caracteres")]
    public string? AreaAtuacao { get; set; }

    [MaxLength(50, ErrorMessage = "O nível de carreira deve ter no máximo 50 caracteres")]
    public string? NivelCarreira { get; set; }
}

public class UsuarioUpdateDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email deve ter um formato válido")]
    [MaxLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "A área de atuação deve ter no máximo 100 caracteres")]
    public string? AreaAtuacao { get; set; }

    [MaxLength(50, ErrorMessage = "O nível de carreira deve ter no máximo 50 caracteres")]
    public string? NivelCarreira { get; set; }
}

public class UsuarioResponseDto
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AreaAtuacao { get; set; }
    public string? NivelCarreira { get; set; }
    public DateTime DataCadastro { get; set; }
}
