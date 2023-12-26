using Application;
using Fluxor;

namespace Presentation.Store.Group;

public record GroupRemoveAction(IEnumerable<Domain.Group> GroupsToRemove);

public class GroupRemoveEffect : Effect<GroupRemoveAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupRemoveEffect(IRepo<Domain.Group> groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupRemoveAction action, IDispatcher dispatcher)
    {
        await _groupRepository.Delete(action.GroupsToRemove);
    }
}

public class GroupRemoveReducer : Reducer<GroupState, GroupRemoveAction>
{
    public override GroupState Reduce(GroupState state, GroupRemoveAction action)
    {

        var groupsWithoutRemoved = state.Groups
            .Where(group => !action.GroupsToRemove.Contains(group))
            .ToList();

        return state with {
            Groups = groupsWithoutRemoved
        };
    }
}