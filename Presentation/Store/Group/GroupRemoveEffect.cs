using Application;
using Fluxor;

namespace Presentation.Store.Group;

public class GroupRemoveEffect : Effect<GroupRemoveAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupRemoveEffect(IRepo<Domain.Group> groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupRemoveAction action, IDispatcher dispatcher)
    {
        await _groupRepository.Remove(action.GroupsToRemove);
    }
}