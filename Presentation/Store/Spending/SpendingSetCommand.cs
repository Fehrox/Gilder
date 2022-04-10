namespace Presentation.Store.Spending;

public record SpendingSetCommand(double MaxMonthNet, IEnumerable<SpendingMonth> SpendingMonths);