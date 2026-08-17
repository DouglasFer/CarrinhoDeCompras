using CarrinhoCompras.Domain.Entities;
using CarrinhoCompras.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CarrinhoCompras.Application.Interfaces.Repositorios;

namespace CarrinhoCompras.Infrastructure.Repositories;

public class CupomRepository(AppDbContext context) : ICupomRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Cupom>> ObterTodosAsync()
    {
        return await _context.Cupons.AsNoTracking().ToListAsync() ?? [];
    }

    public Task<Cupom?> ObterByCodigoAsync(string codigoCupom)
    {
        return _context.Cupons
            .FirstOrDefaultAsync(c => c.CodigoCupom == codigoCupom);
    }
}