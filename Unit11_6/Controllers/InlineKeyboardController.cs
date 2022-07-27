using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Unit11_6.Services;

namespace Unit11_6.Controllers;
public class InlineKeyboardController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
    {
        _telegramClient = telegramBotClient;
        _memoryStorage = memoryStorage;
    }
    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null)
            return;
        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).OperationType = callbackQuery.Data;

        string text = callbackQuery.Data switch
        {
            "length" => "Введите строку для подсчета символов",
            "sum" => "Введите числа через пробел",
            _ => String.Empty
        };

        // Отправляем в ответ уведомление о выборе
        await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
            text, cancellationToken: ct, parseMode: ParseMode.Html);
    }
}
