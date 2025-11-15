using UpSkillingPlatform.Domain.Entities;

namespace UpSkillingPlatform.Domain.Interfaces;

public interface IMatriculaRepository : IRepository<Matricula>
{
    Task<Matricula?> GetByUsuarioAndTrilhaAsync(long usuarioId, long trilhaId);
    Task<IEnumerable<Matricula>> GetByUsuarioIdAsync(long usuarioId);
    Task<IEnumerable<Matricula>> GetByTrilhaIdAsync(long trilhaId);
    Task<bool> ExisteMatriculaAtivaAsync(long usuarioId, long trilhaId);
}
