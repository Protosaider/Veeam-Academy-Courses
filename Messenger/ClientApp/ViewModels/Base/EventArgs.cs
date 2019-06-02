using System;

namespace ClientApp.ViewModels.Base
{
	internal sealed class CEventArgs<T> : EventArgs
    {
        public CEventArgs(T value)
        {
            Value = value;
        }

		private T Value { get; }
    }
}
