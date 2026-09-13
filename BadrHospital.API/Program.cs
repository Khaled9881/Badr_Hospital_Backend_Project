
using BadrHospital.Application.Behaviors;
using FluentValidation;

namespace BadrHospital.API
{
    public class Program
    {
        public static void Main(string[] args)
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
