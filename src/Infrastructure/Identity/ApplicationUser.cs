using HangangRamyeon.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HangangRamyeon.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? AvatarUrl { get; }
    public string? DeviceToken { get; }

    public ICollection<UserShop> UserShops { get; } = new List<UserShop>();
}
