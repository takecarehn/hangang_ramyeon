using System.Security.Claims;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static void AddAsyncSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
    {
        builder.UseAsyncSeeding(async (context, _, ct) =>
        {
            var initialiser = serviceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.SeedAsync();
        });
    }

    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database. Retrying...");
            await Task.Delay(5000);
            await TrySeedAsync();
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            var roleResult = await _roleManager.CreateAsync(administratorRole);
            if (!roleResult.Succeeded)
            {
                _logger.LogError("Failed to create administrator role: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                throw new Exception("Failed to create administrator role.");
            }
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            var userResult = await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!userResult.Succeeded)
            {
                _logger.LogError("Failed to create administrator user: {Errors}", string.Join(", ", userResult.Errors.Select(e => e.Description)));
                throw new Exception("Failed to create administrator user.");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }
        }

        var existingClaims = await _roleManager.GetClaimsAsync(administratorRole);

        var claimsToAdd = new List<Claim>
        {
            new("Permission.Role", "Permission.Role.Add"),
            new("Permission.Role", "Permission.Role.Update"),
            new("Permission.RoleClaim", "Permission.RoleClaim.UpdateClaims"),
            new("Permission.RoleClaim", "Permission.RoleClaim.AddClaims"),
            new("Permission.RoleClaim", "Permission.User.Add"),
            new("Permission.RoleClaim", "Permission.User.Update"),
            new("Permission.RoleClaim", "Permission.User.Delete"),
            new("Permission.RoleClaim", "Permission.User.GetUserName"),
            new("Permission.RoleClaim", "Permission.User.Lock"),
            new("Permission.RoleClaim", "Permission.User.Unlock"),
            new("Permission.RoleClaim", "Permission.User.ChangePass"),
            new("Permission.RoleClaim", "Permission.User.ResetPass"),
            new("Permission.RoleClaim", "Permission.UserClaim.AddClaims"),
            new("Permission.RoleClaim", "Permission.UserClaim.ViewClaims"),
            new("Permission.RoleClaim", "Permission.RoleClaim.UpdateClaims"),
            new("Permission.RoleClaim", "Permission.UserClaim.UpdateClaims"),
            new("Permission.RoleClaim", "Permission.RoleClaim.AllClaims"),
            new("Permission.RoleClaim", "Permission.RoleClaim.Upload"),
            new("Permission.RoleClaim", "Permission.Role.Add"),
            new("Permission.RoleClaim", "Permission.Role.Update"),
            new("Permission.RoleClaim", "Permission.Role.Delete"),
            new("Permission.RoleClaim", "Permission.Role.GetAll")
        };

        foreach (var claim in claimsToAdd)
        {
            if (!existingClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
            {
                var claimResult = await _roleManager.AddClaimAsync(administratorRole, claim);
                if (!claimResult.Succeeded)
                {
                    _logger.LogError("Failed to add claim {ClaimType}:{ClaimValue} to role {RoleName}: {Errors}",
                        claim.Type, claim.Value, administratorRole.Name, string.Join(", ", claimResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
