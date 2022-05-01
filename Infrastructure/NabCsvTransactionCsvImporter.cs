using System.Globalization;
using Application;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;

namespace Infrastructure
{
    public class NabCsvTransactionCsvImporter : ITransactionCsvImporter
    {

        public async Task<IEnumerable<Transaction>> ImportTransactions(
            string transactionCsv,
            string colHeaders,
            bool firstRowIsHeading)
        {
            if (firstRowIsHeading) {
                var firstRowSkipped = transactionCsv.Split("\n").Skip(1);
                transactionCsv = String.Join("\n", firstRowSkipped);
            }

            var csvHeaderStr = String.Join(",", colHeaders) + "\n";
            using var reader = new StringReader(csvHeaderStr + transactionCsv);
            var config = new CsvConfiguration (CultureInfo.CurrentCulture) { MissingFieldFound = null};
            using var csv = new CsvReader(reader, config) ;
            var records = csv.GetRecords<CsvTransaction>().ToList();
            
            var transactions = new List<Transaction>();
            foreach (var csvTransaction in records) {
                // Manditory
                var date = DateTime.Parse(csvTransaction.Date);
                var charge = double.Parse(csvTransaction.Charge);
                var classification = ParseClassification(csvTransaction.Class);
                var details = csvTransaction.Details;
                // Optional
                var group = new Group { Name = csvTransaction.Group };
                double.TryParse(csvTransaction.Balance, out var balance );
                var merchant = csvTransaction.Merchant; 
            
                var transaction = new Transaction {
                    Charge = charge,
                    Date = date,
                    Class = classification,
                    Details = details,
                    Group = group,
                    Balance = balance,
                    Merchant = merchant
                };
                
                transactions.Add(transaction);
            }
            
            await Task.CompletedTask;
            return transactions.AsEnumerable();
        }

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
            public string Class { get; set; }
            public string Details { get; set; }
            public string Group { get; set; }
            public string Merchant { get; set; }
            
            public string Balance { get; set; }
        }
    }
}