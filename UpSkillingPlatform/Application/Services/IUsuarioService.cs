using UpSkillingPlatform.Application.DTOs;

namespace UpSkillingPlatform.Application.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
    Task<UsuarioResponseDto> GetByIdAsync(long id);
    Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto);
    Task<UsuarioResponseDto> UpdateAsync(long id, UsuarioUpdateDto dto);
    Task DeleteAsync(long id);
}
