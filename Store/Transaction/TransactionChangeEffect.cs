using Fluxor;
using System.Threading.Tasks;
using Reconciler.Application;

namespace Reconciler.Store
{
    public class TransactionNoteChangeEffect : Effect<TransactionNoteChangeAction>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ITransactionGroupMapper _transactionGroupMapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionNoteMapper _transactionNoteMapper;

        public TransactionNoteChangeEffect(
            IGroupRepository groupRepository,
            ITransactionGroupMapper transactionGroupMapper,
            ITransactionRepository transactionRepository,
            ITransactionNoteMapper transactionNoteMapper)
        {
            _groupRepository = groupRepository;
            _transactionRepository = transactionRepository;
            _transactionGroupMapper = transactionGroupMapper;
            _transactionNoteMapper = transactionNoteMapper;
        }

        public override async Task HandleAsync(
            TransactionNoteChangeAction action,
            IDispatcher dispatcher)
        {
            // await _transactionGroupMapper
            //     .UpdateTransactionGroup(action.TransactionHash, action.GroupHash);
            await _transactionNoteMapper
                .UpdateTransactionNote(action.TransactionHash, action.Note);
            var transaction = await _transactionRepository.GetTransactionByHash(action.TransactionHash);
            var group = await _groupRepository.GetGroupByHash(action.TransactionHash);
            dispatcher.Dispatch(new TransactionUpdateAction(transaction, group, action.Note));
        }
    }
}