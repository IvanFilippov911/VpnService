namespace DrakarVpn.Domain.ModelDto.Auth;

public class RegisterResponseDto
{
    public string UserId { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}
