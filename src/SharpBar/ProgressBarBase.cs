using System;
using System.Diagnostics;
using System.Text;

namespace SharpBar
{
    /// <summary>
    /// Abstract base class for progress bar
    /// </summary>
    public abstract class ProgressBarBase : IProgressBar
    {
        private readonly int _top;
        private readonly int _left;
        private readonly int _width;
        private readonly int _height;
        private readonly int _length;
        private readonly Stopwatch _stopwatch;
        private readonly StringBuilder _message;

        private int _current = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarBase"/> class.
        /// </summary>
        protected ProgressBarBase(int length)
        {
            _length = length;

            _stopwatch = new Stopwatch();
            _message = new StringBuilder();

            _width = Console.BufferWidth;
            _height = Console.BufferHeight;

            (_left, _top) = Console.GetCursorPosition();

            var isLastLine = _top == _height - 1;
            if (isLastLine)
            {
                --_top;
                _message.Append(Environment.NewLine);
            }

            ReportProgress();
        }

        /// <inheritdoc/>
        public virtual void ReportProgress()
        {
            Console.SetCursorPosition(_left, _top);
            var progress = (float)_current / _length;

            _message.Append('[');
            var remaining = _stopwatch.Elapsed * (_length - _current);
            var progressStr = $" [{progress:P2}|{CalculateTotalTime(_stopwatch.Elapsed)}/it|{CalculateTotalTime(remaining)}]";
            var offset = 2 + progressStr.Length;

            var emptyWidth = _width - offset;
            var filledWidth = Math.Floor(progress * emptyWidth);

            for (var i = 0; i < filledWidth; ++i)
            {
                _message.Append((char)9608);
            }

            for (var i = filledWidth; i < emptyWidth; ++i)
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

        private static string CalculateTotalTime(TimeSpan ts)
        {
            return ts.TotalMilliseconds switch
            {
                >= 100 => $"{ts.TotalSeconds:F2} s",
                < 100 => $"{ts.TotalMilliseconds:F2} ms",
                _ => throw new InvalidOperationException(),
            };
        }
    }
}