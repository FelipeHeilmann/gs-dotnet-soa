using Microsoft.EntityFrameworkCore;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Interfaces;
using UpSkillingPlatform.Infrastructure.Data;

namespace UpSkillingPlatform.Infrastructure.Repositories;

public class MatriculaRepository : Repository<Matricula>, IMatriculaRepository
{
    public MatriculaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Matricula?> GetByUsuarioAndTrilhaAsync(long usuarioId, long trilhaId)
    {
        return await _dbSet
            .Include(m => m.Usuario)
            .Include(m => m.Trilha)
            .FirstOrDefaultAsync(m => m.UsuarioId == usuarioId && m.TrilhaId == trilhaId);
    }

    public async Task<IEnumerable<Matricula>> GetByUsuarioIdAsync(long usuarioId)
    {
        return await _dbSet
            .Include(m => m.Trilha)
            .Include(m => m.Usuario)
            .Where(m => m.UsuarioId == usuarioId)
            .OrderByDescending(m => m.DataInscricao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Matricula>> GetByTrilhaIdAsync(long trilhaId)
    {
        return await _dbSet
            .Include(m => m.Usuario)
            .Include(m => m.Trilha)
            .Where(m => m.TrilhaId == trilhaId)
            .OrderByDescending(m => m.DataInscricao)
            .ToListAsync();
    }

    public async Task<bool> ExisteMatriculaAtivaAsync(long usuarioId, long trilhaId)
    {
        return await _dbSet.AnyAsync(m => 
            m.UsuarioId == usuarioId && 
            m.TrilhaId == trilhaId && 
            m.Status == "Ativa");
    }

    public override async Task<Matricula?> GetByIdAsync(long id)
    {
        return await _dbSet
            .Include(m => m.Usuario)
            .Include(m => m.Trilha)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public override async Task<IEnumerable<Matricula>> GetAllAsync()
    {
        return await _dbSet
            .Include(m => m.Usuario)
            .Include(m => m.Trilha)
            .OrderByDescending(m => m.DataInscricao)
            .ToListAsync();
    }
}
