﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductSales.Application.Helpers;
using ProductSales.Application.PipelineBehaviours;
using ProductSales.Application.Services;
using ProductSales.Application.Services.RuleChecker;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.BusinessRules;
using System.Reflection;

namespace ProductSales.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCommandValidators(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient<IJwtHandler, JwtHandler>();
            services.AddTransient<ICipherService, CipherService>();

            services.AddScoped<ICustomerUniqunessChecker, CustomerUniqunessChecker>();


            services.AddDataProtection();
            return services;

        }
    }
}