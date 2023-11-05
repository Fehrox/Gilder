using Domain;

namespace Presentation.Store.Deductions;

public class DeductionState
{
    public List<Domain.Deduction> Deductions { get; init; } = new();
}