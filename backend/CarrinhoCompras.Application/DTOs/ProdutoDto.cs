namespace CarrinhoCompras.Application.DTOs;

public class ProdutoDto(int id, string descricaoProduto, decimal precoUnitario, int quantidadeEstoque)
{
    public int Id { get; set; } = id;
    public string DescricaoProduto { get; set; } = descricaoProduto;
    public decimal PrecoUnitario { get; set; } = precoUnitario;
    public int QuantidadeEstoque { get; set; } = quantidadeEstoque;
}