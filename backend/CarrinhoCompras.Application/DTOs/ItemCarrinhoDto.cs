namespace CarrinhoCompras.Application.DTOs;
public class ItemCarrinhoDto(int produtoId, string descricaoProduto, int quantidade, decimal precoUnitario)
{
    public int ProdutoId { get; set; } = produtoId;
    public string DescricaoProduto { get; set; } = descricaoProduto;
    public decimal PrecoUnitario { get; set; } = precoUnitario;
    public int Quantidade { get; set; } = quantidade;
}