using Bogus;
using Deopeia.Common.Events;
using Deopeia.Common.Localization;
using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Roles;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure;

public class IdentitySeeder : DbSeeder
{
    public override void Seed(DbContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        context.Set<Client>().AddRange(GetClients());

        var users = GetUsers();
        var roles = GetRoles();
        var permissions = GetPermissions();

        foreach (var user in users)
        {
            user.MarkAsTrader();
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

        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<User>().AddRange(users);
        context.Set<Role>().AddRange(roles);
        context.Set<Permission>().AddRange(permissions);

        var eventLogs = users.SelectMany(x => x.DomainEvents).Select(x => new EventLog(x));
        context.Set<EventLog>().AddRange(eventLogs);

        context.SaveChanges();
    }

    private static IEnumerable<LocaleResource> GetLocaleResources()
    {
        var en = CultureInfo.GetCultureInfo("en");
        var resourcesEN = new LocaleResource[] { };

        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        var resourcesZHHant = new LocaleResource[] { };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

        return results;
    }

    private static List<Client> GetClients()
    {
        var results = new List<Client>
        {
            new(
                "Finance",
                null,
                GrantTypes.AuthorizationCode | GrantTypes.RefreshToken,
                new[] { "openid", "profile", "email", "api" },
                new Uri[]
                {
                    new("http://localhost:5173/auth/sign-in-callback"),
                    new("http://localhost:5173/auth/refresh-callback"),
                }
            ),
        };

        return results;
    }

    private static List<User> GetUsers()
    {
        var password = "123fff";
        var results = new Faker<User>()
            .CustomInstantiator(x => new User(x.Internet.UserName(), password, true))
            .GenerateBetween(10, 50);
        results.Add(new User("admin", password, true));

        return results;
    }

    private static List<Role> GetRoles()
    {
        var result = new List<Role>
        {
            new("Administrator", "The highest level of access within the system.", true),
            new("Trader", "An entity who buys and sells financial instruments.", true),
        };

        return result;
    }

    private static List<Permission> GetPermissions()
    {
        var results = new List<Permission>
        {
            new(
                "SignIn",
                "Sign In",
                "Allowing the user to enter the system but not necessarily granting any further permissions.",
                true
            ),
            new("Trade", "Allowing the user to buy and sell financial instruments.", null, true),
        };

        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        results[0].UpdateName("登入", zhHant);
        results[1].UpdateName("人力資源", zhHant);

        return results;
    }
}
