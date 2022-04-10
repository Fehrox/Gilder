using Application;
using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetRepoCreateEffect : Effect<BudgetRepoCreateAction>
{
    private readonly IRepo<Domain.Budget> _budgetRepo;

    public BudgetRepoCreateEffect(IRepo<Domain.Budget> budgetRepo) => _budgetRepo = budgetRepo;

    public override async Task HandleAsync(BudgetRepoCreateAction action, IDispatcher dispatcher) 
        => await _budgetRepo.Create(action.Budgets);
}