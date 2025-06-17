namespace WireGuardAgent.API.AbstractsRepositories;

public interface IFileSystem
{
    string[] ReadAllLines(string path);
    void WriteAllLines(string path, IEnumerable<string> lines);
    void AppendAllLines(string path, IEnumerable<string> lines);
    bool FileExists(string path);

}
