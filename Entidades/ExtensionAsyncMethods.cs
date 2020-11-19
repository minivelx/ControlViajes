using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entidades
{
    public static class ExtensionAsyncMethods
    {        
        public static async Task ForEachAsync<T>(this List<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                await Task.Run(() => { action(item); }).ConfigureAwait(false);
        }
    }
}