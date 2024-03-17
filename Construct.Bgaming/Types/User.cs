namespace Construct.Bgaming.Types;

public record User
{
    public Guid Id { get; init; }
    public string? Email { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Nickname { get; init; } = null!;
    public string City { get; init; } = null!;
    public string Country { get; init; } = null!;
    public DateOnly DateOfBirth { get; init; }
    public DateOnly DateOfRegistration { get; init; }
    public Gender Gender { get; init; }
}
