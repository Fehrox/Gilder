using Fluxor;
using Presentation.Store.Transaction;

namespace Presentation.Store.Insights;

public class InsightsCollateEffect : Effect<InsightsCollateCommand>
{
    private readonly IState<TransactionsState> _transactionState;

    public InsightsCollateEffect(IState<TransactionsState> transactionState) 
        => _transactionState = transactionState;

    public override Task HandleAsync(InsightsCollateCommand action, IDispatcher dispatcher)
    {
        var insights = GetMonthInsights();
        var maxMonthNet = insights.MaxMonthNet;
        var monthInsights = insights.InsightMonthStats;
        dispatcher.Dispatch(new InsightsSetCommand(maxMonthNet, monthInsights));
        
        return Task.CompletedTask;
    }
    
    private Insights GetMonthInsights()
    {
        var monthStats = new List<InsightMonth>();
        double maxMonthNet = 0D;
        
        var transactionsByMonth = _transactionState.Value.Transactions
            .GroupBy(g => new DateTime(g.Date.Year, g.Date.Month, 1));
        foreach (var monthTransactions in transactionsByMonth)
        {
            var groupStats = monthTransactions
                .GroupBy(t => t.Group?.Name??string.Empty)
                .Select(g => new InsightMonthGroup {
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

            monthStats.Add(new InsightMonth {
                Month = monthTransactions.Key,
                Net = net,
                Income = income,
                Spent = spent,
                GroupStats = groupStats
            });
        }
        
        return new Insights {
            MaxMonthNet = maxMonthNet,
            InsightMonthStats = monthStats,
        };
    }

    private class Insights
    {
        public double MaxMonthNet { get; set; } = 0;
        public IEnumerable<InsightMonth> InsightMonthStats { get; set; } = Array.Empty<InsightMonth>();
    }
}