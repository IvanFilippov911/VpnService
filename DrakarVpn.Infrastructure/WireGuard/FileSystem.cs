using DrakarVpn.Core.AbstractsRepositories.WireGuard;

namespace DrakarVpn.Core.Services.Configs;

public class FileSystem : IFileSystem
{
    public string[] ReadAllLines(string path) => File.ReadAllLines(path);
    public void WriteAllLines(string path, IEnumerable<string> lines) => File.WriteAllLines(path, lines);
    public void AppendAllLines(string path, IEnumerable<string> lines) => File.AppendAllLines(path, lines);
}

