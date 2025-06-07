namespace DrakarVpn.Domain.ModelDto.Auth;

public class RegisterResponseDto
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}
