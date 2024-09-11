using System.ComponentModel.DataAnnotations.Schema;

namespace InvestimentosSimulacao.Domain.Dominio.Base;

[NotMapped]
public abstract class EventoDominio
{
    public DateTime DataOcorrencia { get; protected set; } = DateTime.UtcNow;
    
    protected EventoDominio()
    {
        DataOcorrencia = DateTime.UtcNow;
    }
}