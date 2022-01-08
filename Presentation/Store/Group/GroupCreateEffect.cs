using Application;
using Fluxor;

namespace Presentation.Store;

public class GroupCreateEffect : Effect<GroupCreateAction>
{
    private readonly IGroupRepository _groupRepository;

    public GroupCreateEffect(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupCreateAction action, IDispatcher dispatcher) =>
        await _groupRepository.Create(action.Groups);
}