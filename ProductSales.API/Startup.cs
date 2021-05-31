using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ProductSales.API.Helpers;
using ProductSales.API.Helpers.Middleware;
using ProductSales.Application;
using ProductSales.Infrastructure;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductSales.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddApplication();
   
            services.AddCors();
            services.AddControllers();

            #region JWT
            //JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<JwtSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               //JWT kullanacaðým ve ayarlarý da þunlar olsun dediðimiz yer ise burasýdýr.
               .AddJwtBearer(x =>
               {
                   //Gelen isteklerin sadece HTTPS yani SSL sertifikasý olanlarý kabul etmesi(varsayýlan true)
                   x.RequireHttpsMetadata = false;
                   //Eðer token onaylanmýþ ise sunucu tarafýnda kayýt edilir.
                   x.SaveToken = true;
                   //Token içinde neleri kontrol edeceðimizin ayarlarý.
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       //Token 3.kýsým(imza) kontrolü
                       ValidateIssuerSigningKey = true,
                       //Neyle kontrol etmesi gerektigi
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            #endregion



            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });




                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

            }).AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductSales.API v1"));
            }
            app.AppInfrastructure();



            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (ValidationException e)
                {
                    var response = ctx.Response;
                    if (response.HasStarted)
                        throw;

                    ctx.RequestServices
                        .GetRequiredService<ILogger<Startup>>()
                        .LogWarning(e, "Command could not be validated!");

                    response.Clear();
                    response.StatusCode = 400;
                    response.ContentType = "application/json";
                    await response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        Message = "Opps.. Something went terribly wrong!",
                        ModelState = e.Errors.ToDictionary(error => error.ErrorCode, error => error.ErrorMessage)
                    }), Encoding.UTF8, ctx.RequestAborted);
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
            app.UseHealthChecks("/hc");
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
