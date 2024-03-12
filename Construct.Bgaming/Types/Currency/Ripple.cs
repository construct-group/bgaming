using Construct.Bgaming.Types.Currency;

public class Ripple : CurrencyBase
{
    public new static byte Subunits { get; } = 6;
    public new static string Code => "XRP";
}
