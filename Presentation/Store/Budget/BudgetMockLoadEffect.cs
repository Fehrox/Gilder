using Domain;
using Fluxor;
using Presentation.Store.Group;

namespace Presentation.Store.Budget;

public class BudgetMockLoadEffect : Effect<BudgetMockLoadAction>
{
    private readonly IState<GroupState> _groupState;

    public BudgetMockLoadEffect(IState<GroupState> groupState) => _groupState = groupState;

    public override Task HandleAsync(BudgetMockLoadAction action, IDispatcher dispatcher)
    {
        var groups = _groupState.Value.Groups;

        dispatcher.Dispatch(new BudgetClearAction());
        Domain.Group? foodGroup =  groups.FirstOrDefault(g => g.Name == "Food");
        dispatcher.Dispatch(new BudgetCreateAction(new [] {
            new Domain.Budget() {
                Description = "Grocery Shopping",
                Group = foodGroup,
                Id = Guid.NewGuid(),
                Intervals = new [] {
                    new BudgetInterval{ From = new DateTime(2022, 4, 10), Delta = 60, Interval = TimeSpan.FromDays(7) }
                }
            },
            new Domain.Budget() {
                Description = "F45",
                Group = foodGroup,
                Id = Guid.NewGuid(),
                Intervals = new [] {
                    new BudgetInterval{ From = new DateTime(2022, 4, 10), Delta = 60, Interval = TimeSpan.FromDays(7) }
                }
            }
        }));
        
        return Task.CompletedTask;
    }
}