using Bogus;
using Viriplaca.Common.Localization;
using Viriplaca.HR.Domain.Departments;
using Viriplaca.HR.Domain.Employees;
using Viriplaca.HR.Domain.Jobs;
using Viriplaca.HR.Domain.Leaves;
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
        var leaves = GetLeaves(employees);

        AssignJob(employees, departments, jobs);

        context.Set<Employee>().AddRange(employees);
        context.Set<Department>().AddRange(departments);
        context.Set<Job>().AddRange(jobs);
        context.Set<Leave>().AddRange(leaves);

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
                    x.PickRandom(MaritalStatus.Married, MaritalStatus.Single));

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

    private List<Leave> GetLeaves(List<Employee> employees)
    {
        var results = new Faker<Leave>()
            .CustomInstantiator(x =>
            {
                var startedAt = x.Date.RecentOffset();
                var leave = new Leave(
                    x.PickRandom<LeaveType>(),
                    startedAt,
                    startedAt.AddDays(x.Random.Int(0, 3)),
                    string.Empty,
                    x.PickRandom(employees).Id);

                return leave;
            })
            .GenerateBetween(10, 50);

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
            FromEnum(enUS, LeaveType.Other, "Other"),
            FromEnum(enUS, LeaveType.Personal, "Personal"),
            FromEnum(enUS, LeaveType.Annual, "Annual"),
            FromEnum(enUS, LeaveType.Sick, "Sick"),
            FromEnum(enUS, LeaveType.Official, "Official"),
            FromEnum(enUS, LeaveType.Menstrual, "Menstrual"),
            FromEnum(enUS, LeaveType.Marriage, "Marriage"),
            FromEnum(enUS, LeaveType.Maternity, "Maternity"),
            FromEnum(enUS, LeaveType.Paternity, "Paternity"),
            FromEnum(enUS, LeaveType.Parental, "Parental"),
            FromEnum(enUS, LeaveType.Funeral, "Funeral"),
            FromEnum(enUS, LeaveType.Compensatory, "Compensatory"),
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
            FromEnum(zhTW, LeaveType.Other, "其他"),
            FromEnum(zhTW, LeaveType.Personal, "事假"),
            FromEnum(zhTW, LeaveType.Annual, "年假"),
            FromEnum(zhTW, LeaveType.Sick, "病假"),
            FromEnum(zhTW, LeaveType.Official, "公假"),
            FromEnum(zhTW, LeaveType.Menstrual, "生理假"),
            FromEnum(zhTW, LeaveType.Marriage, "婚假"),
            FromEnum(zhTW, LeaveType.Maternity, "產嫁"),
            FromEnum(zhTW, LeaveType.Paternity, "陪產假"),
            FromEnum(zhTW, LeaveType.Parental, "育嬰假"),
            FromEnum(zhTW, LeaveType.Funeral, "喪假"),
            FromEnum(zhTW, LeaveType.Compensatory, "補休"),
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
            return new LocaleResource(culture, LocaleResourceType.Enum, $"{typeof(TEnum).Name}.{@enum:D}", content);
        }

        LocaleResource FromError(CultureInfo culture, string code, string content)
        {
            return new LocaleResource(culture, LocaleResourceType.Error, code, content);
        }
    }
}
