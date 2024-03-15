namespace Viriplaca.HR.Domain.People;

public abstract class Person : AggregateRoot
{
    protected Person()
    {
    }

    protected Person(string firstName, string? lastName, DateOnly birthDate, Sex sex, MaritalStatus maritalStatus)
    {
        firstName.MustNotBeNullOrWhiteSpace();
        birthDate.MustBeOnOrBeforeNow();
        sex.MustBeDefined();
        maritalStatus.MustBeDefined();

        FirstName = firstName.Trim();
        LastName = lastName?.Trim();
        BirthDate = birthDate;
        Sex = sex;
        MaritalStatus = maritalStatus;
    }

    public string FirstName { get; private set; } = string.Empty;

    public string? LastName { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Sex Sex { get; private set; }

    public MaritalStatus MaritalStatus { get; private set; }

    public Guid? UserId { get; private set; }

    public void UpdateFirstName(string firstName)
    {
        firstName.MustNotBeNullOrWhiteSpace();
        FirstName = firstName.Trim();
    }

    public void UpdateLastName(string? lastName)
    {
        LastName = lastName?.Trim();
    }

    public void UpdateBirthDate(DateOnly birthDate)
    {
        birthDate.MustBeOnOrBeforeNow();
        BirthDate = birthDate;
    }

    public void UpdateSex(Sex sex)
    {
        sex.MustBeDefined();
        Sex = sex;
    }

    public void UpdateMaritalStatus(MaritalStatus maritalStatus)
    {
        maritalStatus.MustBeDefined();
        MaritalStatus = maritalStatus;
    }

    public void BindUser(Guid userId)
    {
        if (UserId.HasValue)
        {
            throw new DomainException("User has already bound");
        }

        UserId = userId;
    }
}
