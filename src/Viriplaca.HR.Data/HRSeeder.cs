using Bogus;
using Viriplaca.Common.Localization;
using Viriplaca.HR.Domain.Departments;
using Viriplaca.HR.Domain.Employees;
using Viriplaca.HR.Domain.Jobs;
using Viriplaca.HR.Domain.LeaveEntitlements;
using Viriplaca.HR.Domain.Leaves;
using Viriplaca.HR.Domain.LeaveTypes;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data;

public class HRSeeder : IDbSeeder<HRContext>
{
    public async Task SeedAsync(HRContext context)
    {
        if (context.Set<Department>().Any())
        {
            return;
        }

        var employees = GetEmployees();
        var departments = GetDepartments();
        var jobs = GetJobs();
        var leaveTypes = GetLeaveTypes();
        var leaves = GetLeaves(employees, leaveTypes);
        var leaveEntitlements = GetLeaveEntitlements(employees, leaveTypes);

        AssignJob(employees, departments, jobs);

        context.Set<Employee>().AddRange(employees);
        context.Set<Department>().AddRange(departments);
        context.Set<Job>().AddRange(jobs);
        context.Set<LeaveType>().AddRange(leaveTypes);
        context.Set<Leave>().AddRange(leaves);
        context.Set<LeaveEntitlement>().AddRange(leaveEntitlements);

        var localeResources = GetLocaleResources();
        context.Set<LocaleResource>().AddRange(localeResources);

        await context.SaveChangesAsync();
    }

    private List<Employee> GetEmployees()
    {
        var results = new Faker<Employee>()
            .CustomInstantiator(x =>
            {
                var gender = x.PickRandom(Sex.Male, Sex.Female);
                var employee = new Employee(
                    x.Name.FirstName((Bogus.DataSets.Name.Gender?)gender),
                    x.Name.LastName(),
                    x.Date.Past(20).ToDateOnly(),
                    gender,
                    x.PickRandom(MaritalStatus.Married, MaritalStatus.Single)
                );

                return employee;
            })
            .GenerateBetween(10, 50);

        return results;
    }

    private List<Department> GetDepartments()
    {
        var results = new List<Department>();
        var departmentRoot = new Department("Headquarters", true, null);
        results.Add(departmentRoot);
        results.Add(new Department("Finance", true, departmentRoot.Id));
        results.Add(new Department("Human Resources", true, departmentRoot.Id));
        results.Add(new Department("Research & Development", true, departmentRoot.Id));
        results.Add(new Department("Information Technology", true, departmentRoot.Id));

        return results;
    }

    private List<Job> GetJobs()
    {
        var results = new List<Job>
        {
            new("Chief Executive Officer", true),
            new("Chief Financial Officer", true),
            new("Manager", true),
            new("Accountant", true),
            new("Engineer", true),
            new("Technician", true),
            new("Staff", true),
        };

        return results;
    }

    private List<LeaveType> GetLeaveTypes()
    {
        var results = new List<LeaveType>
        {
            new("Personal", null),
            new(
                "Annual",
                "3 days for service of 6 months or more but less than 1 year\n7 days for service of 1 year or more but less than 2 years\n10 days for service of 2 years or more but less than 3 years\n14 days for service of 3 years or more but less than 5 years\n15 days for service of 5 years or more but less than 10 years\n1 additional day for each year of service over 10 years up to a maximum of 30 days",
                true
            ),
            new("Sick", null),
            new("Official", null),
            new("Menstrual", null),
            new("Marriage", null),
            new("Maternity", null),
            new("Paternity", null),
            new("Parental", null),
            new("Funeral", null),
            new("Compensatory", null),
        };

        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        results[0].UpdateName("事假", zhTW);
        results[1].UpdateName("年假", zhTW);
        results[2].UpdateName("病假", zhTW);
        results[3].UpdateName("公假", zhTW);
        results[4].UpdateName("生理假", zhTW);
        results[5].UpdateName("婚假", zhTW);
        results[6].UpdateName("產嫁", zhTW);
        results[7].UpdateName("陪產假", zhTW);
        results[8].UpdateName("育嬰假", zhTW);
        results[9].UpdateName("喪假", zhTW);
        results[10].UpdateName("補休", zhTW);

        return results;
    }

    private List<Leave> GetLeaves(List<Employee> employees, List<LeaveType> leaveTypes)
    {
        var results = new Faker<Leave>()
            .CustomInstantiator(x =>
            {
                var startedAt = x.Date.RecentOffset();
                var leave = new Leave(
                    x.PickRandom(leaveTypes).Id,
                    startedAt,
                    startedAt.AddDays(x.Random.Int(0, 3)),
                    string.Empty,
                    x.PickRandom(employees).Id
                );

                return leave;
            })
            .GenerateBetween(10, 50);

        return results;
    }

