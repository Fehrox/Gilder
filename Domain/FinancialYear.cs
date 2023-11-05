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
}