using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entity;

public class User: IdentityUser
{
    [Column("Role")]
    public UserRole Role { get; set; }
}