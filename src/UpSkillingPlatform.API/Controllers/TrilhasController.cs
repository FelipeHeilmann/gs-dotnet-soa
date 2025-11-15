using Microsoft.AspNetCore.Mvc;
using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Application.Services;

namespace UpSkillingPlatform.API.Controllers;

[ApiController]
[Route("api/trilhas")]
public class TrilhasController : ControllerBase
{
    private readonly TrilhaService _service;
    private readonly ILogger<TrilhasController> _logger;

    public TrilhasController(TrilhaService service, ILogger<TrilhasController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Lista todas as trilhas de aprendizagem
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrilhaResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TrilhaResponseDto>>> GetAll()
    {
        _logger.LogInformation("Buscando todas as trilhas");
        var trilhas = await _service.GetAllAsync();
        return Ok(trilhas);
    }

    /// <summary>
    /// Busca uma trilha específica por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TrilhaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrilhaResponseDto>> GetById(long id)
    {
        _logger.LogInformation("Buscando trilha com ID {Id}", id);
        var trilha = await _service.GetByIdAsync(id);
        return Ok(trilha);
    }

    /// <summary>
    /// Cria uma nova trilha de aprendizagem
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TrilhaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<TrilhaResponseDto>> Create([FromBody] TrilhaCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Criando nova trilha: {Nome}", dto.Nome);
        var trilha = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = trilha.Id }, trilha);
    }

    /// <summary>
    /// Atualiza uma trilha existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TrilhaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<TrilhaResponseDto>> Update(long id, [FromBody] TrilhaUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Atualizando trilha com ID {Id}", id);
        var trilha = await _service.UpdateAsync(id, dto);
        return Ok(trilha);
    }

    /// <summary>
    /// Remove uma trilha
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        _logger.LogInformation("Removendo trilha com ID {Id}", id);
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