    private List<LeaveEntitlement> GetLeaveEntitlements(
        List<Employee> employees,
        List<LeaveType> leaveTypes
    )
    {
        var year = DateTime.UtcNow.Year;
        var startedOn = new DateOnly(year, 1, 1);
        var endedOn = new DateOnly(year, 12, 31);
        var results = employees
            .Select(x => new LeaveEntitlement(
                x.Id,
                leaveTypes[2].Id,
                startedOn,
                endedOn,
                WorkingTime.FromDays(14)
            ))
            .ToList();

        return results;
    }

    private void AssignJob(List<Employee> employees, List<Department> departments, List<Job> jobs)
    {
        var managerCount = 3;
        var faker = new Faker();
        for (var i = 0; i < employees.Count; i++)
        {
            var employee = employees[i];
            if (i < managerCount)
            {
                employee.AssignJob(departments[i], jobs[i], true, faker.Date.PastOffset());
                continue;
            }

            var department = faker.PickRandom(departments);
            var job = faker.PickRandom(jobs.Skip(managerCount));
            var isHead = jobs.IndexOf(job) < managerCount;
            employee.AssignJob(department, job, isHead, faker.Date.PastOffset());
        }
    }

    private List<LocaleResource> GetLocaleResources()
    {
        var enUS = CultureInfo.GetCultureInfo("en-US");
        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        var results = new List<LocaleResource>
        {
            FromNone(enUS, "Name", "Name"),
            FromEnum(enUS, MaritalStatus.Unknown, "Unknown"),
            FromEnum(enUS, MaritalStatus.Single, "Single"),
            FromEnum(enUS, MaritalStatus.Married, "Married"),
            FromEnum(enUS, MaritalStatus.Divorced, "Divorced"),
            FromEnum(enUS, MaritalStatus.Widowed, "Widowed"),
            FromEnum(enUS, Sex.NotKnown, "Not Known"),
            FromEnum(enUS, Sex.Male, "Male"),
            FromEnum(enUS, Sex.Female, "Female"),
            FromEnum(enUS, Sex.NotApplicable, "Not Applicable"),
            FromError(enUS, "AccessDenied", "Access denied."),
            FromError(enUS, "String.NotEmpty", "{Property} must not be empty."),
            FromError(enUS, "Date.OnOrBeforeNow", "{Property} must be on or before now."),
            FromError(enUS, "Date.AfterNow", "{Property} must be after now."),
            FromError(enUS, "Enum.Defined", "{Property} must be defined."),
            FromNone(zhTW, "Name", "名稱"),
            FromEnum(zhTW, MaritalStatus.Unknown, "未知"),
            FromEnum(zhTW, MaritalStatus.Single, "未婚"),
            FromEnum(zhTW, MaritalStatus.Married, "已婚"),
            FromEnum(zhTW, MaritalStatus.Divorced, "離婚"),
            FromEnum(zhTW, MaritalStatus.Widowed, "鰥寡"),
            FromEnum(zhTW, Sex.NotKnown, "未知"),
            FromEnum(zhTW, Sex.Male, "男性"),
            FromEnum(zhTW, Sex.Female, "女性"),
            FromEnum(zhTW, Sex.NotApplicable, "不適用"),
            FromError(zhTW, "AccessDenied", "存取被拒。"),
            FromError(zhTW, "String.NotEmpty", "{Property}不可為空。"),
            FromError(zhTW, "Date.OnOrBeforeNow", "{Property}必須等於或早於現在。"),
            FromError(zhTW, "Date.AfterNow", "{Property}必須晚於現在。"),
            FromError(zhTW, "Enum.Defined", "{Property}必須被定義。"),
        };

        return results;

        LocaleResource FromNone(CultureInfo culture, string code, string content)
        {
            return new LocaleResource(culture, LocaleResourceType.None, code, content);
        }

        LocaleResource FromEnum<TEnum>(CultureInfo culture, TEnum @enum, string content)
        {
            return new LocaleResource(
                culture,
                LocaleResourceType.Enum,
                $"{typeof(TEnum).Name}.{@enum:D}",
                content
            );
        }

        LocaleResource FromError(CultureInfo culture, string code, string content)
        {
            return new LocaleResource(culture, LocaleResourceType.Error, code, content);
        }
    }
}
