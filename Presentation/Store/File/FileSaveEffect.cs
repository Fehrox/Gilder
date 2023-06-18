using System.Text;
using System.Text.Json;
using Application;
using Fluxor;
using Microsoft.JSInterop;

namespace Presentation.Store.File;

public class FileSaveEffect : Effect<FileSaveAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepo;
    private readonly IRepo<Domain.Group> _groupRepo;
    private readonly IRepo<Domain.Budget> _budgetRepo;
    private readonly IJSRuntime _jsRuntime;

    public FileSaveEffect(
        IRepo<Domain.Transaction> transactionRepo,
        IRepo<Domain.Group> groupRepo,
        IRepo<Domain.Budget> budgetRepo,
        IJSRuntime jsRuntime)
    {
        _transactionRepo = transactionRepo;
        _groupRepo = groupRepo;
        _budgetRepo = budgetRepo;
        _jsRuntime = jsRuntime;
    }
    
    public override async Task HandleAsync(FileSaveAction action, IDispatcher dispatcher)
    {
        var transactions = await _transactionRepo.Read();
        var groups = await _groupRepo.Read();
        var budgets = await _budgetRepo.Read();
        
        var gilderData = new DataToSave {
            Groups = groups,
            Budgets = budgets,
            Transactions = transactions,
        };
        
        string gilderDataJson = JsonSerializer.Serialize(gilderData);
        var gilderDataBytes = Encoding.ASCII.GetBytes(gilderDataJson);
        var gilderDataByteStr = Convert.ToBase64String(gilderDataBytes);
        
        // Requires saveAsFile.js
        await _jsRuntime.InvokeAsync<object>(
            "saveAsFile",
            $"GilderData-{System.DateTime.Today.Date.ToShortDateString()}.gdb",
            gilderDataByteStr);
    }
    
    private class DataToSave
    {
        public IEnumerable<Domain.Group> Groups { get; set; }
        public IEnumerable<Domain.Budget> Budgets { get; set; }
        public IEnumerable<Domain.Transaction> Transactions { get; set; }
    }
}