using System;
using System.Collections.Generic;
using System.Text;

namespace Mangau.WillNeedUmbrella
{
    public class GenericComparer<T> : IEqualityComparer<T> // where T : struct, class
    {
        private Func<T, object> _expr { get; set; }

        public GenericComparer(Func<T, object> expr)
        {
            _expr = expr;
        }

        public bool Equals(T x, T y)
        {
            var first = _expr.Invoke(x);
            var sec = _expr.Invoke(y);

            return first != null && first.Equals(sec);
        }

        public int GetHashCode(T obj)
        {
            var first = _expr.Invoke(obj);

            return first != null ? first.GetHashCode() : 0;
        }
    }
}
