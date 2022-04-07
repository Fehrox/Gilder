using Application;
using Fluxor;

namespace Presentation.Store.Group;

public class GroupRepoLoadEffect : Effect<GroupRepoLoadAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupRepoLoadEffect(IRepo<Domain.Group> groupRepository) =>
        _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupRepoLoadAction loadAction, IDispatcher dispatcher)
    {
        var groups = await _groupRepository.Read();
        dispatcher.Dispatch(new GroupCreateAction(groups));
    }
}