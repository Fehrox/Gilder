using Application;
using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetRepoUpdateEffect : Effect<BudgetRepoUpdateAction>
{
    private readonly IRepo<Domain.Budget> _budgetRepo;

    public BudgetRepoUpdateEffect(IRepo<Domain.Budget> budgetRepo) => _budgetRepo = budgetRepo;

    public override async Task HandleAsync(BudgetRepoUpdateAction action, IDispatcher dispatcher)
        => await _budgetRepo.Update(action.Budget);
}