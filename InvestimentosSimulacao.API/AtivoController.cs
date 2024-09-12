using InvestimentosSimulacao.Application.Dtos.Ativo;
using InvestimentosSimulacao.Application.Interfaces.AplicacaoServicos;
using Microsoft.AspNetCore.Mvc;

namespace InvestimentosSimulacao.API;

[Route("api/[controller]")]
[ApiController]
public class AtivoController : ControllerBase
{
    private readonly IAtivoServico _ativoServico;

    public AtivoController(IAtivoServico ativoServico)
    {
        _ativoServico = ativoServico;
        
    }
    
    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorIdAsync(int id)
    {
        try
        {
            var ativo = await _ativoServico.ObterPorIdAsync(id);
            return Ok(ativo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("ObterTodos")]
    public async Task<IActionResult> ObterTodosAsync()
    {
        try
        {
            var ativos = await _ativoServico.ObterTodosAsync();
            return Ok(ativos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("Adicionar")]
    public async Task<IActionResult> AdicionarAsync(AtivoInserirDto dto)
    {
        try
        {
            await _ativoServico.AdicionarAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            
        }
    }
    
}