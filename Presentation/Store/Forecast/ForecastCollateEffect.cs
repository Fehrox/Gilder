using Fluxor;
using Presentation.Store.Budget;

namespace Presentation.Store.Forecast;

public class ForecastCollateEffect : Effect<ForecastCollateCommand>
{
    private const double DAYS_IN_YEAR = 365.242199;
    private const double FORECAST_DAYS = 3 * DAYS_IN_YEAR;
    private readonly IState<BudgetState> _budgetState;

    public ForecastCollateEffect(IState<BudgetState> budgetState) => _budgetState = budgetState;

    public override Task HandleAsync(ForecastCollateCommand action, IDispatcher dispatcher)
    {
        var transactions = new List<ForecastTransaction>();

        var forecastEnd = DateTime.Now + TimeSpan.FromDays(FORECAST_DAYS);
        var budgets = _budgetState.Value.Budgets;
        foreach (var budget in budgets) {
            foreach (var interval in budget.Intervals) {
                if(interval.Suppress)
                    continue;
                
                if (interval.Interval == null) {
                    var forecastTransaction = new ForecastTransaction {
                        Charge = interval.Delta,
                        Date = interval.From,
                        Group = budget.Group,
                        Details = budget.Description };
                    transactions.Add(forecastTransaction);
                    continue;
                }

                var simDate = interval.From;
                while (simDate < forecastEnd && simDate < (interval.To ?? forecastEnd)) {
                    var forecastTransaction = new ForecastTransaction {
                        Charge = interval.Delta,
                        Date = simDate,
                        Group = budget.Group,
                        Details = budget.Description };
                    transactions.Add(forecastTransaction);
                    simDate += interval.Interval.Value;
                }
            }   
        }

        // var forecast = new List<MonthForecast>();
        // var transactionsByMonth = transactions
        //     .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1));
        // double lastMonthNet = 0;
        // foreach (var monthTransactions in transactionsByMonth) {
        //     var thisMonthNet = monthTransactions.Sum(t => t.Charge);
        //     forecast.Add( new MonthForecast {
        //         Month = monthTransactions.Key,
        //         NetPosition = lastMonthNet += thisMonthNet
        //     });
        // }

        dispatcher.Dispatch(new ForecastSetCommand(transactions));

        return Task.CompletedTask;
    }




}
//
// public static class EnumExtensions {
//     public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
//         this IEnumerable<TSource> source, 
//         Func<TSource, TSource, TResult> projection)
//     {
//         using (var iterator = source.GetEnumerator())
//         {
//             if (!iterator.MoveNext())
//             {
//                 yield break;
//             }
//             TSource previous = iterator.Current;
//             while (iterator.MoveNext())
//             {
//                 yield return projection(previous, iterator.Current);
//                 previous = iterator.Current;
//             }
//         }
//     }
// }