using System.Text;
using System.Text.Json;
using Application;
using Domain;
using Fluxor;
using Microsoft.JSInterop;

namespace Presentation.Store.File;

public class FileSaveEffect : Effect<FileSaveAction>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IJSRuntime _jsRuntime;

    public FileSaveEffect(
        ITransactionRepository transactionRepository,
        IGroupRepository groupRepository,
        IJSRuntime jsRuntime)
    {
        _transactionRepository = transactionRepository;
        _groupRepository = groupRepository;
        _jsRuntime = jsRuntime;
    }
    
    public override async Task HandleAsync(FileSaveAction action, IDispatcher dispatcher)
    {
        var transactions = await _transactionRepository.ReadTransactions();
        var groups = await _groupRepository.ReadGroups();
        
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