using Fluxor;
using Presentation.Store.Transaction;

namespace Presentation.Store.Spending;

public class SpendingCollateEffect : Effect<SpendingCollateCommand>
{
    private readonly IState<TransactionState> _transactionState;

    public SpendingCollateEffect(IState<TransactionState> transactionState) 
        => _transactionState = transactionState;

    public override Task HandleAsync(SpendingCollateCommand action, IDispatcher dispatcher)
    {
        var spendings = GetMonthSpendings();
        var maxMonthNet = spendings.MaxMonthNet;
        var monthSpendings = spendings.InsightMonthStats;
        dispatcher.Dispatch(new SpendingSetCommand(maxMonthNet, monthSpendings));
        
        return Task.CompletedTask;
    }
    
    private Spendings GetMonthSpendings()
    {
        var monthStats = new List<SpendingMonth>();
        double maxMonthNet = 0D;
        
        var transactionsByMonth = _transactionState.Value.Transactions
            .GroupBy(g => new DateTime(g.Date.Year, g.Date.Month, 1));
        foreach (var monthTransactions in transactionsByMonth)
        {
            var groupStats = monthTransactions
                .GroupBy(t => t.Group?.Name??string.Empty)
                .Select(g => new SpendingMonthGroup {
                    Group = g.First()?.Group,
                    TransactionCount = g.Count(),
                    Delta = g.Sum(t => t.Charge),
                    Month = monthTransactions.Key
                }).OrderBy(t => t.Delta)
                .ToArray();
            
            double net = 0, income = 0, spent = 0, maxDelta = 0;
            foreach (var monthTransactionBar in groupStats) {
                var delta = monthTransactionBar.Delta;
                if (delta > 0)
                    income += delta;
                else
                    spent += delta;

                var absDelta = Math.Abs(delta);
                if (absDelta > maxDelta)
                    maxDelta = absDelta;
            
                net += delta;
            }

            if (maxDelta > maxMonthNet)
                maxMonthNet = maxDelta;

            monthStats.Add(new SpendingMonth {
                Month = monthTransactions.Key,
                Net = net,
                Income = income,
                Spent = spent,
                GroupStats = groupStats
            });
        }
        
        return new Spendings {
            MaxMonthNet = maxMonthNet,
            InsightMonthStats = monthStats,
        };
    }

    private class Spendings
    {
        public double MaxMonthNet { get; set; } = 0;
        public IEnumerable<SpendingMonth> InsightMonthStats { get; set; } = Array.Empty<SpendingMonth>();
    }
}