using System;
using ClientApp.Other;

namespace ClientApp.DataSuppliers.Data
{
	internal sealed class CChatData
    {
		public Guid Id { get; }
        public String Title { get; }
        public EChatType Type { get; }
        public Boolean IsPersonal { get; }

        public CChatData(Guid id, String title, EChatType type, Boolean isPersonal)
        {
            Id = id;
            Title = title;
            Type = type;
            IsPersonal = isPersonal;
        }

        public static readonly CChatData Null = new CChatData(default(Guid), String.Empty, EChatType.Common, false);
    }
}