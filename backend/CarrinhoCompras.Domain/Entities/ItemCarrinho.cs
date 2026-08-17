namespace CarrinhoCompras.Domain.Entities;

public class ItemCarrinho
{
    private ItemCarrinho(){}

    public int Id { get; private set; }
    public int CarrinhoId { get; private set; }
    public int ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal PrecoItem => PrecoUnitario * Quantidade;

    public ItemCarrinho(int carrinhoId, Produto produto, int quantidade)
    {
        CarrinhoId = carrinhoId;
        ProdutoId = produto.Id;
        PrecoUnitario = produto.PrecoUnitario;
        Quantidade = quantidade;
    }

    public void SomarQuantidade(int quantidade)
    {
        Quantidade += quantidade;
    }

    public void DefinirQuantidade(int quantidade)
    {
        Quantidade = quantidade;
    }
}