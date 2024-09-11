using InvestimentosSimulacao.Domain.Enums;
using InvestimentosSimulacao.Domain.ValueObjects;

namespace InvestimentosSimulacao.Application.Dtos.Ativo;

public class AtivoInserirDto
{
    public string Nome { get;  set; }
    public TipoInvestimentoEnum TipoInvestimento { get;  set; }
    
    public double TaxaDeRetorno { get;  set; }
    
}