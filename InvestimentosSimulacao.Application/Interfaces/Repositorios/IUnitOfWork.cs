using InvestimentosSimulacao.Domain.Dominio.Entidades;

namespace InvestimentosSimulacao.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepositorio<T> Repository<T>() where T : class;
    IAtivoRepositorio Ativos { get; }   
    Task<int> CompleteAsync();
    Task RollbackAsync();
   
}