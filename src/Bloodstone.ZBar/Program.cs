using System;
using System.Threading.Tasks;

namespace Bloodstone.ZBar
{
    public class Program
    {
        public static async Task Main()
        {
            var length = 10ul;

            using (var progress = new ProgressBar(length))
            {
                for (var i = 0ul; i < length; ++i)
                {
                    await DoStuff();

                    progress.RaportProgress();
                }
            }

            await Task.CompletedTask;
        }

        private static async Task DoStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }
    }
}
