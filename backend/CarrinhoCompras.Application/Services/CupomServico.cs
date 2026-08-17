using CarrinhoCompras.Application.DTOs;
using CarrinhoCompras.Application.Interfaces.Servicos;
using CarrinhoCompras.Application.Interfaces.Repositorios;
using CarrinhoCompras.Domain.Entities;
 
namespace CarrinhoCompras.Application.Services;
 
public class CupomServico(ICupomRepository cupomRepository) : ICupomServico
{
    private readonly ICupomRepository _cupomRepository = cupomRepository;

    public async Task<IEnumerable<CupomDto>> ListarAsync()
    {
        var cupons = await _cupomRepository.ObterTodosAsync();

        return cupons.Select(MapearParaDto);
    }

    public async Task<CupomDto> ObterByCodigoAsync(string codigoCupom)
    {
        var cupom = await _cupomRepository.ObterByCodigoAsync(codigoCupom)
            ?? throw new KeyNotFoundException($"Cupom '{codigoCupom}' é inválido ou não existe.");

        return MapearParaDto(cupom);
    }

    private static CupomDto MapearParaDto(Cupom cupom)
    {
        return new CupomDto(cupom.Id, cupom.CodigoCupom, cupom.PercentualDesconto, cupom.DataExpiracao);
    }
}
