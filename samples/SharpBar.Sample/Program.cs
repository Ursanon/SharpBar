using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBar;

namespace SharpBar.Sample
{
    public static class Program
    {
        public static async Task Main()
        {
            var length = 10ul;

            var collection = Enumerable.Range(0, 10);

            foreach (var i in collection.WithProgress())
            {
                await DoStuff();
            }

            await Task.CompletedTask;
        }

        private static async Task DoStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }
    }
}