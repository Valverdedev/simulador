using InvestimentosSimulacao.Application.Interfaces;
using InvestimentosSimulacao.Domain.Dominio.Entidades;

namespace InvestimentosSimulacao.Infrastructure.Repositorios;

public class AtivoRepositorio : Repositorio<Ativo>, IAtivoRepositorio
{
    public AtivoRepositorio(PostgreContext context) : base(context)
    {
    }
}