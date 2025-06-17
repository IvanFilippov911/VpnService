

namespace DrakarVpn.Domain.ModelsOptions;

public class WireGuardConfigOptions
{
    public string ConfigFilePath { get; set; } = default!;
    public string ServerPublicKey { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
}
