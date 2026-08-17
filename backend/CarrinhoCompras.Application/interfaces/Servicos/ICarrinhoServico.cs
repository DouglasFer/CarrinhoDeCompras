using CarrinhoCompras.Application.DTOs;

namespace CarrinhoCompras.Application.Interfaces.Servicos;

public interface ICarrinhoServico
{
    Task<CarrinhoDto> CriarAsync();
    Task<CarrinhoDto> ObterByIdAsync(int id);
    Task<CarrinhoDto> AdicionarItemAsync(int carrinhoId, int produtoId, int quantidade);
    Task<CarrinhoDto> AlterarQuantidadeAsync(int carrinhoId, int produtoId, int quantidade);
    Task<CarrinhoDto> RemoverItemAsync(int carrinhoId, int produtoId);
    Task<CarrinhoDto> AplicarCupomAsync(int carrinhoId, string codigoCupom);
    Task<CarrinhoDto> RemoverCupomAsync(int carrinhoId);
    Task<CarrinhoDto> FinalizarAsync(int carrinhoId);
    Task<List<CarrinhoDto>> ObterHistoricoAsync();
}