using WireGuardAgent.API.Settings.Extensions;

namespace WireGuardAgent.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();


        var configFilePathTest = builder.Configuration
            .GetSection("WireGuardConfig")
            .GetValue<string>("ConfigFilePath");

        Console.WriteLine($"[Program.cs] ConfigFilePath from Configuration = {configFilePathTest}");

        services.AddWireGuardAgentServices(builder.Configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        var app = builder.Build();

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
