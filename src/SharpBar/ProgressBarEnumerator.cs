using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpBar
{
    /// <summary>
    /// ProgressBarEnumerator
    /// </summary>
    public class ProgressBarEnumerator : IEnumerator
    {
        private readonly IEnumerator _enumerator;
        private readonly EnumerableProgressBar _progressBar;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarEnumerator"/> class.
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="collection"></param>
        public ProgressBarEnumerator(EnumerableProgressBar progressBar, IEnumerable collection)
        {
            _progressBar = progressBar;
            _enumerator = collection.GetEnumerator();
        }

        /// <inheritdoc/>
        object IEnumerator.Current => _enumerator.Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_enumerator.MoveNext())
            {
                _progressBar.ReportProgress();

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _enumerator.Reset();
        }
    }

    /// <summary>
    /// ProgressBarEnumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProgressBarEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly EnumerableProgressBar<T> _progressBar;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarEnumerator{T}"/> class.
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="collection"></param>
        public ProgressBarEnumerator(EnumerableProgressBar<T> progressBar, IEnumerable<T> collection)
        {
            _progressBar = progressBar;
            _enumerator = collection.GetEnumerator();
        }

        /// <inheritdoc/>
        public T Current => _enumerator.Current;

        /// <inheritdoc/>
        object IEnumerator.Current => Current!;

        /// <inheritdoc/>
        public void Dispose()
        {
            _enumerator.Dispose();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_enumerator.MoveNext())
            {
                _progressBar.ReportProgress();

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}