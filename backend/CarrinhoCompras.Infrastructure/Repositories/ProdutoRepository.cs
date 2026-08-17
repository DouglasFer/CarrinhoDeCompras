using CarrinhoCompras.Domain.Entities;
using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoCompras.Infrastructure.Repositories;

public class ProdutoRepository(AppDbContext context) : IProdutoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Produto>> ObterTodosAsync()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync() ?? [];
    }

    public Task<Produto?> ObterByIdAsync(int id)
    {
        return _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
}