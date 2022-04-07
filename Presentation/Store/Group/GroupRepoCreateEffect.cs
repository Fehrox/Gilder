using Application;
using Fluxor;

namespace Presentation.Store.Group;

public class GroupRepoCreateEffect : Effect<GroupRepoCreateAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupRepoCreateEffect(IRepo<Domain.Group> groupRepository)
        => _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupRepoCreateAction action, IDispatcher dispatcher) =>
        await _groupRepository.Create(action.Groups);
}