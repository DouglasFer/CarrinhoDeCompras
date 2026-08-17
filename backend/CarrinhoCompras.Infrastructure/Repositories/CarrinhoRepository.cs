using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Domain.Entities;
using CarrinhoCompras.Domain.Enums;
using CarrinhoCompras.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoCompras.Infrastructure.Repositories;

public class CarrinhoRepository(AppDbContext context) : ICarrinhoRepository
{
    private readonly AppDbContext _context = context;

    public Task<Carrinho?> ObterByIdAsync(int id)
    {
        return _context.Carrinho
            .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
            .Include(c => c.CupomAplicado)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AdicionarAsync(Carrinho carrinho)
    {
        await _context.Carrinho.AddAsync(carrinho);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Carrinho carrinho)
    {
        _context.Carrinho.Update(carrinho);
        await _context.SaveChangesAsync();
    }

    public Task<List<Carrinho>> ObterHistoricoAsync()
    {
        return _context.Carrinho
            .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
            .Include(c => c.CupomAplicado)
            .Where(c => c.Status == StatusCarrinho.Finalizado)
            .OrderByDescending(c => c.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}