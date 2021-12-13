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
        private readonly ITransactionNoteMapper _noteMapper;

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
            var groupHash = action.GroupHash;
            var transactionHash = action.TransactionHash;

            await _transactionGroupMapper
                .UpdateTransactionGroup(transactionHash, groupHash);
            var transaction = await _transactionRepository
                .GetTransactionByHash(transactionHash);
            var note = await _noteMapper
                .GetNoteForTransaction(transactionHash);

            var group = await _groupRepository.GetGroupByHash(groupHash);
            dispatcher.Dispatch(new TransactionUpdateAction(transaction, group, note));
        }
    }
}