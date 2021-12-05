using System;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionViewModel
    {
        public Hash Hash { get; set; }
        public string Details { get; set; }
        public Transaction.Classification Class { get; set; }
        public double Charge { get; set; }
        public DateTime Date { get; set; }

        public GroupViewModel Group { get; set; }
    }

    public class GroupViewModel
    {
        public string Name { get; set; }
    }
}