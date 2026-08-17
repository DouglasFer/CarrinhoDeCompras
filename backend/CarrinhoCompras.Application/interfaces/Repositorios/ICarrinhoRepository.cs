using CarrinhoCompras.Domain.Entities;

namespace CarrinhoCompras.Application.Interfaces.Repositorios;

public interface ICarrinhoRepository
{
    Task<Carrinho?> ObterByIdAsync(int id);
    Task AdicionarAsync(Carrinho carrinho);
    Task AtualizarAsync(Carrinho carrinho);
    Task<List<Carrinho>> ObterHistoricoAsync();
}