using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScooterApi.Data.Database;
using ScooterApi.Data.Repository.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Messaging.Send.Options.v1;
using ScooterApi.Messaging.Send.Sender.v1;
using ScooterApi.Models.v1;
using ScooterApi.Service.v1.Command;
using ScooterApi.Service.v1.Query;
using ScooterApi.Validators.v1;

namespace ScooterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<ScooterContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("ScooterDatabase"));
                });
            }
            else
            {
                services.AddDbContext<ScooterContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton);
               
            }

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Scooter Api",
                    Description = "A simple API to create scooter",
                    Contact = new OpenApiContact
                    {
                        Name = "Novokshonov Evgeniy",
                        Email = "jon2k@mail.ru",                    
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IScooterRepository, ScooterRepository>();

            services.AddTransient<IValidator<DataFromScooterModel>, DataFromScooterModelValidator>();
            
            services.AddSingleton<IScooterAddSender, ScooterAddSender>();

            services.AddTransient<IRequestHandler<CreateScooterCommand, Scooter>, CreateScooterCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateScooterCommand, Scooter>, UpdateScooterCommandHandler>();
            services.AddTransient<IRequestHandler<GetScooterByIdQuery, List<Scooter>>, GetScooterByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetScootersQuery, List<Scooter>>, GetScootersQueryHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scooter API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}