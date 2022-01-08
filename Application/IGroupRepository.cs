
using Domain;

namespace Application
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> ReadGroups();
        Task Create(IEnumerable<Group> groups);
        Task Remove(IEnumerable<Group> groups);
    }
}
