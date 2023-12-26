using DocumentFormat.OpenXml.Drawing.Diagrams;
using Domain;

namespace Presentation.Store.Deductions;

public record DeductionRepoCreateAction
{
    public IEnumerable<Domain.Deduction> Deductions { get; set; }
    public DeductionRepoCreateAction(params Deduction[] deductions) => Deductions = deductions;
    public DeductionRepoCreateAction(IEnumerable<Deduction> deductions) => Deductions = deductions;

};