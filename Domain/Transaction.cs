using System;
using System.Linq;

namespace Reconciler.Domain
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public double Charge { get; set; }
        public Classification Class { get; set; }
        public string Details { get; set; }

        public enum Classification
        {
            EftposDebit,
            MiscDebit,
            InterBankCredit,
            AtmDebit,
            TransferDebit,
            Fees,
            ReversalCredit
        }

        public Hash ToHash()
        {
            var transactionStr =
                Charge + 
                Details + 
                Date.Ticks +
                (int)Class;

            return new(transactionStr);
        }
    }
}