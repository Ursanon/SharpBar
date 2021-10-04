using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SharpBar
{
    /// <summary>
    /// Enumerable progress bar
    /// </summary>
    public class EnumerableProgressBar<T> : IProgressBar, IEnumerable<T>
    {
        private readonly int _top;
        private readonly int _left;
        private readonly int _width;
        private readonly int _height;
        private readonly int _length;
        private readonly Stopwatch _stopwatch;
        private readonly StringBuilder _message;
        private readonly IEnumerable<T> _collection;

        private int _current = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableProgressBar{T}"/> class.
        /// </summary>
        /// <param name="enumerable">Length</param>
        public EnumerableProgressBar(IEnumerable<T> enumerable)
        {
            _collection = enumerable;
            _length = enumerable.Count();

            _stopwatch = new Stopwatch();
            _message = new StringBuilder();

            Console.CursorVisible = false;
            Console.WriteLine("1");
            _width = Console.BufferWidth;
            _height = Console.BufferHeight;

            (_left, _top) = Console.GetCursorPosition();

            var isLastLine = _top == _height - 1;
            if (isLastLine)
            {
                --_top;
                _message.Append(Environment.NewLine);
            }
        }

        /// <summary>
        /// Progress raporting method.
        /// Should be invoked by user.
        /// </summary>
        public void ReportProgress()
        {
            Console.SetCursorPosition(_left, _top);
            var progress = (float)_current / _length;

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

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Console.CursorVisible = true;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return new ProgressBarEnumerator<T>(this, _collection);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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