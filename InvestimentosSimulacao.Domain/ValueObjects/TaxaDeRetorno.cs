namespace InvestimentosSimulacao.Domain.ValueObjects;

public class TaxaDeRetorno
{
    public double Valor { get; private set; }
    
    public TaxaDeRetorno(double valor)
    {
        if (valor < 0) throw new ArgumentException("A taxa de retorno deve ser maior ou igual a zero.");
        Valor = valor;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        TaxaDeRetorno other = (TaxaDeRetorno)obj;
        return Valor == other.Valor;
    }
    
    public override int GetHashCode()
    {
        return Valor.GetHashCode();
    }
}