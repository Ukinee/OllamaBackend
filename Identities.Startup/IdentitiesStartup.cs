﻿using Identities.DataAccess.Implementations;
using Identities.DataAccess.Interfaces;
using Identities.Services.Factories;
using Identities.SQRS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identities.Startup
{
    public static class IdentitiesStartup
    {
        public static IServiceCollection ConfigureIdentities(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                    .AddScoped<IIdentityRepository, IdentityRepository>()
                    .AddScoped<IdentityFactory>()
                    .AddScoped<CreateIdentityQuery>()
                ;
        }
    }
}
