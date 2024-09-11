using InvestimentosSimulacao.Domain.Dominio.Base;
using InvestimentosSimulacao.Domain.Dominio.Entidades;

namespace InvestimentosSimulacao.Domain.Events;

public class SimulacaoConcluidaEvento : EventoDominio
{
    public Simulacao Simulacao { get; }
    
    public SimulacaoConcluidaEvento(Simulacao simulacao)
    {
        Simulacao = simulacao;
    }
}