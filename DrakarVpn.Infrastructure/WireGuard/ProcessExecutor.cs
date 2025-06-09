using DrakarVpn.Core.AbstractsRepositories.WireGuard;
using System.Diagnostics;

namespace DrakarVpn.Core.Services.Configs;

public class ProcessExecutor : IProcessExecutor
{
    public void Execute(string command, string args)
    {
        var psi = new ProcessStartInfo
        {
            FileName = command,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = Process.Start(psi))
        {
            if (process == null) throw new Exception($"Failed to start process {command}");

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var error = process.StandardError.ReadToEnd();
                throw new Exception($"Process {command} failed. ExitCode={process.ExitCode}. Error={error}");
            }
        }
    }
}

