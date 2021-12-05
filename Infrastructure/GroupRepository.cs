using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Reconciler.Application;
using Reconciler.Domain;

namespace Reconciler.Infrastructure
{
    public class GroupRepository : IGroupRepository
    {
        public Task<IEnumerable<Group>> GetGroups()
        {
            using var reader = new StreamReader($"Data/{nameof(Group)}.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Group>();
            var recordsArray = records.ToArray();

            return Task.FromResult(recordsArray.AsEnumerable());
        }
    }
}