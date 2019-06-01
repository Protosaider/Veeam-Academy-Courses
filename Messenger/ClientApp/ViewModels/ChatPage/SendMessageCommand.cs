using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.DataSuppliers.Data;
using ClientApp.ServiceProxies;
using DTO;

namespace ClientApp.ViewModels
{
    public sealed class CSendCommand : CBaseCommand
    {
        private readonly Int32 MAX_MESSAGE_LENGTH = 2028;
        private readonly Int32 AVG_MESSAGE_LENGTH = 256;

        private readonly IChatSupplier _chatSupplier;
        private readonly Func<Boolean> _getValidationResult;
        private readonly Action<CMessageData> _onMessageSent;

        private readonly String _anyNonWhitespacePattern = @"\S+";
        private Regex _regex;
        private Regex Regex => _regex ?? (_regex = new Regex(_anyNonWhitespacePattern));

        protected override Boolean CanExecute<T>(Object parameter)
        {
            return _getValidationResult();
        }

        protected override void Execute<T>(Object parameter)
        {
            if (parameter == null)
                return;

            CCreateMessageData messageData = (CCreateMessageData)parameter;

            String cleanMessage = CleanUpMessageText(messageData.Text);
            DateTimeOffset dispatchDate = DateTimeOffset.Now;

            var messagePosted = _chatSupplier.SendMessage(cleanMessage, dispatchDate, messageData.Type, messageData.Content, messageData.ChatId, STokenProvider.Id);

            if (messagePosted != null)
                _onMessageSent(messagePosted);
        }

        private String CleanUpMessageText(String newMessageText)
        {
            var messageText = newMessageText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            Int32 firstNotEmptyStringIndex;
            Int32 lastNotEmptyStringIndex = 0;
            for (firstNotEmptyStringIndex = 0; firstNotEmptyStringIndex < messageText.Length; firstNotEmptyStringIndex++)
            {
                if (Regex.IsMatch(messageText[firstNotEmptyStringIndex]))
                    break;
            }

            for (var i = firstNotEmptyStringIndex; i < messageText.Length; i++)
            {
                if (Regex.IsMatch(messageText[i]))
                    lastNotEmptyStringIndex = i;
            }

            StringBuilder sb = new StringBuilder(AVG_MESSAGE_LENGTH, MAX_MESSAGE_LENGTH);
            sb.Append(messageText[firstNotEmptyStringIndex].Trim());
            for (var i = firstNotEmptyStringIndex + 1; i <= lastNotEmptyStringIndex; i++)
            {
                sb.Append(Environment.NewLine);
                sb.Append(messageText[i].Trim());
            }
            String cleanMessage = sb.ToString();

            //String cleanMessage = String.Join(
            //    Environment.NewLine, 
            //    messageText.Skip(firstNotEmptyStringIndex + 1)
            //        .Take(lastNotEmptyStringIndex - firstNotEmptyStringIndex + 1)
            //        .Select(x => x.Trim())
            //    );

            return cleanMessage;
        }

        public CSendCommand(IChatSupplier chatSupplier, Func<Boolean> getValidationResult, Action<CMessageData> onMessageSent)
        {
            _chatSupplier = chatSupplier ?? throw new ArgumentNullException(nameof(chatSupplier));
            _getValidationResult = getValidationResult ?? throw new ArgumentNullException(nameof(getValidationResult));
            _onMessageSent = onMessageSent ?? throw new ArgumentNullException(nameof(onMessageSent));
        }

    }
}