using Microsoft.EntityFrameworkCore;
using CP2_CS.Application.Interfaces;
using CP2_CS.Application.Services;
using CP2_CS.Domain.Interfaces;
using CP2_CS.Infrastructure.Data;
using CP2_CS.Infrastructure.Repositories;

namespace CP2_CS.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        services.AddScoped<IAgenciaService, AgenciaService>();
        services.AddScoped<IAgenciaRepository, AgenciaRepository>();

        services.AddScoped<IContratacaoService, ContratacaoService>();
        services.AddScoped<IContratacaoRepository, ContratacaoRepository>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        return services;
    }
}