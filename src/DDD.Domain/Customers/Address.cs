namespace DDD.Domain.Customers;

public record Address
{
    public string Line1 { get; }
    public string? Line2 { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(string line1, string? line2, string city, string state, string zipCode, string country)
    {
        Guard.Against.Empty(line1);
        Guard.Against.Empty(city);
        Guard.Against.Empty(state);
        Guard.Against.Empty(zipCode);
        Guard.Against.Empty(country);

        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
}
