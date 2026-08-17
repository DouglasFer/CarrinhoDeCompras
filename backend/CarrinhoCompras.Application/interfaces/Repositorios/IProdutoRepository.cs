using CarrinhoCompras.Domain.Entities;

namespace CarrinhoCompras.Application.Interfaces.Repositorios;

public interface IProdutoRepository
{
    Task<List<Produto>> ObterTodosAsync();
    Task<Produto?> ObterByIdAsync(int id);
}