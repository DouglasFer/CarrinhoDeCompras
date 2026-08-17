using CarrinhoCompras.Application.DTOs;
using CarrinhoCompras.Application.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using static CarrinhoCompras.Application.DTOs.RequestsDtos;

namespace CarrinhoCompras.API.Controllers;

[ApiController]
[Route("api/carrinhos")]
public class CarrinhosController(ICarrinhoServico servico) : ControllerBase
{
    private readonly ICarrinhoServico _servico = servico;

    [HttpPost]
    public async Task<IActionResult> CriarAsync()
    {
        return Ok(await _servico.CriarAsync());
    }

    [HttpGet("{id:int}")]
    public Task<IActionResult> ObterByIdAsync(int id) =>
        ExecutarAsync(() => _servico.ObterByIdAsync(id));

    [HttpPost("{id:int}/itens")]
    public Task<IActionResult> AdicionarItemAsync(int id, AdicionarItemRequest request) =>
        ExecutarAsync(() => _servico.AdicionarItemAsync(id, request.ProdutoId, request.Quantidade));

    [HttpPut("{id:int}/itens/{produtoId:int}")]
    public Task<IActionResult> AlterarQuantidadeAsync(int id, int produtoId, AlterarQuantidadeRequest request) =>
        ExecutarAsync(() => _servico.AlterarQuantidadeAsync(id, produtoId, request.Quantidade));

    [HttpDelete("{id:int}/itens/{produtoId:int}")]
    public Task<IActionResult> RemoverItemAsync(int id, int produtoId) =>
        ExecutarAsync(() => _servico.RemoverItemAsync(id, produtoId));

    [HttpPost("{id:int}/cupom")]
    public Task<IActionResult> AplicarCupomAsync(int id, CupomRequest request) =>
        ExecutarAsync(() => _servico.AplicarCupomAsync(id, request.CodigoCupom));

    [HttpDelete("{id:int}/cupom")]
    public Task<IActionResult> RemoverCupomAsync(int id) =>
        ExecutarAsync(() => _servico.RemoverCupomAsync(id));

    [HttpPost("{id:int}/finalizar")]
    public Task<IActionResult> FinalizarAsync(int id) =>
        ExecutarAsync(() => _servico.FinalizarAsync(id));

    [HttpGet("historico")]
    public async Task<IActionResult> ObterHistoricoAsync()
    {
        return Ok(await _servico.ObterHistoricoAsync());
    }
    private async Task<IActionResult> ExecutarAsync(Func<Task<CarrinhoDto>> operacao)
    {
        try
        {
            return Ok(await operacao());
        }
        catch (KeyNotFoundException excecao)
        {
            return Problem(detail: excecao.Message, statusCode: StatusCodes.Status404NotFound, title : "Não encontrado");
        }
        catch (ArgumentOutOfRangeException excecao)
        {
            return Problem(detail: excecao.Message, statusCode: StatusCodes.Status400BadRequest, title: "Quantidade inválida");
        }
        catch (ArgumentException excecao)
        {
            return Problem(detail: excecao.Message, statusCode: StatusCodes.Status400BadRequest, title: "Estoque insuficiente");
        }
        catch (InvalidOperationException excecao)
        {
            return Problem(detail: excecao.Message, statusCode: StatusCodes.Status409Conflict, title: "Carrinho finalizado");
        }
    }
}