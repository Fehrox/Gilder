using Application;

namespace Gilder.Infrastructure;

public class ConstTransactionSource : ITransactionSource
{

    private readonly string _data;
    
    public ConstTransactionSource(string data) {
        _data = data;
    }

    public Task<string> ReadTransactionText() => Task.FromResult(_data);
}