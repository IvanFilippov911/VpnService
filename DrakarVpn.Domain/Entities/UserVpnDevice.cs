
using System.ComponentModel.DataAnnotations;

namespace DrakarVpn.Domain.Entities;

public class UserVpnDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }
    
    [MaxLength(50)]
    public string DeviceName { get; set; } = "";  

    public Guid PeerId { get; set; }              
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

