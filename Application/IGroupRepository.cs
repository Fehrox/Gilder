using Reconciler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reconciler.Application
{
    interface IGroupRepository
    {
        Task<IEnumerable<Group>> ReadGroups();
        Task<Group> ReadGroup(Guid id);
    }
}
