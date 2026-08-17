using CarrinhoCompras.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarrinhoCompras.API.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController(
    IProdutoServico servico) : ControllerBase
{
    private readonly IProdutoServico _servico = servico;

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok(await _servico.ObterTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterById(int id)
    {
        try
        {
            return Ok(await _servico.ObterByIdAsync(id));
        }
        catch (KeyNotFoundException excecao)
        {
            return Problem(
                detail: excecao.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Produto não encontrado");
        }
    }
}
