using System.Collections.Generic;
using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroups();
    }
}