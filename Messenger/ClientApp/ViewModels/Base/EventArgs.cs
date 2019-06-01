using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
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
