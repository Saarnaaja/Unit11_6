using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit11_6.Operations;
public class GetTextLenght : IOperation
{
    public string Execute(string message)
    {
        return $"Длина сообщения: {message.Length} знаков";
    }
}