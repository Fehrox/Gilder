using Application;
using Domain;
using Fluxor;
using Presentation.Store.Group;

namespace Presentation.Store.Budget;

public class BudgetRepoLoadEffect : Effect<BudgetRepoLoadAction>
{
    private readonly IRepo<Domain.Budget> _budgetRepo;
    public BudgetRepoLoadEffect(IRepo<Domain.Budget> budgetRepo) => _budgetRepo = budgetRepo;

    public override async Task HandleAsync(BudgetRepoLoadAction action, IDispatcher dispatcher)
    {
        var budgets = await _budgetRepo.Read();
        dispatcher.Dispatch(new BudgetCreateAction(budgets));
    }
}