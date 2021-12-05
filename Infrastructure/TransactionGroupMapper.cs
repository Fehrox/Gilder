using System;
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
    public class TransactionGroupMapper : ITransactionGroupMapper
    {
        private readonly IGroupRepository _groupRepository;
        private static readonly string _csvPath = $"Data/{nameof(GroupTransactionMap)}.csv";

        public TransactionGroupMapper(
            ITransactionRepository transactionRepository,
            IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> GetGroupForTransaction(Transaction transaction)
        {
            var records = GetGroupTransactionMaps();
            foreach (var map in records) {
                if (map.TransactionHash == transaction.ToHash().ToString()) {
                    var groups = await _groupRepository.GetGroups();
                    foreach (var group in groups) {
                        if (map.GroupHash == group.ToHash().ToString()) {
                            return group;
                        }
                    }
                }
            }

            return null;
        }

        public Task UpdateTransactionGroup(Hash transaction, Hash group)
        {
            bool existingUpdated = false;
            var maps = GetGroupTransactionMaps().ToList();
            foreach (var map in maps) {
                var isMatchingTransaction = map.TransactionHash == transaction.ToString();
                if (!isMatchingTransaction) continue;
                map.GroupHash = group.ToString();
                existingUpdated = true;
            }
            
            if(!existingUpdated)
                maps.Add(new GroupTransactionMap {
                    GroupHash = group.ToString(),
                    TransactionHash = transaction.ToString()
                });
            
            using (var writer = new StreamWriter(_csvPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(maps);
            }
            
            return Task.CompletedTask;
        }

        private static IEnumerable<GroupTransactionMap> GetGroupTransactionMaps()
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<GroupTransactionMap>();
            
            return records.ToArray();
        }
    }
}