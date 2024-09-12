using System.ComponentModel.DataAnnotations.Schema;
using InvestimentosSimulacao.Domain.Enums;
using InvestimentosSimulacao.Domain.Helpers;
using InvestimentosSimulacao.Domain.ValueObjects;

namespace InvestimentosSimulacao.Application.Dtos.Ativo;

public class AtivoDto
{
    public int Id { get;  set; }
    public string Nome { get;  set; }
    public TipoInvestimentoEnum TipoInvestimento { get;  set; }
    public double TaxaDeRetorno { get;  set; }
    
    [NotMapped]
    public string TipoInvestimentoString => EnumHelper.ObterDescricaoEnum(TipoInvestimento);
    
}