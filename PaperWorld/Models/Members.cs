using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Members : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
}
