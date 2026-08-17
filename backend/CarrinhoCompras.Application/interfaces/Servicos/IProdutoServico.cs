using CarrinhoCompras.Application.DTOs;
 
namespace CarrinhoCompras.Application.Services;

public interface IProdutoServico
{
    Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
    Task<ProdutoDto> ObterByIdAsync(int id);
}