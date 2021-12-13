using Reconciler.Domain;
using System;

namespace Reconciler.Infrastructure.Entities
{
    public class TransactionEntity { 
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Transaction.Classification Class { get; set; }
        public string Details { get; set; }

        public Guid GroupId { get; set; } 
        public GroupEntity Group { get; set; }
    }
}