using System.Text;
using WireGuardAgent.API.AbstractsRepositories;

namespace WireGuardAgent.API.Repositories;

public class FileSystem : IFileSystem
{
    public string[] ReadAllLines(string path) => File.ReadAllLines(path);
    public void WriteAllLines(string path, IEnumerable<string> lines) => File.WriteAllLines(path, lines);
    public void AppendAllLines(string path, IEnumerable<string> lines)
    {
        using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
        using var writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

        foreach (var line in lines)
            writer.WriteLine(line);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

}