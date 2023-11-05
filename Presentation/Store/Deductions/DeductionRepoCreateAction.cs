using Domain;

namespace Presentation.Store.Deductions;

public record DeductionRepoCreateAction(params Deduction[] Deductions);