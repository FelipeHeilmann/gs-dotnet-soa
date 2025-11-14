using Microsoft.EntityFrameworkCore;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Interfaces;
using UpSkillingPlatform.Infrastructure.Data;

namespace UpSkillingPlatform.Infrastructure.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }
}
