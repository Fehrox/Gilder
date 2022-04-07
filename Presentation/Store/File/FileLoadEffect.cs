using Fluxor;

using System.Text.Json;
using Presentation.Store.Group;
using Presentation.Store.Transaction;

namespace Presentation.Store.File;

public class FileLoadEffect : Effect<FileLoadAction>
{
    public override async Task HandleAsync(FileLoadAction action, IDispatcher dispatcher)
    {
        var chosenFile = action.FileToLoad;
        
        var ms = new MemoryStream();
        await chosenFile.OpenReadStream().CopyToAsync(ms);
            
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        var jsonText = await reader.ReadToEndAsync();
        
        var gilderData = JsonSerializer.Deserialize<LoadedGilderData>(jsonText);
        if (gilderData != null) {
            Console.WriteLine(nameof(HandleAsync)); 
            dispatcher.Dispatch(new TransactionClearAction());
            dispatcher.Dispatch(new TransactionCreateAction(gilderData.Transactions));   
            
            dispatcher.Dispatch(new GroupClearAction());
            dispatcher.Dispatch(new GroupRepoCreateAction(gilderData.Groups));
        }
    }
    
    private class LoadedGilderData
    {
        public IEnumerable<Domain.Group> Groups { get; set; } = new List<Domain.Group>();
        public IEnumerable<Domain.Transaction> Transactions { get; set; } = new List<Domain.Transaction>();
    }
}