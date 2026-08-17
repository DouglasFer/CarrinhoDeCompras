namespace CarrinhoCompras.Application.DTOs;

public class RequestsDtos
{
    public class AdicionarItemRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class AlterarQuantidadeRequest
    {
        public int Quantidade { get; set; }
    }

    public class CupomRequest
    {
        public string CodigoCupom { get; set; }
    }
}
