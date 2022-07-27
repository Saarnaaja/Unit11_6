using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit11_6.Operations;
public class GetSum : IOperation
{
    public string Execute(string message)
    {
        var splitText = message.Split(' ');
        var findNumberElements = splitText.Select(x => double.TryParse(x, out var value) ? value : double.NaN).ToList();
        if (findNumberElements.Contains(double.NaN))
        {
            return "Ошибка. Некорректные значения!";
        }
        var sum = findNumberElements.Sum(x => x);
        return $"Сумма: {sum}";
    }
}
