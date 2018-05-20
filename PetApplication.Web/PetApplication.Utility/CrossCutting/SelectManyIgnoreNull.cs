using System;
using System.Collections.Generic;
using System.Linq;

namespace PetApplication.Utility
{
    public static class SelectManyIgnoreNull
    {

        public static IEnumerable<TResult> SelectManyIgnoringNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            return source.Select(selector)
                .Where(e => e != null)
                .SelectMany(e => e);
        }
    }
}
