using Application;
using Fluxor;

namespace Presentation.Store.Group;

public class GroupCreateEffect : Effect<GroupCreateAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupCreateEffect(IRepo<Domain.Group> groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupCreateAction action, IDispatcher dispatcher) =>
        await _groupRepository.Create(action.Groups);
}