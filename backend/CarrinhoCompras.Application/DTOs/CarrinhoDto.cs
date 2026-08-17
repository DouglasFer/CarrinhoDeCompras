namespace CarrinhoCompras.Application.DTOs;

public class CarrinhoDto(int id, string status, List<ItemCarrinhoDto> itens, string? codigoCupom, decimal subtotal, decimal valorDesconto, decimal total)
{
    public int Id { get; set; } = id;
    public string Status { get; set; } = status;
    public List<ItemCarrinhoDto> Itens { get; set; } = itens;
    public string CodigoCupom { get; set; } = codigoCupom;
    public decimal Subtotal { get; set; } = subtotal;
    public decimal ValorDesconto { get; set; } = valorDesconto;
    public decimal Total { get; set; } = total;
}

