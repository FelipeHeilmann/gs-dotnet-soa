using Microsoft.EntityFrameworkCore;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Interfaces;
using UpSkillingPlatform.Infrastructure.Data;

namespace UpSkillingPlatform.Infrastructure.Repositories;

public class TrilhaRepository : Repository<Trilha>, ITrilhaRepository
{
    public TrilhaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Trilha>> GetByNivelAsync(string nivel)
    {
        return await _dbSet.Where(t => t.Nivel == nivel).ToListAsync();
    }
}
