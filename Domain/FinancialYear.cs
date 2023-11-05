namespace Domain;

public class FinancialYear
{
    public static string GetFinancialYear(DateTime date)
    {
        // Determine the financial year starting year
        int startYear;
        if (date.Month < 7)
        {
            // If the month is before July, the financial year started last calendar year
            startYear = date.Year - 1;
        }
        else
        {
            // If the month is July or later, the financial year started this calendar year
            startYear = date.Year;
        }

        // The financial year ends in the next calendar year
        int endYear = startYear + 1;

        // Format the financial year as "YYYY-YYYY"
        return $"{startYear}-{endYear}";
    }

    public static void GetSpan(string actionFinancialYear, out DateTime startDate, out DateTime endDate)
    {
        // Parse the financial year start and end years
        var years = actionFinancialYear.Split('-');
        if (years.Length != 2 || !int.TryParse(years[0], out int startYear) || !int.TryParse(years[1], out int endYear))
            throw new ArgumentException("The financial year format should be 'YYYY-YYYY'.");

        // Assuming the financial year starts on July 1st and ends on June 30th
        startDate = new DateTime(startYear, 7, 1);
        endDate = new DateTime(endYear, 6, 30);
    }
}