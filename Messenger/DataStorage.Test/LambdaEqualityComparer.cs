using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Test
{
	internal sealed class CLambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, Boolean> _equalsFunction;
        readonly Func<T, Int32> _hashCodeFunction;

        public CLambdaEqualityComparer(Func<T, T, Boolean> equalsFunction, Func<T, Int32> hashCodeFunction)
        {
            _equalsFunction = equalsFunction ?? throw new ArgumentNullException();
            _hashCodeFunction = hashCodeFunction ?? throw new ArgumentNullException();
        }

        public Boolean Equals(T x, T y) => _equalsFunction(x, y);
        public Int32 GetHashCode(T obj) => _hashCodeFunction(obj);
    }
}
