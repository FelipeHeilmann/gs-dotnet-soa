using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Application.Services;

public class TrilhaService : ITrilhaService
{
    private readonly ITrilhaRepository _repository;

    public TrilhaService(ITrilhaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrilhaResponseDto>> GetAllAsync()
    {
        var trilhas = await _repository.GetAllAsync();
        return trilhas.Select(MapToResponseDto);
    }

    public async Task<TrilhaResponseDto> GetByIdAsync(long id)
    {
        var trilha = await _repository.GetByIdAsync(id);
        if (trilha == null)
        {
            throw new TrilhaNaoEncontradaException(id);
        }
        return MapToResponseDto(trilha);
    }

    public async Task<TrilhaResponseDto> CreateAsync(TrilhaCreateDto dto)
    {
        ValidateNivel(dto.Nivel);

        var trilha = new Trilha
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Nivel = dto.Nivel,
            CargaHoraria = dto.CargaHoraria,
            FocoPrincipal = dto.FocoPrincipal
        };

        var created = await _repository.CreateAsync(trilha);
        return MapToResponseDto(created);
    }

    public async Task<TrilhaResponseDto> UpdateAsync(long id, TrilhaUpdateDto dto)
    {
        var trilha = await _repository.GetByIdAsync(id);
        if (trilha == null)
        {
            throw new TrilhaNaoEncontradaException(id);
        }

        ValidateNivel(dto.Nivel);

        trilha.Nome = dto.Nome;
        trilha.Descricao = dto.Descricao;
        trilha.Nivel = dto.Nivel;
        trilha.CargaHoraria = dto.CargaHoraria;
        trilha.FocoPrincipal = dto.FocoPrincipal;

        var updated = await _repository.UpdateAsync(trilha);
        return MapToResponseDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        if (!await _repository.ExistsAsync(id))
        {
            throw new TrilhaNaoEncontradaException(id);
        }
        await _repository.DeleteAsync(id);
    }

    private static void ValidateNivel(string nivel)
    {
        var niveisValidos = new[] { "INICIANTE", "INTERMEDIARIO", "AVANCADO" };
        if (!niveisValidos.Contains(nivel))
        {
            throw new ValidationException($"Nível inválido. Valores aceitos: {string.Join(", ", niveisValidos)}");
        }
    }

    private static TrilhaResponseDto MapToResponseDto(Trilha trilha)
    {
        return new TrilhaResponseDto
        {
            Id = trilha.Id,
            Nome = trilha.Nome,
            Descricao = trilha.Descricao,
            Nivel = trilha.Nivel,
            CargaHoraria = trilha.CargaHoraria,
            FocoPrincipal = trilha.FocoPrincipal
        };
    }
}
