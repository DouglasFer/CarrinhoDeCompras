namespace CarrinhoCompras.Domain.Entities;
 
public class Produto
{
    public int Id { get; private set; }
    public string DescricaoProduto { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public int QuantidadeEstoque { get; private set; }
}