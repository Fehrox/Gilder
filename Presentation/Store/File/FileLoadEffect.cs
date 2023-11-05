using Fluxor;

using System.Text.Json;
using Presentation.Store.Budget;
using Presentation.Store.Deductions;
using Presentation.Store.Group;
using Presentation.Store.Imports;
using Presentation.Store.Transaction;

namespace Presentation.Store.File;

public class FileLoadEffect : Effect<FileLoadAction>
{
    public override async Task HandleAsync(FileLoadAction action, IDispatcher dispatcher)
    {
        var chosenFile = action.FileToLoad;
        
        var ms = new MemoryStream();
        const long MAX_FILE_SIZE_1_GB = 1L * 1024 * 1024 * 1024;
        await chosenFile.OpenReadStream(MAX_FILE_SIZE_1_GB).CopyToAsync(ms);
            
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        var jsonText = await reader.ReadToEndAsync();
        
        var gilderData = JsonSerializer.Deserialize<LoadedGilderData>(jsonText);
        if (gilderData != null) {
            
            dispatcher.Dispatch(new ImportClearAction());
            dispatcher.Dispatch(new ImportCreateAction(gilderData.Imports));
            dispatcher.Dispatch(new ImportRepoCreateAction(gilderData.Imports));
            
            dispatcher.Dispatch(new TransactionClearAction());
            dispatcher.Dispatch(new TransactionCreateAction(gilderData.Transactions));
            dispatcher.Dispatch(new TransactionRepoCreateAction(gilderData.Transactions));   
            
            dispatcher.Dispatch(new GroupClearAction());
            dispatcher.Dispatch(new GroupCreateAction(gilderData.Groups));
            dispatcher.Dispatch(new GroupRepoCreateAction(gilderData.Groups));
            
            dispatcher.Dispatch(new BudgetClearAction());
            dispatcher.Dispatch(new BudgetCreateAction(gilderData.Budgets));
            dispatcher.Dispatch(new BudgetRepoCreateAction(gilderData.Budgets));
            
            dispatcher.Dispatch(new DeductionClearAction());
            var deductionArray = gilderData.Deductions.ToArray();
            dispatcher.Dispatch(new DeductionCreateAction(deductionArray));
            dispatcher.Dispatch(new DeductionRepoCreateAction(deductionArray));
        }
    }
    
    private class LoadedGilderData
    {
        public IEnumerable<Domain.Group> Groups { get; set; } = new List<Domain.Group>();
        public IEnumerable<Domain.Import> Imports { get; set; } = new List<Domain.Import>();
        public IEnumerable<Domain.Transaction> Transactions { get; set; } = new List<Domain.Transaction>();
        public IEnumerable<Domain.Budget> Budgets { get; set; } = new List<Domain.Budget>();
        public IEnumerable<Domain.Deduction> Deductions { get; set; } = new List<Domain.Deduction>();
    }
}