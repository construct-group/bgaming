using System.Globalization;

namespace Construct.Bgaming.Types;

public static class DateTime
{
    public static string ToISO8601DateTime(this System.DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

    public static string ToISO8601Date(this System.DateTime dateTime) => dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    public static System.DateTime FromISO8601(this string dateTime)
    {
        if (System.DateTime.TryParseExact(dateTime, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)) return result;
        return System.DateTime.ParseExact(dateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
    }
}
