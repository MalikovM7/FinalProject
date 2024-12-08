using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    // Additional fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

}