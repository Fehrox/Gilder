using System;
using System.Threading.Tasks;
using Fluxor;
using Reconciler.Application;
using Reconciler.Infrastructure;

namespace Reconciler.Store
{
    public class TransactionsLoadEffect : Effect<TransactionsLoadAction>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionGroupMapper _transactionGroupMapper;
        private readonly ITransactionNoteMapper _transactionNoteMapper;

        public TransactionsLoadEffect(
            ITransactionRepository transactionRepository,
            ITransactionGroupMapper transactionGroupMapper,
            ITransactionNoteMapper transactionNoteMapper
        ) {
            _transactionRepository = transactionRepository;
            _transactionGroupMapper = transactionGroupMapper;
            _transactionNoteMapper =  transactionNoteMapper;
        }

        public override async Task HandleAsync(TransactionsLoadAction action, IDispatcher dispatcher)
        {
            var transactions = await _transactionRepository.GetTransactions();
            
            foreach (var transaction in transactions) {
                var group = await _transactionGroupMapper.GetGroupForTransaction(transaction);
                var note = await _transactionNoteMapper.GetNoteForTransaction(transaction);
                
                dispatcher.Dispatch(new TransactionAddAction(transaction, group, note));
            }
        }
    }
}