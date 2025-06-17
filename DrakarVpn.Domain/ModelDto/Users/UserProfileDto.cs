namespace DrakarVpn.Domain.ModelDto.Users;

public class UserProfileDto
{
    public string Email { get; set; } = null!;           
    public string FullName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Language { get; set; } = "ru";         
}

