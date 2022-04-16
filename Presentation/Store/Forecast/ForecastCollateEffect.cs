using Fluxor;
using Presentation.Store.Budget;

namespace Presentation.Store.Forecast;

public class ForecastCollateEffect : Effect<ForecastCollateCommand>
{
    private readonly IState<ForecastState> _forecastState;
    private readonly IState<BudgetState> _budgetState;

    public ForecastCollateEffect(
        IState<ForecastState> forecastState,
        IState<BudgetState> budgetState)
    {
        _forecastState = forecastState;
        _budgetState = budgetState;
    }

    public override Task HandleAsync(ForecastCollateCommand action, IDispatcher dispatcher)
    {
        var transactions = new List<ForecastTransaction>();

        var forecastEnd = _forecastState.Value.Till;
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



        dispatcher.Dispatch(new ForecastSetTransactionsCommand(transactions));

        return Task.CompletedTask;
    }
    
}
