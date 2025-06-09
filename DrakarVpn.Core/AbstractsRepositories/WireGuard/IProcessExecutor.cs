namespace DrakarVpn.Core.AbstractsRepositories.WireGuard;

public interface IProcessExecutor
{
    void Execute(string command, string args);
}


