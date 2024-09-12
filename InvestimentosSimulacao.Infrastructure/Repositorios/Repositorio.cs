using System.Linq.Expressions;
using InvestimentosSimulacao.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestimentosSimulacao.Infrastructure.Repositorios;

public class Repositorio<T> : IRepositorio<T> where T : class
{
    protected readonly PostgreContext _context;
    private readonly DbSet<T> _dbSet;
    
    public Repositorio(PostgreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    
    public async Task<T?> ObterPorIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> ListarTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<T>> FiltrarAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate!).ToListAsync();
    }

    public async Task AdicionarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

    }

    public void Atualizar(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remover(T entity)
    {
        _dbSet.Remove(entity);
    }
    
}