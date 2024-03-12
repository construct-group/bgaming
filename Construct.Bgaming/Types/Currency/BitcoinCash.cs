using Construct.Bgaming.Types.Currency;

public class BitcoinCash : CurrencyBase
{
    public new static byte Subunits { get; } = 8;
    public new static string Code => "BCH";
}
