
using IdentityService.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public class AppDbContext: IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions options) : base(options) { }
}