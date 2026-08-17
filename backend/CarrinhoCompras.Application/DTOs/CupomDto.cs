namespace CarrinhoCompras.Application.DTOs;

public class CupomDto
{
    public CupomDto(int id, string codigoCupom, decimal percentualDesconto, DateTime dataExpiracao)
    {
        Id = id;
        CodigoCupom = codigoCupom;
        PercentualDesconto = percentualDesconto;
        DataExpiracao = dataExpiracao;
    }
    public int Id { get; set; }
    public string CodigoCupom { get; set; }
    public decimal PercentualDesconto { get; set; }
    public DateTime DataExpiracao { get; set; }
}