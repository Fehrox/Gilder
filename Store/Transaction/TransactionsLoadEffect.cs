using System.Threading.Tasks;
using Fluxor;
using Reconciler.Application;

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
                var transactionHash = transaction.ToHash();

                var group = await _transactionGroupMapper.GetGroupForTransaction(transactionHash);
                var note = await _transactionNoteMapper.GetNoteForTransaction(transactionHash);
                
                dispatcher.Dispatch(new TransactionAddAction(transaction, group, note));
            }
        }
    }
}