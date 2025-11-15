using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Domain.Interfaces;

public interface ITrilhaRepository : IRepository<Trilha>
{
    Task<IEnumerable<Trilha>> GetByNivelAsync(string nivel);
}
