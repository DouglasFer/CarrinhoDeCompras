using CarrinhoCompras.Application.DTOs;
using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Application.Interfaces.Servicos;
using CarrinhoCompras.Domain.Entities;

namespace CarrinhoCompras.Application.Services;

public class CarrinhoServico(
    ICarrinhoRepository carrinhoRepository,
    IProdutoRepository produtoRepository,
    ICupomRepository cupomRepository) : ICarrinhoServico
{
    private readonly ICarrinhoRepository _carrinhoRepository = carrinhoRepository;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly ICupomRepository _cupomRepository = cupomRepository;

    public async Task<CarrinhoDto> CriarAsync()
    {
        var carrinho = new Carrinho();

        await _carrinhoRepository.AdicionarAsync(carrinho);

        return MapearParaDto(carrinho);
    }

    public async Task<CarrinhoDto> ObterByIdAsync(int id)
    {
        var carrinho = await ObterCarrinhoAsync(id);

        return MapearParaDto(carrinho);
    }

    public async Task<CarrinhoDto> AdicionarItemAsync(int carrinhoId, int produtoId, int quantidade)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        var produto = await _produtoRepository.ObterByIdAsync(produtoId)
            ?? throw new KeyNotFoundException($"Produto com id {produtoId} não foi encontrado.");

        carrinho.AdicionarItem(produto, quantidade);

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    public async Task<CarrinhoDto> AlterarQuantidadeAsync(int carrinhoId, int produtoId, int novaQuantidade)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        var produto = await _produtoRepository.ObterByIdAsync(produtoId)
            ?? throw new KeyNotFoundException($"Produto com id {produtoId} não foi encontrado.");

        carrinho.AlterarQuantidade(produto, novaQuantidade);

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    public async Task<CarrinhoDto> RemoverItemAsync(int carrinhoId, int produtoId)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        carrinho.RemoverItem(produtoId);

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    public async Task<CarrinhoDto> AplicarCupomAsync(int carrinhoId, string codigoCupom)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        var cupom = await _cupomRepository.ObterByCodigoAsync(codigoCupom)
            ?? throw new KeyNotFoundException($"Cupom '{codigoCupom}' é inválido ou não existe.");

        carrinho.AplicarCupom(cupom);

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    public async Task<CarrinhoDto> RemoverCupomAsync(int carrinhoId)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        carrinho.RemoverCupom();

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    public async Task<CarrinhoDto> FinalizarAsync(int carrinhoId)
    {
        var carrinho = await ObterCarrinhoAsync(carrinhoId);

        carrinho.Finalizar();

        await _carrinhoRepository.AtualizarAsync(carrinho);

        var carrinhoAtualizado = await ObterCarrinhoAsync(carrinhoId);

        return MapearParaDto(carrinhoAtualizado);
    }

    private async Task<Carrinho> ObterCarrinhoAsync(int carrinhoId)
    {
        return await _carrinhoRepository.ObterByIdAsync(carrinhoId)
            ?? throw new KeyNotFoundException($"Carrinho com id {carrinhoId} não foi encontrado.");
    }

    public async Task<List<CarrinhoDto>> ObterHistoricoAsync()
    {
        var carrinhos = await _carrinhoRepository.ObterHistoricoAsync();
        return [.. carrinhos.Select(c => MapearParaDto(c))];
    }

    private static CarrinhoDto MapearParaDto(Carrinho carrinho)
    {
        var itens = carrinho.Itens
            .Select(i => new ItemCarrinhoDto(i.ProdutoId, i.Produto.DescricaoProduto, i.Quantidade, i.PrecoUnitario))
            .ToList();

        return new CarrinhoDto(
            carrinho.Id,
            carrinho.Status.ToString(),
            itens,
            carrinho.CupomAplicado?.CodigoCupom,
            carrinho.Subtotal,
            carrinho.Desconto,
            carrinho.Total);
    }
}