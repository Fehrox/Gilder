using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionUpdateEffect : Effect<DeductionUpdateAction>
{
    private readonly IRepo<Deduction> _deductionRepo;

    public DeductionUpdateEffect(IRepo<Deduction> deductionRepo) => 
        _deductionRepo = deductionRepo;

    public override async Task HandleAsync(DeductionUpdateAction action, IDispatcher dispatcher)
    {
        // var deductionId = action.Deduction.Id;
        // var deduction = await _deductionRepo.Read(deductionId);
        // if (deduction != null) {
        //     deduction.Reason = action.Deduction.Reason;
            await _deductionRepo.Update(action.Deduction);
        // }
    }
}