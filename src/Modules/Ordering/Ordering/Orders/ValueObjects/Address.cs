namespace Ordering.Orders.ValueObjects;

public record Address
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? EmailAddress { get; set; } = default!;

    public string AddressLine { get; set; } = default!;

    public string Country { get; set; } = default!;

    public string ZipCode { get; set; } = default!;

    protected Address()
    {
    }

    private Address(string firstName, string lastName, string? emailAddress, string addressLine, string country, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string? emailAddress, string addressLine, string country, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new(firstName, lastName, emailAddress, addressLine, country, zipCode);
    }
}
