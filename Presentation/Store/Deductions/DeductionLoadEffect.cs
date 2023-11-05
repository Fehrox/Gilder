using Application;
using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionLoadEffect : Effect<DeductionLoadAction>
{
    private readonly IRepo<Domain.Deduction> _deductionRepository;

    public DeductionLoadEffect(IRepo<Domain.Deduction> groupRepository) =>
        _deductionRepository = groupRepository;

    public override async Task HandleAsync(DeductionLoadAction action, IDispatcher dispatcher)
    {
        var deductions = await _deductionRepository.Read();
        foreach (var loadedDeduction in deductions)
        {
            Console.WriteLine(loadedDeduction.Id.ToString());
        }
        dispatcher.Dispatch(new DeductionLoadedAction(deductions.ToArray()));
    }
}