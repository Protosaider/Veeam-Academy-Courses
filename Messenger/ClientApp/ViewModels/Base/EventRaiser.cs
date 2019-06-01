using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
	internal static class CEventRaiser
    {
        public static void Raise(this EventHandler handler, Object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise<T>(this EventHandler<CEventArgs<T>> handler, Object sender, T value)
        {
            handler?.Invoke(sender, new CEventArgs<T>(value));
        }

        public static void Raise<T>(this EventHandler<T> handler, Object sender, T value) where T : EventArgs
        {
            handler?.Invoke(sender, value);
        }

        public static void Raise<T>(this EventHandler<CEventArgs<T>> handler, Object sender, CEventArgs<T> value)
        {
            handler?.Invoke(sender, value);
        }
    }
}
