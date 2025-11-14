using UpSkillingPlatform.Application.DTOs;

namespace UpSkillingPlatform.Application.Services;

public interface ITrilhaService
{
    Task<IEnumerable<TrilhaResponseDto>> GetAllAsync();
    Task<TrilhaResponseDto> GetByIdAsync(long id);
    Task<TrilhaResponseDto> CreateAsync(TrilhaCreateDto dto);
    Task<TrilhaResponseDto> UpdateAsync(long id, TrilhaUpdateDto dto);
    Task DeleteAsync(long id);
}
