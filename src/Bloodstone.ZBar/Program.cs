using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Bloodstone.ZBar
{
    public class Program
    {
        public static async Task Main()
        {
            var width = Console.BufferWidth;
            var height = Console.BufferHeight;

            var length = 10;
            var (left, top) = Console.GetCursorPosition();
            Console.CursorVisible = false;

            var message = new StringBuilder();
            var sw = new Stopwatch();
            for (var i = 0; i < length; ++i)
            {
                message.Clear();

                sw.Restart();
                await DoStuff();
                sw.Stop();

                Console.SetCursorPosition(left, top);
                var progress = (float)i / (length - 1);

                message.Append('[');

                var progressStr = $" [{progress:P2}|{CalculateTotalTime(sw)}/it]";
                var offset = 2 + progressStr.Length;

                var p = Math.Floor(progress * (width - offset));

                for (var j = 0; j < p; ++j)
                {
                    message.Append('|');
                }

                for (var j = p; j < (width - offset); ++j)
                {
                    message.Append('-');
                }

                message.Append(']')
                       .Append(progressStr);

                Console.Write(message);
            }
            Console.CursorVisible = true;


            await Task.CompletedTask;
        }

        private static string CalculateTotalTime(Stopwatch sw)
        {
            var elapsed = sw.Elapsed;

            return elapsed.TotalMilliseconds switch
            {
                < 100 => $"{elapsed.TotalMilliseconds:F2} ms",
                >= 100 => $"{elapsed.TotalSeconds:F2} s",
                _ => throw new InvalidOperationException()
            };
        }

        private static async Task DoStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }
    }
}
