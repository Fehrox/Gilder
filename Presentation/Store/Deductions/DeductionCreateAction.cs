namespace Presentation.Store.Deductions;

public class DeductionCreateAction
{
    public DeductionCreateAction(params Domain.Deduction[] deductions) =>
        Deductions = deductions;

    public IEnumerable<Domain.Deduction> Deductions { get; }
}