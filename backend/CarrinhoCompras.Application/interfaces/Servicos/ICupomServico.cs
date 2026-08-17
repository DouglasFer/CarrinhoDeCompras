using CarrinhoCompras.Application.DTOs;

namespace CarrinhoCompras.Application.Interfaces.Servicos;

public interface ICupomServico
{
    Task<IEnumerable<CupomDto>> ListarAsync();
    Task<CupomDto> ObterByCodigoAsync(string codigoCupom);
}
