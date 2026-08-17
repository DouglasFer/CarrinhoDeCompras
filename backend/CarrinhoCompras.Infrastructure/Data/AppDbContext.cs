using CarrinhoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoCompras.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Cupom> Cupons => Set<Cupom>();
    public DbSet<Carrinho> Carrinho => Set<Carrinho>();
    public DbSet<ItemCarrinho> ItensCarrinho => Set<ItemCarrinho>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}