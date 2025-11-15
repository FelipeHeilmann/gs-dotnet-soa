using Microsoft.AspNetCore.Mvc;
using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Application.Services;

namespace UpSkillingPlatform.API.Controllers;

[ApiController]
[Route("api/matriculas")]
public class MatriculasController : ControllerBase
{
    private readonly MatriculaService _service;
    private readonly ILogger<MatriculasController> _logger;

    public MatriculasController(MatriculaService service, ILogger<MatriculasController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Lista todas as matrículas cadastradas
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MatriculaResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> GetAll()
    {
        _logger.LogInformation("Buscando todas as matrículas");
        var matriculas = await _service.GetAllAsync();
        return Ok(matriculas);
    }

    /// <summary>
    /// Busca uma matrícula específica por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MatriculaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MatriculaResponseDto>> GetById(long id)
    {
        _logger.LogInformation("Buscando matrícula com ID {Id}", id);
        var matricula = await _service.GetByIdAsync(id);
        return Ok(matricula);
    }

    /// <summary>
    /// Lista todas as matrículas de um usuário específico
    /// </summary>
    [HttpGet("usuarios/{usuarioId}")]
    [ProducesResponseType(typeof(IEnumerable<MatriculaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> GetByUsuarioId(long usuarioId)
    {
        _logger.LogInformation("Buscando matrículas do usuário {UsuarioId}", usuarioId);
        var matriculas = await _service.GetByUsuarioIdAsync(usuarioId);
        return Ok(matriculas);
    }

    /// <summary>
    /// Lista todas as matrículas de uma trilha específica
    /// </summary>
    [HttpGet("trilhas/{trilhaId}")]
    [ProducesResponseType(typeof(IEnumerable<MatriculaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> GetByTrilhaId(long trilhaId)
    {
        _logger.LogInformation("Buscando matrículas da trilha {TrilhaId}", trilhaId);
        var matriculas = await _service.GetByTrilhaIdAsync(trilhaId);
        return Ok(matriculas);
    }

    /// <summary>
    /// Cria uma nova matrícula (inscrição do aluno na trilha)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MatriculaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MatriculaResponseDto>> Create([FromBody] CreateMatriculaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Criando nova matrícula - Usuário: {UsuarioId}, Trilha: {TrilhaId}", 
            dto.UsuarioId, dto.TrilhaId);
        
        var matricula = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = matricula.Id }, matricula);
    }

    /// <summary>
    /// Cancela uma matrícula existente
    /// </summary>
    [HttpPatch("{id}/cancelar")]
    [ProducesResponseType(typeof(MatriculaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<MatriculaResponseDto>> Cancelar(long id)
    {
        _logger.LogInformation("Cancelando matrícula {Id}", id);
        var matricula = await _service.CancelarMatriculaAsync(id);
        return Ok(matricula);
    }

    /// <summary>
    /// Marca uma matrícula como concluída
    /// </summary>
    [HttpPatch("{id}/concluir")]
    [ProducesResponseType(typeof(MatriculaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<MatriculaResponseDto>> Concluir(long id)
    {
        _logger.LogInformation("Concluindo matrícula {Id}", id);
        var matricula = await _service.ConcluirMatriculaAsync(id);
        return Ok(matricula);
    }

    /// <summary>
    /// Remove uma matrícula do sistema
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(long id)
    {
        _logger.LogInformation("Removendo matrícula {Id}", id);
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
