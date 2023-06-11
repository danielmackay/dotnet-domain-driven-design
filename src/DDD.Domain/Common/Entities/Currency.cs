namespace DDD.Domain.Common.Entities;

public record Currency
{

    public string Symbol { get; }

    public Currency(string symbol)
    {
        if (!Symbols.Contains(symbol.ToUpper()))
            throw new DomainException($"Invalid Symbol {symbol}");

        Symbol = symbol.ToUpper();
    }

    public static Currency Default => AUD;
    public static Currency AUD => new Currency("AUD");
    public static Currency NZD => new Currency("NZD");
    public static Currency USD => new Currency("USD");
    public static Currency GBP => new Currency("GBP");
    public static Currency EUR => new Currency("EUR");
    public static Currency CAD => new Currency("CAD");

    private static readonly List<string> Symbols = new List<string>() { "AUD", "NZD", "GBP", "USD", "EUR", "CAD" };

    public static List<Currency> Currencies => Symbols.Select(s => new Currency(s)).ToList();
}