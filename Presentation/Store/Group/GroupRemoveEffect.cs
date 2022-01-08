using Application;
using Fluxor;

namespace Presentation.Store;

public class GroupRemoveEffect : Effect<GroupRemoveAction>
{
    private readonly IGroupRepository _groupRepository;

    public GroupRemoveEffect(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupRemoveAction action, IDispatcher dispatcher)
    {
        await _groupRepository.Remove(action.GroupsToRemove);
    }
}