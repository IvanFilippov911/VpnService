using DrakarVpn.Domain.ModelsOptions;
using Microsoft.Extensions.Configuration;
using WireGuardAgent.API.AbstractsRepositories;
using WireGuardAgent.API.AbstractsServices;
using WireGuardAgent.API.Repositories;
using WireGuardAgent.API.Services;
using WireGuardAgent.API.Settings.Extensions;

namespace WireGuardAgent.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5002);
        });

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

         services.Configure<WireGuardConfigOptions>(opt => opt.ConfigFilePath = "/etc/wireguard/wg0.conf");

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IProcessExecutor, ProcessExecutor>();
        services.AddScoped<IWireGuardConfigService, WireGuardConfigService>();
        
        var configFilePathTest = builder.Configuration
            .GetSection("WireGuardConfig")
            .GetValue<string>("ConfigFilePath");

        Console.WriteLine($"[Program.cs] ConfigFilePath from Configuration = {configFilePathTest}");

        //services.AddWireGuardAgentServices(builder.Configuration);

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
