using Microsoft.AspNetCore.Mvc;
using CarrinhoCompras.Application.Interfaces.Servicos;

namespace CarrinhoCompras.API.Controllers;

[ApiController]
[Route("api/cupons")]
public class CuponsController(ICupomServico servico) : ControllerBase
{
    private readonly ICupomServico _servico = servico;

    [HttpGet]
    public async Task<IActionResult> ListarAsync()
    {
        return Ok(await _servico.ListarAsync());
    }

    [HttpGet("{codigoCupom}")]
    public async Task<IActionResult> ObterByCodigoAsync(string codigoCupom)
    {
        try
        {
            return Ok(await _servico.ObterByCodigoAsync(codigoCupom));
        }
        catch (KeyNotFoundException excecao)
        {
            return Problem(
                detail: excecao.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Cupom não encontrado");
        }
    }
}