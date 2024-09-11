using System.ComponentModel;

namespace InvestimentosSimulacao.Domain.Enums;

public enum TipoInvestimentoEnum
{
    [Description("LCA")]
    lca = 0,
    
    [Description("LCI")]
    lci = 1,
    
    [Description("CRI")]
    cri = 2,
    
    [Description("CRA")]
    cra = 3,
    
    [Description("CDB")]
    cdb = 4,
}