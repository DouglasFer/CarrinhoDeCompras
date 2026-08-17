using CarrinhoCompras.Application.DTOs;
using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Domain.Entities;
 
namespace CarrinhoCompras.Application.Services;
 
public class ProdutoServico(IProdutoRepository produtoRepository) : IProdutoServico
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
    {
        var produtos = await _produtoRepository.ObterTodosAsync();
 
        return produtos.Select(MapearParaDto);
    }
 
    public async Task<ProdutoDto> ObterByIdAsync(int id)
    {
        var produto = await _produtoRepository.ObterByIdAsync(id)
            ?? throw new KeyNotFoundException($"Produto com id {id} não foi encontrado.");
 
        return MapearParaDto(produto);
    }
 
    private static ProdutoDto MapearParaDto(Produto produto)
    {
        return new ProdutoDto(produto.Id, produto.DescricaoProduto, produto.PrecoUnitario, produto.QuantidadeEstoque);
    }
}