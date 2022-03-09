﻿using DevIO.Business.Intefaces;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configurantion
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDepedecies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            
            return services;
        }
    }
}
