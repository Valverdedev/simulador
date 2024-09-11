using InvestimentosSimulacao.Domain.Dominio.Base;
using InvestimentosSimulacao.Domain.Enums;
using InvestimentosSimulacao.Domain.ValueObjects;

namespace InvestimentosSimulacao.Domain.Dominio.Entidades;

public class Ativo : EntidadeBase
{
    public string Nome { get; private set; }
    public TipoInvestimentoEnum TipoInvestimento { get; private set; }
    public TaxaDeRetorno TaxaDeRetorno { get; private set; }
    
    private Ativo()
    {
    }
    
    public Ativo(string nome, TipoInvestimentoEnum tipoInvestimento, TaxaDeRetorno taxaDeRetorno)
    {
        if (string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome do ativo é obrigatório.");
        if (string.IsNullOrEmpty(tipoInvestimento.ToString())) throw new ArgumentException("Tipo do ativo é obrigatório.");
        if (taxaDeRetorno == null) throw new ArgumentNullException(nameof(taxaDeRetorno));

        Nome = nome;
        TipoInvestimento = tipoInvestimento;
        TaxaDeRetorno = taxaDeRetorno;
    }
    
    public double SimularRetorno(double valorInicial, int prazoAnos)
    {
        return valorInicial * Math.Pow(1 + TaxaDeRetorno.Valor, prazoAnos);
    }
}