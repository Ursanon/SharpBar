using System.Collections;
using System.Collections.Generic;

namespace SharpBar
{
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
            _progressBar.ReportProgress();

            return _enumerator.MoveNext();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}