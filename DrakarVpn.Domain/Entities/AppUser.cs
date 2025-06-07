using Microsoft.AspNetCore.Identity;

namespace DrakarVpn.Domain.Entities;

public class AppUser : IdentityUser
{
    
    public string? FullName { get; set; }             
    public string? Country { get; set; }               
    public string? Language { get; set; }             
  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public DateTime? LastLoginAt { get; set; }         
   
    public bool IsBlocked { get; set; } = false;       
    public bool IsVerified { get; set; } = false;     

    public string? AdminNote { get; set; }             

}
