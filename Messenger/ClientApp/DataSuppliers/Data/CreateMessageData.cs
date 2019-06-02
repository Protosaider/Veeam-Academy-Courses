using System;

namespace ClientApp.DataSuppliers.Data
{
	internal sealed class CCreateMessageData
    {
        public Guid ChatId { get; }
        public String Text { get; }
        public Int32 Type { get; }
        public String Content { get; }

        public CCreateMessageData(Guid chatId, String text, Int32 type, String content)
        {
            ChatId = chatId;
            Text = text;
            Type = type;
            Content = content;
        }
    }
}
