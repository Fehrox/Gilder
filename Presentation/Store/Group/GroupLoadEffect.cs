using Application;
using Fluxor;

namespace Presentation.Store.Group;

public class GroupLoadEffect : Effect<GroupLoadAction>
{
    private readonly IRepo<Domain.Group> _groupRepository;

    public GroupLoadEffect(IRepo<Domain.Group> groupRepository) =>
        _groupRepository = groupRepository;

    public override async Task HandleAsync(GroupLoadAction action, IDispatcher dispatcher)
    {
        var groups = await _groupRepository.Read();
        dispatcher.Dispatch(new GroupRestoreAction(groups));
    }
}