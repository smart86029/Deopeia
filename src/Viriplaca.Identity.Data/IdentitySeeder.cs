using Bogus;
using System.Globalization;
using Viriplaca.Identity.Domain.Clients;
using Viriplaca.Identity.Domain.Permissions;
using Viriplaca.Identity.Domain.Roles;
using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.Data;

public class IdentitySeeder : IDbSeeder<IdentityContext>
{
    public async Task SeedAsync(IdentityContext context)
    {
        if (context.Set<Client>().Any())
        {
            return;
        }

        context.Set<Client>().AddRange(GetClients());

        var users = GetUsers();
        var roles = GetRoles();
        var permissions = GetPermissions();

        foreach (var user in users)
        {
            foreach (var role in roles)
            {
                user.AssignRole(role);
            }
        }

        foreach (var role in roles)
        {
            foreach (var permission in permissions)
            {
                role.AssignPermission(permission);
            }
        }

        context.Set<User>().AddRange(users);
        context.Set<Role>().AddRange(roles);
        context.Set<Permission>().AddRange(permissions);

        await context.SaveChangesAsync();
    }

    private IEnumerable<Client> GetClients()
    {
        var results = new Client[]
        {
            new(
                "Enterprise",
                null,
                GrantTypes.Code,
                new[] { "openid", "profile", "email", "api" },
                new Uri[]
                {
                    new("http://localhost:5173/auth/sign-in-callback"),
                    new("http://localhost:5173/auth/refresh-callback"),
                }),
        };

        return results;
    }

    private IEnumerable<User> GetUsers()
    {
        var password = "123fff";
        var results = new Faker<User>()
            .CustomInstantiator(x => new User(x.Internet.UserName(), password, true))
            .GenerateBetween(10, 50);
        results.Add(new User("admin", password, true));

        return results;
    }

    private IEnumerable<Role> GetRoles()
    {
        var result = new Role[]
        {
            new("Administrator", true),
            new("Human Resources", true)
        };

        return result;
    }

    private IEnumerable<Permission> GetPermissions()
    {
        var results = new Permission[]
        {
            new("SignIn", true),
            new("HumanResources", true),
        };

        var enUS = CultureInfo.GetCultureInfo("en-US");
        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        results[0].UpdateName("Sign In", enUS);
        results[0].UpdateName("登入", zhTW);
        results[1].UpdateName("Human Resources", enUS);
        results[1].UpdateName("人力資源", zhTW);

        return results;
    }
}
