﻿namespace DrakarVpn.Domain.ModelDto.Auth;

public class RegisterRequestDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
