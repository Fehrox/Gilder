using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application;
using CsvHelper;
using Domain;

namespace Gilder.Infrastructure
{
    public class NabCsvTransactionImporter : ITransactionImporter
    {
        
        public async Task<IEnumerable<Transaction>> ImportTransactions(string transactionCsv)
        {
            var csvHeaderStr = "Date,Charge,A,B,Classification,Details,Balance\n";
            using var reader = new StringReader(csvHeaderStr + transactionCsv);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvTransaction>().ToList();

            var transactions = new List<Transaction>();
            foreach (var csvTransaction in records) {
                var date = DateTime.Parse(csvTransaction.Date);
                var charge = double.Parse(csvTransaction.Charge);
                var classification = ParseClassification(csvTransaction.Classification);
                var details = csvTransaction.Details;

                var transaction = new Transaction {
                    Charge = charge,
                    Date = date,
                    Class = classification,
                    Details = details
                };
                
                transactions.Add(transaction);
            }
            
            return transactions.AsEnumerable();
        }

        // public async Task<Transaction> GetTransactionByHash(Hash transactionHash)
        // {
        //     var transactions = await ImportTransactions();
        //     var transactionForHash = transactions
        //         .FirstOrDefault(t => t.ToHash().ToString() == transactionHash.ToString());
        //
        //     return transactionForHash;
        // }

        private Transaction.Classification ParseClassification(string transaction)
        {
            switch (transaction) {
                case "MISCELLANEOUS DEBIT": return Transaction.Classification.MiscDebit;
                case "EFTPOS DEBIT": return Transaction.Classification.EftposDebit;
                case "ATM DEBIT" : return Transaction.Classification.AtmDebit;
                case "INTER-BANK CREDIT" : return Transaction.Classification.InterBankCredit;
                case "TRANSFER DEBIT" : return Transaction.Classification.TransferDebit;
                case "TRANSFER CREDIT": return Transaction.Classification.TransferCredit;
                case "FEES": return Transaction.Classification.Fees;
                case "REVERSAL CREDIT" : return Transaction.Classification.ReversalCredit;
                case "AUTOMATIC DRAWING" : return Transaction.Classification.AutomaticDrawing;
                case "INTEREST PAID" : return Transaction.Classification.InterestPaid;
                case "MISCELLANEOUS CREDIT": return Transaction.Classification.MiscCredit;
                default: throw new FormatException($"{transaction} is not a know transaction type.");
            }
        }

        private class CsvTransaction
        {
            public string Date { get; set; }
            public string Charge { get; set; }
            public string Classification { get; set; }
            public string Details { get; set; }
        }
    }
}