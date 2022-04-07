namespace Presentation.Store.Budget;

public record BudgetState
{
    private IEnumerable<Domain.Budget> Budgets { get; set; } = Array.Empty<Domain.Budget>();
}