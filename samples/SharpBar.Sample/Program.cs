using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharpBar;

namespace SharpBar.Sample
{
    public static class Program
    {
        public static async Task Main()
        {
            var collection = Enumerable.Range(0, 10);

            foreach (var i in collection.WithProgress())
            {
                await DoStuff();
            }

            foreach (var i in Coroutine().WithProgress())
            {
            }
        }

        private static async Task DoStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }

        private static IEnumerable Coroutine()
        {
            for (var i = 0; i < 5; ++i)
            {
                Thread.Sleep(500);

                yield return null;
            }

            yield return null;
        }
    }
}