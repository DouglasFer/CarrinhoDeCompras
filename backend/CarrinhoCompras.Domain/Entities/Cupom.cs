namespace CarrinhoCompras.Domain.Entities;
 
public class Cupom
{
    public int Id { get; private set; }
    public string CodigoCupom { get; private set; }
    public decimal PercentualDesconto { get; private set; }
    public DateTime DataExpiracao { get; private set; }
}