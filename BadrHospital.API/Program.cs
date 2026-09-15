
using BadrHospital.Application.Behaviors;
using BadrHospital.Infrastructure.Identity;
using FluentValidation;
using HospitalManagementSystem.Infrastructure.Persistence;
using HospitalManagementSystem.Infrastructure.Persistence.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BadrHospital.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(ValidationBehavior<,>).Assembly);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await DataSeeder.SeedAsync(services);
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
