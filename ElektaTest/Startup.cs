using ElektaTest.Commands;
using ElektaTest.DbContexts;
using ElektaTest.Queries;
using ElektaTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektaTest
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
            services.AddControllers();
            services.AddTransient<IEquipmentAvailabilityService, EquipmentAvailabilityService>();
            services.AddTransient<IEmailNotificationService, EmailNotificationService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddHttpClient();

            services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddScoped<IQueryHandler, QueryHandler>();

            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandHandler, CommandHandler>();

            RegisterCommandHandlers(services);
            RegisterQueryHandlers(services);

            services.AddDbContext<AppointmentContext>(
            options => options.UseSqlite(@"Data Source=sqliteDB1.db"));
            services.AddMvc();
            services.AddSwaggerDocument();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterQueryHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<IQuery>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>))
                .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromAssemblyOf<ICommand>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>))
                .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
