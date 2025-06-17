namespace WireGuardAgent.API.AbstractsRepositories;

public interface IProcessExecutor
{
    void Execute(string command, string args);
}
