using InvestimentosSimulacao.Domain.Dominio.Base;
using InvestimentosSimulacao.Domain.Events;

namespace InvestimentosSimulacao.Domain.Dominio.Entidades;

public class Simulacao : EntidadeBase
{
    public Ativo Ativo { get; private set; }
    public double ValorInicial { get; private set; }
    public int PrazoAnos { get; private set; }
    public double ValorFinal { get; private set; }
    public double Lucro { get; private set; }
    
    public Simulacao(Ativo ativo, double valorInicial, int prazoAnos)
    {
        if (valorInicial <= 0) throw new ArgumentException("O valor inicial deve ser maior que zero.");
        if (prazoAnos <= 0) throw new ArgumentException("O prazo deve ser maior que zero.");
        if (ativo == null) throw new ArgumentNullException(nameof(ativo));

        Ativo = ativo;
        ValorInicial = valorInicial;
        PrazoAnos = prazoAnos;

        ExecutarSimulacao();
    }

    private void ExecutarSimulacao()
    {
        ValorFinal = Ativo.SimularRetorno(ValorInicial, PrazoAnos);
        Lucro = ValorFinal - ValorInicial;

      
        AdicionarEventoDominio(new SimulacaoConcluidaEvento(this));
    }
}