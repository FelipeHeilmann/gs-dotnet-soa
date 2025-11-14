using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
