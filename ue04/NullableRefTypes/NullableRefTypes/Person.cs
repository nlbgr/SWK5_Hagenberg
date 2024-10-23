using System.Diagnostics.CodeAnalysis;

public class Person(string lastName, string? firstName = null) {
    public string? FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName ?? throw new ArgumentNullException(nameof(lastName));

    private Address? address;

    [DisallowNull]
    public Address? Address {
        get => address;
        set => address = value ?? throw new ArgumentNullException(nameof(address));
    }
}

public record Address(int zipCode, string City);