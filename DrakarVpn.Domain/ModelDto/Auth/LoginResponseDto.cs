namespace DrakarVpn.Domain.ModelDto.Auth;

public class LoginResponseDto
{
    public string UserId { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
}
