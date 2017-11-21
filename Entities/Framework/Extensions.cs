using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxThingy.Entity.Framework
{
    internal static class Extensions
    {
        public static int IndexOf<T>(this IEnumerable<T> list, T item)
        {
            var iterator = list.GetEnumerator();
            for (int i = 0; iterator.MoveNext(); i++)
                if (iterator.Current.Equals(item))
                    return i;
            return -1;
        }
        public static int Pow(this int a, int toThePowerOf) => (int)Math.Pow(a, toThePowerOf);
        public static double Pow(this double a, int toThePowerOf) => Math.Pow(a, toThePowerOf);
    }
}
