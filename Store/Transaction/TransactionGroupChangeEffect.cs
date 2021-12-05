using System.Threading.Tasks;
using System.Transactions;
using Fluxor;
using Reconciler.Application;

namespace Reconciler.Store
{
    public class TransactionGroupChangeEffect : Effect<TransactionGroupChangeAction>
    {
        private readonly ITransactionGroupMapper _transactionGroupMapper;
        private readonly IGroupRepository _groupRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionGroupChangeEffect(
            ITransactionGroupMapper transactionGroupMapper,
            IGroupRepository groupRepository,
            ITransactionRepository transactionRepository)
        {
            _transactionGroupMapper = transactionGroupMapper;
            _groupRepository = groupRepository;
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionGroupChangeAction action, IDispatcher dispatcher)
        {
            await _transactionGroupMapper
                .UpdateTransactionGroup(action.TransactionHash, action.GroupHash);
            var transaction = await _transactionRepository.GetTransactionByHash(action.TransactionHash);
            var group = await _groupRepository.GetGroupByHash(action.GroupHash);
            dispatcher.Dispatch(new TransactionUpdateAction(transaction, group));
        }
    }
}