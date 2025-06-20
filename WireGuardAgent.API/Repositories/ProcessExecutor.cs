using System.Diagnostics;
using WireGuardAgent.API.AbstractsRepositories;
using WireGuardAgent.API.models;

namespace WireGuardAgent.API.Repositories;

public class ProcessExecutor : IProcessExecutor
{
    public ProcessResult ExecuteWithOutput(string fileName, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        var stdout = process.StandardOutput.ReadToEnd();
        var stderr = process.StandardError.ReadToEnd();

        process.WaitForExit();

        return new ProcessResult
        {
            ExitCode = process.ExitCode,
            StandardOutput = stdout,
            StandardError = stderr
        };
    }
}
