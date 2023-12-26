using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionToggleEffect : Effect<DeductionToggleAction>
{
    private readonly IRepo<Deduction> _deductionRepo;

    public DeductionToggleEffect(IRepo<Deduction> deductionRepo) => 
        _deductionRepo = deductionRepo;

    public override async Task HandleAsync(DeductionToggleAction action, IDispatcher dispatcher)
    {
        var deductions = await _deductionRepo.Read();
        var deduction = deductions.FirstOrDefault(d => d.TransactionId == action.Transaction.Id);
        if (deduction == null) {
            await _deductionRepo.Create(new Deduction {
                TransactionId = action.Transaction.Id,
                Id = Guid.NewGuid()
            });   
        } else {
            await _deductionRepo.Delete(deduction);
        }
    }
}