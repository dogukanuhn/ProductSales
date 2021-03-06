using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProductSales.Application.PipelineBehaviours
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandValidators(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient,
            Func<AssemblyScanner.AssemblyScanResult, bool> filter = null
        )
        {
            services.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), lifetime));
            services.AddValidatorsFromAssemblies(assemblies, lifetime, filter);
            return services;
        }
    }
}
