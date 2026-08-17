using CarrinhoCompras.Domain.Enums;

namespace CarrinhoCompras.Domain.Entities;

public class Carrinho()
{
    private readonly List<ItemCarrinho> _itens = new();

    public int Id { get; private set; }
    public StatusCarrinho Status { get; private set; } = StatusCarrinho.Aberto;
    public int? CupomAplicadoId { get; private set; }
    public Cupom? CupomAplicado { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; private set; } = DateTime.UtcNow;

    public IReadOnlyCollection<ItemCarrinho> Itens => _itens.AsReadOnly();

    public decimal Subtotal => _itens.Sum(item => item.PrecoItem);
    public decimal Desconto => CupomAplicado is null ? 0m : Subtotal * (CupomAplicado.PercentualDesconto / 100m);
    public decimal Total => Subtotal - Desconto;

   public void AdicionarItem(Produto produto, int quantidade)
    {
        GarantirCarrinhoAberto();

        if (quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");

        var itemExistente = _itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
        var quantidadeTotal = (itemExistente?.Quantidade ?? 0) + quantidade;

        if (quantidadeTotal > produto.QuantidadeEstoque)
        {
            throw new ArgumentException(
                $"Quantidade solicitada ({quantidadeTotal}) para \"{produto.DescricaoProduto}\" excede o estoque disponível ({produto.QuantidadeEstoque}).",
                nameof(quantidade));
        }

        if (itemExistente is null)
            _itens.Add(new ItemCarrinho(Id, produto, quantidade));
        else
            itemExistente.SomarQuantidade(quantidade);

        AttDataAtualizacao();
    }
    public void RemoverItem(int produtoId)
    {
        GarantirCarrinhoAberto();

        var item = ObterItem(produtoId);
        _itens.Remove(item);

        AttDataAtualizacao();
    }

   public void AlterarQuantidade(Produto produto, int novaQuantidade)
    {
        GarantirCarrinhoAberto();

        if (novaQuantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(novaQuantidade), "A quantidade deve ser maior que zero.");

        var item = ObterItem(produto.Id);

        if (novaQuantidade > produto.QuantidadeEstoque)
        {
            throw new ArgumentException(
                $"Quantidade solicitada ({novaQuantidade}) para \"{produto.DescricaoProduto}\" excede o estoque disponível ({produto.QuantidadeEstoque}).",
                nameof(novaQuantidade));
        }

        item.DefinirQuantidade(novaQuantidade);

        AttDataAtualizacao();
    }

    public void AplicarCupom(Cupom cupom)
    {
        GarantirCarrinhoAberto();

        CupomAplicadoId = cupom.Id;
        CupomAplicado = cupom;

        AttDataAtualizacao();
    }

    public void RemoverCupom()
    {
        GarantirCarrinhoAberto();

        CupomAplicadoId = null;
        CupomAplicado = null;

        AttDataAtualizacao();
    }

    public void Finalizar()
    {
        GarantirCarrinhoAberto();

        Status = StatusCarrinho.Finalizado;
        AttDataAtualizacao();
    }

    private ItemCarrinho ObterItem(int produtoId)
    {
        return _itens.FirstOrDefault(i => i.ProdutoId == produtoId)
            ?? throw new KeyNotFoundException($"O carrinho não possui o produto com id {produtoId}.");
    }

    private void GarantirCarrinhoAberto()
    {
        if (Status == StatusCarrinho.Finalizado)
            throw new InvalidOperationException($"O carrinho {Id} já foi finalizado e não pode mais ser alterado.");
    }

    private void AttDataAtualizacao()
    {
        AtualizadoEm = DateTime.UtcNow;
    }
}