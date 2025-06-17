using WireGuardAgent.API.AbstractsRepositories;

namespace WireGuardAgent.API.Repositories;

public class FileSystem : IFileSystem
{
    public string[] ReadAllLines(string path) => File.ReadAllLines(path);
    public void WriteAllLines(string path, IEnumerable<string> lines) => File.WriteAllLines(path, lines);
    public void AppendAllLines(string path, IEnumerable<string> lines) => File.AppendAllLines(path, lines);

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

}