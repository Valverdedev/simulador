using InvestimentosSimulacao.Domain.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace InvestimentosSimulacao.Infrastructure;

public class PostgreContext : DbContext
{
    public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
    {
    }

    public DbSet<Ativo> Ativos { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreContext).Assembly);
    }
   
    
}