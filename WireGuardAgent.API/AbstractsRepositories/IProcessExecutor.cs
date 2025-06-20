using WireGuardAgent.API.models;

namespace WireGuardAgent.API.AbstractsRepositories;

public interface IProcessExecutor
{
    ProcessResult ExecuteWithOutput(string fileName, string arguments);

}
