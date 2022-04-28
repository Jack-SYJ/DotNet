using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Jack.Extend.MySQL
{
    public static class IEnumerableExtend
    {

        public static IEnumerable<IEnumerable<T>> Limit<T>(this IEnumerable<T> items, int count)
        {
            var index = 0;
            var total = items.Count();

            while (index < total)
            {
                yield return items.Skip(index).Take(count);
                index += count;
            }
        }

        /// <summary>
        /// 不为null&&count>0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this IEnumerable<T> value)
        {
            if (value != null && value.Count() > 0)
            {
                return true;
            }
            return false;
        }

       

    }
}
