using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class CSendMessageCommand : ICommand
    {
        public Int32 MAX_MESSAGE_LENGTH = 2028;
        public Int32 AVG_MESSAGE_LENGTH = 256;

        public Boolean IsWaitingForExecution { get; private set; } = true;

        private readonly Predicate<Object> _canExecute;
        private readonly Action<String> _sendAction;

        public CSendMessageCommand(Action<String> sendAction, Predicate<Object> canExecute)
        {
            _sendAction = sendAction ?? throw new ArgumentNullException(nameof(sendAction));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        private String CleanUpMessageText(String newMessageText)
        {
            var messageText = newMessageText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            String anyNonWhitespace = @"\S+";
            Regex regex = new Regex(anyNonWhitespace);

            Int32 firstNotEmptyStringIndex;
            Int32 lastNotEmptyStringIndex = 0;
            for (firstNotEmptyStringIndex = 0; firstNotEmptyStringIndex < messageText.Length; firstNotEmptyStringIndex++)
            {
                if (regex.IsMatch(messageText[firstNotEmptyStringIndex]))
                    break;
            }

            for (var i = firstNotEmptyStringIndex; i < messageText.Length; i++)
            {
                if (regex.IsMatch(messageText[i]))
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


        #region ICommand

        public Boolean CanExecute(Object parameter)
        {
            return IsWaitingForExecution && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(Object parameter)
        {
            IsWaitingForExecution = false;

            _sendAction(CleanUpMessageText((String)parameter));

            IsWaitingForExecution = true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion
    }
}