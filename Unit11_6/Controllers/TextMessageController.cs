using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Unit11_6.Operations;
using Unit11_6.Services;

namespace Unit11_6.Controllers;
public class TextMessageController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;
    private readonly OperationExecuter _operationExecuter;

    public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
    {
        _telegramClient = telegramBotClient;
        _memoryStorage = memoryStorage;
        _operationExecuter = new OperationExecuter(memoryStorage);
    }
    public async Task Handle(Message message, CancellationToken ct)
    {
        //TODO:
        //оставить switch, но команды разбить на несколько классов
        //1. обработчик команд (находит команду по введенному сообщению, если есть - выполняет ее)
        //2. если команда не обнаружена выполнять код default
        switch (message.Text)
        {
            case "/start":
                var btns = CreateButtons();
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Давай посчитаем?", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(btns));
                break;
            default:
                var returnedText = _operationExecuter.S(message);
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, returnedText, cancellationToken: ct);
                break;
        }
    }

    private List<InlineKeyboardButton[]> CreateButtons()
    {
        var buttons = new List<InlineKeyboardButton[]>();
        buttons.Add(new[]
        {
                        InlineKeyboardButton.WithCallbackData($"Подсчет символов" , $"length"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел" , $"sum")
                    });
        return buttons;
    }
}