using System.Globalization;

namespace Domain;

public static class DoubleExtensions
{
    public static string ToCurrencyString(this double value)
    {
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        return value.ToString("C", culture);
    }
}