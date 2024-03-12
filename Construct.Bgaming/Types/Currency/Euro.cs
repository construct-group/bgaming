using Construct.Bgaming.Types.Currency;

public class Euro : CurrencyBase
{
    public new static byte Subunits { get; } = 2;
    public new static string Code => "EUR";
}
