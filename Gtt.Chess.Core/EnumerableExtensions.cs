using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtt.Chess.Core
{
    public static class EnumerableExtensions
    {
        public static List<IGrouping<int, T>> CreateBlock<T>(this IEnumerable<T> list, int blockSize)
        {
            List<IGrouping<int, T>> result = list.Select((x, index) => new {x, index})
                .GroupBy(x => x.index / blockSize, y => y.x).ToList();
            return result;
        }
    }
}
