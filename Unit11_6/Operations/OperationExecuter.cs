using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Unit11_6.Services;

namespace Unit11_6.Operations;
public class OperationExecuter
{
    private readonly IStorage _memoryStorage;
    private readonly Dictionary<string, IOperation> _operationDict = new Dictionary<string, IOperation>
    {
        { "length", new GetTextLenght() },
        { "sum", new GetSum() },
    };
    public OperationExecuter(IStorage memoryStorage)
    {
        _memoryStorage = memoryStorage;
    }
    public string S(Message message)
    {
        var session = _memoryStorage.GetSession(message.Chat.Id);
        return _operationDict[session.OperationType].Execute(message.Text);
    }
}
