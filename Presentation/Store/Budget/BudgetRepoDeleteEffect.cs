using Application;
using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetRepoDeleteEffect : Effect<BudgetRepoDeleteAction>
{
    private readonly IRepo<Domain.Budget> _budgetRepo;

    public BudgetRepoDeleteEffect(IRepo<Domain.Budget> budgetRepo) => _budgetRepo = budgetRepo;

    public override async Task HandleAsync(BudgetRepoDeleteAction action, IDispatcher dispatcher)
        => await _budgetRepo.Remove(action.Budget);
}