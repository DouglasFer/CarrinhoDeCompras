using CarrinhoCompras.Domain.Entities;

namespace CarrinhoCompras.Application.Interfaces.Repositorios;

public interface ICupomRepository
{
    Task<List<Cupom>> ObterTodosAsync();
    Task<Cupom?> ObterByCodigoAsync(string codigoCupom);
}