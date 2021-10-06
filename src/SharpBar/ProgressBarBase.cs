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
        private readonly int _maxProgress;
        private readonly Stopwatch _stopwatch;
        private readonly StringBuilder _message;

        private int _progress = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarBase"/> class.
        /// </summary>
        protected ProgressBarBase(int maxProgress)
        {
            _maxProgress = maxProgress;

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
            var remainingTime = _stopwatch.Elapsed * (_maxProgress - _progress);

            Console.SetCursorPosition(_left, _top);
            var percentage = (float)_progress / _maxProgress;

            _message.Append('[');
            var pergentageMessage = $" [{percentage:P2}|{ToDisplayTime(_stopwatch.Elapsed)}/it|{ToDisplayTime(remainingTime)}]";
            var offset = 2 + pergentageMessage.Length;

            var emptyWidth = _width - offset;
            var filledWidth = Math.Floor(percentage * emptyWidth);

            for (var i = 0; i < filledWidth; ++i)
            {
                _message.Append((char)9608);
            }

            for (var i = filledWidth; i < emptyWidth; ++i)
            {
                _message.Append('-');
            }

            _message.Append(']')
                    .Append(pergentageMessage);

            Console.WriteLine(_message);

            _progress++;
            _message.Clear();

            _stopwatch.Restart();
        }

        private static string ToDisplayTime(TimeSpan ts)
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