using Application;
using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionRepoCreateEffect : Effect<DeductionRepoCreateAction>
{
    private readonly IRepo<Domain.Deduction> _deductionRepo;

    public DeductionRepoCreateEffect(IRepo<Domain.Deduction> deductionRepo) => _deductionRepo = deductionRepo;

    public override async Task HandleAsync(DeductionRepoCreateAction action, IDispatcher dispatcher) => 
        await _deductionRepo.Create(action.Deductions);
}