namespace Construct.Bgaming.Types.Currency;

public class CurrencyBase
{
    protected double value;

    public static byte Subunits { get; }

    public static string Code { get; } = null!;

    public static implicit operator CurrencyBase(double value) => new() { value = value };

    public static implicit operator CurrencyBase(string value) => new() { value = Convert.ToDouble(value) / Math.Pow(10, Subunits) };

    public static implicit operator double(CurrencyBase value) => value.value;

    public static implicit operator string(CurrencyBase value) => value.value.ToString();

    public override string ToString() => Convert.ToString(value * Math.Pow(10, Subunits));
}