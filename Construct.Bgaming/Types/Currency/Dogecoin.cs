using Construct.Bgaming.Types.Currency;

public class Dogecoin : CurrencyBase
{
    public new static byte Subunits { get; } = 8;
    public new static string Code => "DOG";
}
