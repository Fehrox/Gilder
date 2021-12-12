
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
    public class TransactionNoteMapper : ITransactionNoteMapper
    {
        private static readonly string _csvPath = $"Data/{nameof(NoteTransactionMap)}.csv";
        private readonly ITransactionRepository _transactionRepository;

        public TransactionNoteMapper(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        
        public Task<string> GetNoteForTransaction(Transaction transaction)
        {
            var transactionHash = transaction.ToHash();
            var records = GetGroupTransactionMaps();
            foreach(var record in records)
                if(record.TransactionHash == transactionHash.ToString())
                    return Task.FromResult(record.Note);

            return null;
        }

        public Task UpdateTransactionNote(Hash noteHash, string note)
        {
            bool existingUpdated = false;
            var records = GetGroupTransactionMaps().ToList();
            foreach(var record in records) {
                var match = record.TransactionHash.ToString() == noteHash.ToString();
                if(!match) continue;

                record.Note = note;
                existingUpdated = true;
            }            

            if(!existingUpdated)
                records.Add(new NoteTransactionMap {
                    Note = note,
                    TransactionHash = noteHash.ToString()
                });

            using (var writer = new StreamWriter(_csvPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                csv.WriteRecords(records);

            return Task.CompletedTask;
        }

        private static IEnumerable<NoteTransactionMap> GetGroupTransactionMaps()
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<NoteTransactionMap>();

            System.Console.WriteLine(records);
            
            return records.ToArray();
        }
    }
}