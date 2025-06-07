using DrakarVpn.API.Settings.Extensions;

namespace DrakarVpn;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services.AddPostgreSqlContext(builder.Configuration);
        services.AddIdentityConfiguration();
        services.AddAuthenticationConfiguration(builder.Configuration);
        services.AddJwtTokenGenerator();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var app = builder.Build();

        await app.Services.InitializeRolesAsync();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
