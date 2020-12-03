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

            var isLastLine = top == height - 1;
            if (isLastLine)
            {
                --top;
            }

            Console.CursorVisible = false;

            var message = new StringBuilder();
            var sw = new Stopwatch();
            var lastElapsed = sw.Elapsed;
            for (var i = 0; i < length; ++i)
            {
                message.Clear();

                sw.Restart();
                await DoStuff();
                sw.Stop();

                Console.SetCursorPosition(left, top);
                var progress = (float)i / (length - 1);

                if (isLastLine)
                {
                    message.Append(Environment.NewLine);
                }

                message.Append('[');
                var remaining = sw.Elapsed * (length - 1 - i);
                var progressStr = $" [{progress:P2}|{CalculateTotalTime(sw.Elapsed)}/it|{CalculateTotalTime(remaining)}]";
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

                lastElapsed = sw.Elapsed;
            }

            Console.CursorVisible = true;

            await Task.CompletedTask;
        }

        private static string CalculateTotalTime(TimeSpan ts)
        {
            return ts.TotalMilliseconds switch
            {
                >= 100 => $"{ts.TotalSeconds:F2} s",
                < 100 => $"{ts.TotalMilliseconds:F2} ms",
                _ => throw new InvalidOperationException()
            };
        }

        private static async Task DoStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }
    }
}
