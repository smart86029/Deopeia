using Bogus;
using Viriplaca.Common.Extensions;
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

        await context.SaveChangesAsync();
    }

    private List<Employee> GetEmployees()
    {
        var results = new Faker<Employee>()
            .CustomInstantiator(x =>
            {
                var gender = x.PickRandom(Gender.Male, Gender.Female);
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
}
