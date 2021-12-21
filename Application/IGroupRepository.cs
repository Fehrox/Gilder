
using Domain;

namespace Application
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> ReadGroups();
        Task<Group> ReadGroup(Guid id);
    }
}
