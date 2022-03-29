namespace Presentation.Store.Insights;

public record InsightsSetCommand(double MaxMonthNet, IEnumerable<InsightMonth> MonthInsights);