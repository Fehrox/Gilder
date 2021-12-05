using System.Threading.Tasks;
using Fluxor;
using Reconciler.Application;

namespace Reconciler.Store
{
    public class TransactionsLoadEffect : Effect<TransactionsLoadAction>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionGroupMapper _transactionGroupMapper;

        public TransactionsLoadEffect(
            ITransactionRepository transactionRepository,
            ITransactionGroupMapper transactionGroupMapper)
        {
            _transactionRepository = transactionRepository;
            _transactionGroupMapper = transactionGroupMapper;
        }

        public override async Task HandleAsync(TransactionsLoadAction action, IDispatcher dispatcher)
        {
            var transactions = await _transactionRepository.GetTransactions();
            
            foreach (var transaction in transactions) {
                var group = await _transactionGroupMapper.GetGroupForTransaction(transaction); 
                dispatcher.Dispatch(new TransactionAddAction(transaction, group));
            }
        }
    }
}