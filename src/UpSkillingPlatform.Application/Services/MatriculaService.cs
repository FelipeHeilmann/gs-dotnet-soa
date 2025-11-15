using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Application.Services;

public class MatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITrilhaRepository _trilhaRepository;

    public MatriculaService
    (
        IMatriculaRepository matriculaRepository,
        IUsuarioRepository usuarioRepository,
        ITrilhaRepository trilhaRepository)
    {
        _matriculaRepository = matriculaRepository;
        _usuarioRepository = usuarioRepository;
        _trilhaRepository = trilhaRepository;
    }

    public async Task<IEnumerable<MatriculaResponseDto>> GetAllAsync()
    {
        var matriculas = await _matriculaRepository.GetAllAsync();
        return matriculas.Select(MapToResponseDto);
    }

    public async Task<MatriculaResponseDto> GetByIdAsync(long id)
    {
        var matricula = await this._matriculaRepository.GetByIdAsync(id);
        if (matricula is null)
            throw new MatriculaNaoEncontradaException(id);
        return MapToResponseDto(matricula);
    }

    public async Task<IEnumerable<MatriculaResponseDto>> GetByUsuarioIdAsync(long usuarioId)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario is null)
            throw new UsuarioNaoEncontradoException(usuarioId);

        var matriculas = await this._matriculaRepository.GetByUsuarioIdAsync(usuarioId);
        return matriculas.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<MatriculaResponseDto>> GetByTrilhaIdAsync(long trilhaId)
    {
        var trilha = await _trilhaRepository.GetByIdAsync(trilhaId);
        if (trilha is null)
            throw new TrilhaNaoEncontradaException(trilhaId);

        var matriculas = await this._matriculaRepository.GetByTrilhaIdAsync(trilhaId);
        return matriculas.Select(MapToResponseDto);
    }

    public async Task<MatriculaResponseDto> CreateAsync(CreateMatriculaDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
        if (usuario is null)
            throw new UsuarioNaoEncontradoException(dto.UsuarioId);

        var trilha = await _trilhaRepository.GetByIdAsync(dto.TrilhaId);
        if (trilha is null)
        {
            throw new TrilhaNaoEncontradaException(dto.TrilhaId);
        }

        if (await this._matriculaRepository.ExisteMatriculaAtivaAsync(dto.UsuarioId, dto.TrilhaId))
        {
            throw new MatriculaJaExisteException(dto.UsuarioId, dto.TrilhaId);
        }

        var matricula = new Matricula
        {
            UsuarioId = dto.UsuarioId,
            TrilhaId = dto.TrilhaId,
            DataInscricao = DateTime.Now,
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };

        var created = await this._matriculaRepository.CreateAsync(matricula);
        return MapToResponseDto(created);
    }

    public async Task<MatriculaResponseDto> CancelarMatriculaAsync(long id)
    {
        var matricula = await this._matriculaRepository.GetByIdAsync(id);
        if (matricula is null)
            throw new MatriculaNaoEncontradaException(id);

        if (matricula.Status == "Cancelada")
            throw new MatriculaInvalidaException("Esta matrícula já está cancelada.");

        if (matricula.Status == "Concluída")
            throw new MatriculaInvalidaException("Não é possível cancelar uma matrícula concluída.");

        matricula.Status = "Cancelada";
        var updated = await this._matriculaRepository.UpdateAsync(matricula);
        return MapToResponseDto(updated);
    }

    public async Task<MatriculaResponseDto> ConcluirMatriculaAsync(long id)
    {
        var matricula = await this._matriculaRepository.GetByIdAsync(id);
        if (matricula is null)
            throw new MatriculaNaoEncontradaException(id);

        if (matricula.Status == "Cancelada")
            throw new MatriculaInvalidaException("Não é possível concluir uma matrícula cancelada.");

        if (matricula.Status == "Concluída")
            throw new MatriculaInvalidaException("Esta matrícula já está concluída.");

        matricula.Status = "Concluída";
        var updated = await this._matriculaRepository.UpdateAsync(matricula);
        return MapToResponseDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        var matricula = await this._matriculaRepository.GetByIdAsync(id);
        if (matricula is null)
            throw new MatriculaNaoEncontradaException(id);

        await this._matriculaRepository.DeleteAsync(id);
    }

    private static MatriculaResponseDto MapToResponseDto(Matricula matricula)
    {
        return new MatriculaResponseDto
        {
            Id = matricula.Id,
            UsuarioId = matricula.UsuarioId,
            NomeUsuario = matricula.Usuario?.Nome ?? string.Empty,
            EmailUsuario = matricula.Usuario?.Email ?? string.Empty,
            TrilhaId = matricula.TrilhaId,
            NomeTrilha = matricula.Trilha?.Nome ?? string.Empty,
            NivelTrilha = matricula.Trilha?.Nivel ?? string.Empty,
            CargaHoraria = matricula.Trilha?.CargaHoraria ?? 0,
            DataInscricao = matricula.DataInscricao,
            Status = matricula.Status
        };
    }
}
