using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(MapToResponseDto);
    }

    public async Task<UsuarioResponseDto> GetByIdAsync(long id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is null)
        {
            throw new UsuarioNaoEncontradoException(id);
        }
        return MapToResponseDto(usuario);
    }

    public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto)
    {
        if (await _repository.EmailExistsAsync(dto.Email))
        {
            throw new EmailJaCadastradoException(dto.Email);
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            AreaAtuacao = dto.AreaAtuacao,
            NivelCarreira = dto.NivelCarreira,
            DataCadastro = DateTime.Now
        };

        var created = await _repository.CreateAsync(usuario);
        return MapToResponseDto(created);
    }

    public async Task<UsuarioResponseDto> UpdateAsync(long id, UsuarioUpdateDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is null)
        {
            throw new UsuarioNaoEncontradoException(id);
        }

        var existingUser = await _repository.GetByEmailAsync(dto.Email);
        if (existingUser != null && existingUser.Id != id)
        {
            throw new EmailJaCadastradoException(dto.Email);
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.AreaAtuacao = dto.AreaAtuacao;
        usuario.NivelCarreira = dto.NivelCarreira;

        var updated = await _repository.UpdateAsync(usuario);
        return MapToResponseDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        if (!await _repository.ExistsAsync(id))
        {
            throw new UsuarioNaoEncontradoException(id);
        }
        await _repository.DeleteAsync(id);
    }

    private static UsuarioResponseDto MapToResponseDto(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            AreaAtuacao = usuario.AreaAtuacao,
            NivelCarreira = usuario.NivelCarreira,
            DataCadastro = usuario.DataCadastro
        };
    }
}
