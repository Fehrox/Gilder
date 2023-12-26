using System.Text;
using System.Text.Json;
using Application;
using Domain;
using Fluxor;
using Microsoft.JSInterop;

namespace Presentation.Store.File;

public class FileSaveEffect : Effect<FileSaveAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepo;
    private readonly IRepo<Domain.Group> _groupRepo;
    private readonly IRepo<Domain.Budget> _budgetRepo;
    private readonly IRepo<Domain.Deduction> _deductionRepo;
    private readonly IRepo<Domain.Import> _importRepo;
    private readonly IRepo<Domain.Review> _reviewRepo;
    private readonly IJSRuntime _jsRuntime;

    public FileSaveEffect(
        IRepo<Domain.Transaction> transactionRepo,
        IRepo<Domain.Group> groupRepo,
        IRepo<Domain.Budget> budgetRepo,
        IRepo<Domain.Import> importRepo,
        IRepo<Deduction> deductionRepo,
        IRepo<Review> reviewRepo,
        IJSRuntime jsRuntime)
    {
        _transactionRepo = transactionRepo;
        _groupRepo = groupRepo;
        _budgetRepo = budgetRepo;
        _deductionRepo = deductionRepo;
        _importRepo = importRepo;
        _reviewRepo = reviewRepo;
        _jsRuntime = jsRuntime;
    }
    
    public override async Task HandleAsync(FileSaveAction action, IDispatcher dispatcher)
    {
        var transactions = await _transactionRepo.Read();
        var groups = await _groupRepo.Read();
        var budgets = await _budgetRepo.Read();
        var deductions = await _deductionRepo.Read();
        var imports = await _importRepo.Read();
        var reviews = await _reviewRepo.Read();
        
        var gilderData = new DataToSave {
            Groups = groups,
            Budgets = budgets,
            Transactions = transactions,
            Imports = imports,
            Deductions = deductions,
            Reviews = reviews,
        };
        
        string gilderDataJson = JsonSerializer.Serialize(gilderData);
        var gilderDataBytes = Encoding.ASCII.GetBytes(gilderDataJson);
        var gilderDataByteStr = Convert.ToBase64String(gilderDataBytes);
        
        // Requires saveAsFile.js
        await _jsRuntime.InvokeAsync<object>(
            "saveAsFile",
            $"GilderData-{DateTime.Today.Date.ToShortDateString()}.gdb",
            gilderDataByteStr);
    }
    
    private class DataToSave
    {
        public IEnumerable<Domain.Group> Groups { get; set; }
        public IEnumerable<Domain.Budget> Budgets { get; set; }
        public IEnumerable<Domain.Transaction> Transactions { get; set; }
        public IEnumerable<Domain.Import> Imports { get; set; }
        public IEnumerable<Domain.Deduction> Deductions { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}