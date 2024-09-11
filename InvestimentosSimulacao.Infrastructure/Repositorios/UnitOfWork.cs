using InvestimentosSimulacao.Application.Interfaces;
using InvestimentosSimulacao.Domain.Dominio.Entidades;

namespace InvestimentosSimulacao.Infrastructure.Repositorios;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgreContext _context;
    private readonly Dictionary<string, object> _repositorios;
    private AtivoRepositorio _ativoRepositorio;
    
    public IAtivoRepositorio Ativos => _ativoRepositorio ??= new AtivoRepositorio(_context);
    
    public UnitOfWork(PostgreContext context)
    {
        _context = context;
        _repositorios = new Dictionary<string, object>(); 
    }
    
    public IRepositorio<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;

        if (!_repositorios.ContainsKey(type))
        {
            var repositoryType = typeof(Repositorio<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

            _repositorios.Add(type, repositoryInstance);
        }

        return (IRepositorio<T>)_repositorios[type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await Task.Run(() => _context.ChangeTracker.Clear());
    }
    public void Dispose()
    {
        _context.Dispose();
    }
  
}