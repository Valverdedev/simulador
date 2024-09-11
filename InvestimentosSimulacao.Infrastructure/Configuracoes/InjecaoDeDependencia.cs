using FluentValidation;
using InvestimentosSimulacao.Application.Dtos.Ativo;
using InvestimentosSimulacao.Application.Interfaces;
using InvestimentosSimulacao.Application.Interfaces.AplicacaoServicos;
using InvestimentosSimulacao.Application.Maps;
using InvestimentosSimulacao.Application.Servicos;
using InvestimentosSimulacao.Application.Validacoes;
using InvestimentosSimulacao.Infrastructure;
using InvestimentosSimulacao.Infrastructure.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InvestimentosSimulacao.Infrastructure.Configuracoes;

public static class InjecaoDeDependencia
{
    public static IServiceCollection AddDependeciasAplicacao(this IServiceCollection services, string connectionString)
    {
        
        services.AddDbContext<PostgreContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(typeof(Map));
        
        services.AddScoped<IValidator<AtivoInserirDto>, AtivoInseriValidar>();
        
       services.AddScoped<IUnitOfWork, UnitOfWork>();
       services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
       services.AddScoped<IAtivoRepositorio, AtivoRepositorio>();
       
       
       services.AddScoped<IAtivoServico, AtivoServico>();
       
       
        return services;
    }
}