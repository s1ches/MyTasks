using System.ComponentModel.DataAnnotations;

namespace Notes.Identity.Models;

public class RegisterViewModel
{
    [Required] 
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    public string ReturnUrl { get; set; } = null!;
}