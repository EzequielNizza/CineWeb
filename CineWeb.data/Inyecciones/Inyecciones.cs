using Cine.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cine.Data.Inyecciones
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PeliculasContext>(o =>
                o.UseSqlServer(connectionString));
            services.AddScoped<PeliculasContext, PeliculasContext>();
            return services;
        }
    }

}