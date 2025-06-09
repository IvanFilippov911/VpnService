namespace DrakarVpn.Core.AbstractsRepositories.WireGuard;

public interface IFileSystem
{
    string[] ReadAllLines(string path);
    void WriteAllLines(string path, IEnumerable<string> lines);
    void AppendAllLines(string path, IEnumerable<string> lines);
}

