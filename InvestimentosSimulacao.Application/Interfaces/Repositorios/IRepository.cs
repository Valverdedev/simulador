using System.Linq.Expressions;

namespace InvestimentosSimulacao.Application.Interfaces;

public interface IRepositorio<T> where T : class
{
    Task<T?> ObterPorIdAsync(int id);
    Task<List<T>> ListarTodosAsync();
    Task<List<T>> FiltrarAsync(Expression<Func<T, bool>> predicate);
    Task AdicionarAsync(T entity);
    void Atualizar(T entity);
    void Remover(T entity);
    
}