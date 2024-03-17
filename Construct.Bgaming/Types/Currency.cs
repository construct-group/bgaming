using Newtonsoft.Json.Linq;

namespace Construct.Bgaming.Types;

public class Currency
{
    public Currency(double amount, byte subunits, string code)
    {
        this.Subunits = subunits;
        this.Amount = Convert.ToInt64(amount * Math.Pow(10, subunits));
        this.Code = code;
    }

    public Currency(long amount, string code)
    {
        this.Subunits = code switch
        {
            "EUR" => 2,
            "USD" => 2,
            "JPY" => 0,
            "BTC" => 8,
            "LTC" => 8,
            "DOG" => 8,
            "ETH" => 9,
            "BCH" => 8,
            "XRP" => 6,
            "USDT" => 8,
            _ => throw new ArgumentException()
        };
        this.Amount = amount;
        this.Code = code;
    }

    public long Amount { get; set; }
    public byte Subunits { get; }
    public string Code { get; } = null!;

    public double GetAmount() => Convert.ToDouble(Amount) / Math.Pow(10, Subunits);

    public static Currency Euro(double amount) => new(amount, 2, "EUR");
    public static Currency Dollar(double amount) => new(amount, 2, "USD");
    public static Currency Japan(double amount) => new(amount, 0, "JPY");
    public static Currency Bitcoin(double amount) => new(amount, 8, "BTC");
    public static Currency Litecoin(double amount) => new(amount, 8, "LTC");
    public static Currency Dogecoin(double amount) => new(amount, 8, "DOG");
    public static Currency Etherium(double amount) => new(amount, 9, "ETH");
    public static Currency BitcoinCash(double amount) => new(amount, 8, "BCH");
    public static Currency Tether(double amount) => new(amount, 8, "USDT");
    public static Currency Ripple(double amount) => new(amount, 6, "XRP");
    public static Currency GetCurrency(double amount, string name)
    {
        return name switch
        {
            "EUR" => Euro(amount),
            "USD" => Dollar(amount),
            "JPY" => Japan(amount),
            "BTC" => Bitcoin(amount),
            "LTC" => Litecoin(amount),
            "DOG" => Dogecoin(amount),
            "ETH" => Etherium(amount),
            "BCH" => BitcoinCash(amount),
            "XRP" => Ripple(amount),
            "USDT" => Tether(amount),
            _ => throw new ArgumentException()
        };
    }
}