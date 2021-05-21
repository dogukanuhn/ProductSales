using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Abstract.Services;
using ProductSales.Infrastructure.Caching;
using ProductSales.Infrastructure.Config;
using ProductSales.Infrastructure.Interfaces;
using ProductSales.Infrastructure.Payments;
using ProductSales.Infrastructure.Repositories;

namespace ProductSales.Infrastructure
{
    public static class DI
    {
        public static IApplicationBuilder AppInfrastructure(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            var _warmUpService = app.ApplicationServices.GetService<ICacheWarmUpService>();
            BackgroundJob.Enqueue(() => _warmUpService.WarmUp());


            return app;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });

            services.Configure<IyzicoSettings>(options =>
            {
                options.ApiKey = configuration
                    .GetSection(nameof(IyzicoSettings) + ":" + IyzicoSettings.ApiKeyValue).Value;
                options.BaseUrl = configuration
                    .GetSection(nameof(IyzicoSettings) + ":" + IyzicoSettings.BaseUrlValue).Value;
                options.SecretKey = configuration
                   .GetSection(nameof(IyzicoSettings) + ":" + IyzicoSettings.SecretKeyValue).Value;
            });


            services.AddStackExchangeRedisCache(action =>
            {
                action.Configuration = "localhost:6379,DefaultDatabase=1";
            });

            services.AddTransient(typeof(IMongoDbContext<>), typeof(DbContext<>));

            services.AddTransient<ICacheManager, RedisCacheManager>();
            services.AddTransient<ICacheWarmUpService, CacheWarmUpService>();


            services.AddTransient<IPaymentService, PaymentService>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISellerProductRepository, SellerProductRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();


            var mongoURL = configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
            var database = configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;


            services.AddHangfire(configuration => configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseMongoStorage(mongoURL + $"/{database}", new MongoStorageOptions
             {
                 MigrationOptions = new MongoMigrationOptions
                 {
                     MigrationStrategy = new MigrateMongoMigrationStrategy(),
                     BackupStrategy = new CollectionMongoBackupStrategy()
                 },
                 Prefix = "hangfire.mongo",
                 CheckConnection = true
             })
         );




            services.AddHangfireServer();


            return services;

        }
    }
}
