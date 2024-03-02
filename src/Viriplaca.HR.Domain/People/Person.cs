namespace Viriplaca.HR.Domain.People;

public abstract class Person : AggregateRoot
{
    protected Person()
    {
    }

    protected Person(string firstName, string? lastName, DateOnly birthDate, Gender gender, MaritalStatus maritalStatus)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainException("First name can not bet null");
        }

        if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainException("Birth date must less than now");
        }

        FirstName = firstName.Trim();
        LastName = lastName?.Trim();
        BirthDate = birthDate;
        Gender = gender;
        MaritalStatus = maritalStatus;
    }

    public string FirstName { get; private set; } = string.Empty;

    public string? LastName { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public MaritalStatus MaritalStatus { get; private set; }

    public Guid? UserId { get; private set; }

    public void UpdateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainException("First name can not be null");
        }

        FirstName = firstName.Trim();
    }

    public void UpdateLastName(string lastName)
    {
        LastName = lastName?.Trim();
    }

    public void UpdateBirthDate(DateOnly birthDate)
    {
        if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainException("Birth date must less than now");
        }

        BirthDate = birthDate;
    }

    public void UpdateGender(Gender gender)
    {
        Gender = gender;
    }

    public void UpdateMaritalStatus(MaritalStatus maritalStatus)
    {
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
