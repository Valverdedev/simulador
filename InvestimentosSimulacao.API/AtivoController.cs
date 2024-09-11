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
    
    [HttpPost]
    [Route("Adicionar")]
    public async Task<IActionResult> AdicionarAsync(AtivoInserirDto dto)
    {
        await _ativoServico.AdicionarAsync(dto);
        return Ok();
    }
    
}