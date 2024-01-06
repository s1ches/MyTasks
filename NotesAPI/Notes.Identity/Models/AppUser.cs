using Microsoft.AspNetCore.Identity;

namespace Notes.Identity.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}