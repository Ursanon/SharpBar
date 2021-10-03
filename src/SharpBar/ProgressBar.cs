using System;
using System.Diagnostics;
using System.Text;

namespace SharpBar
{
    public sealed class ProgressBar : IDisposable
    {
        private readonly int _top;
        private readonly int _left;
        private readonly int _width;
        private readonly int _height;
        private readonly ulong _length;
        private readonly Stopwatch _stopwatch;
        private readonly StringBuilder _message;

        private ulong _current = 0;

        public ProgressBar(ulong length)
        {
            _length = length;
            _stopwatch = new Stopwatch();
            _message = new StringBuilder();

            Console.CursorVisible = false;

            _width = Console.BufferWidth;
            _height = Console.BufferHeight;

            (_left, _top) = Console.GetCursorPosition();

            var isLastLine = _top == _height - 1;
            if (isLastLine)
            {
                --_top;
                _message.Append(Environment.NewLine);
            }

            RaportProgress();
        }

        public void RaportProgress()
        {
            Console.SetCursorPosition(_left, _top);
            var progress = (float)_current / (_length);

            _message.Append('[');
            var remaining = _stopwatch.Elapsed * (_length - _current);
            var progressStr = $" [{progress:P2}|{CalculateTotalTime(_stopwatch.Elapsed)}/it|{CalculateTotalTime(remaining)}]";
            var offset = 2 + progressStr.Length;

            var emptyWidth = Math.Floor(progress * (_width - offset));

            for (var j = 0; j < emptyWidth; ++j)
            {
                _message.Append((char)9608);
            }

            for (var j = emptyWidth; j < (_width - offset); ++j)
            {
                _message.Append('-');
            }

            _message.Append(']')
                    .Append(progressStr);

            Console.WriteLine(_message);

            _current++;
            _message.Clear();

            _stopwatch.Restart();
        }

        public void Dispose()
        {
            Console.CursorVisible = true;
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
    }
}