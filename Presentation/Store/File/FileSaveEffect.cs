using System.Text;
using System.Text.Json;
using Application;
using Fluxor;
using Microsoft.JSInterop;

namespace Presentation.Store.File;

public class FileSaveEffect : Effect<FileSaveAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepository;
    private readonly IRepo<Domain.Group> _groupRepository;
    private readonly IJSRuntime _jsRuntime;

    public FileSaveEffect(
        IRepo<Domain.Transaction> transactionRepository,
        IRepo<Domain.Group> groupRepository,
        IJSRuntime jsRuntime)
    {
        _transactionRepository = transactionRepository;
        _groupRepository = groupRepository;
        _jsRuntime = jsRuntime;
    }
    
    public override async Task HandleAsync(FileSaveAction action, IDispatcher dispatcher)
    {
        var transactions = await _transactionRepository.Read();
        var groups = await _groupRepository.Read();
        
        var gilderData = new DataToSave {
            Groups = groups,
            Transactions = transactions
        };
        
        string gilderDataJson = JsonSerializer.Serialize(gilderData);
        var gilderDataBytes = Encoding.ASCII.GetBytes(gilderDataJson);
        var gilderDataByteStr = Convert.ToBase64String(gilderDataBytes);
        
        // Requires saveAsFile.js
        await _jsRuntime.InvokeAsync<object>(
            "saveAsFile",
            "GilderData.gdb",
            gilderDataByteStr);
    }
    
    private class DataToSave
    {
        public IEnumerable<Domain.Group> Groups { get; set; }
        public IEnumerable<Domain.Transaction> Transactions { get; set; }
    }
}