namespace DDD.Domain.Common.Entities;

public record Currency
{
    private List<string> _symbols = new List<string>() { "AUD", "NZD", "GBP", "USD", "EUR", "CAD" };

    public string Symbol { get; }

    public Currency(string symbol)
    {
        if (!_symbols.Contains(symbol.ToUpper()))
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

    public static List<Currency> Currencies => new()
    {
        AUD, NZD, USD, GBP, EUR, CAD
    };
}