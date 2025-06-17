using DrakarVpn.Core.AbstractsServices.WireGuard;

namespace DrakarVpn.Core.Services.WireGuard;

public class WireGuardIpAllocator : IWireGuardIpAllocator
{
    private readonly SortedSet<int> allocatedIpNumbers = new();
    private readonly object lockObj = new();

    private readonly int baseIpNumber;
    private readonly int maxIpNumber;
    private readonly string subnetPrefix;

    public WireGuardIpAllocator(
        IEnumerable<string> alreadyAllocatedIps = null,
        string subnetPrefix = "10.0.0",
        int baseIpNumber = 2,
        int maxIpNumber = 254)
    {
        this.subnetPrefix = subnetPrefix;
        this.baseIpNumber = baseIpNumber;
        this.maxIpNumber = maxIpNumber;

        if (alreadyAllocatedIps != null)
        {
            foreach (var ip in alreadyAllocatedIps)
            {
                var number = ParseIpToNumber(ip);
                allocatedIpNumbers.Add(number);
            }
        }
    }

    public Task<string> AllocateNextIpAsync()
    {
        lock (lockObj)
        {
            for (int i = baseIpNumber; i <= maxIpNumber; i++)
            {
                if (!allocatedIpNumbers.Contains(i))
                {
                    allocatedIpNumbers.Add(i);
                    return Task.FromResult(FormatIp(i));
                }
            }

            throw new InvalidOperationException("No available IPs.");
        }
    }

    public Task ReleaseIpAsync(string ip)
    {
        lock (lockObj)
        {
            var number = ParseIpToNumber(ip);
            allocatedIpNumbers.Remove(number);
            return Task.CompletedTask;
        }
    }

    private int ParseIpToNumber(string ip)
    {
        var parts = ip.Split('/')[0].Split('.');

        if (parts.Length != 4)
        {
            throw new FormatException($"Invalid IP format: {ip}");
        }

        if (!int.TryParse(parts[3], out int number))
        {
            throw new FormatException($"Invalid IP format: {ip}");
        }

        if (number < baseIpNumber || number > maxIpNumber)
        {
            throw new InvalidOperationException($"IP {ip} is outside of allowed range.");
        }

        return number;
    }

    private string FormatIp(int number)
    {
        return $"{subnetPrefix}.{number}/32";
    }
}

