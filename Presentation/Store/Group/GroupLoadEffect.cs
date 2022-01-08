using Application;
using Fluxor;

namespace Presentation.Store;

public class GroupLoadEffect : Effect<GroupLoadAction>
{
    private readonly IGroupRepository _groupRepository;

    public GroupLoadEffect(IGroupRepository groupRepository) =>
        _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupLoadAction action, IDispatcher dispatcher)
    {
        var groups = await _groupRepository.ReadGroups();
        dispatcher.Dispatch(new GroupRestoreAction(groups));
    }
}