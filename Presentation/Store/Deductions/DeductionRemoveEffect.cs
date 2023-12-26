using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionRemoveEffect : Effect<DeductionRemoveAction>
{
    private readonly IRepo<Deduction> _deductionRepo;

    public DeductionRemoveEffect(IRepo<Deduction> deductionRepo) => _deductionRepo = deductionRepo;

    public override async Task HandleAsync(DeductionRemoveAction action, IDispatcher dispatcher) => 
        await _deductionRepo.Delete(action.Deduction);
}