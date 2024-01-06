using System.ComponentModel.DataAnnotations;

namespace Notes.Identity.Models;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string ReturnUrl { get; set; } = string.Empty;
}