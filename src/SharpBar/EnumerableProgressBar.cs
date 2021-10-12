using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpBar
{
    /// <summary>
    /// Enumerable progress bar
    /// </summary>
    public class EnumerableProgressBar : ProgressBarBase, IEnumerable
    {
        private readonly IEnumerable _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableProgressBar"/> class.
        /// </summary>
        /// <param name="target">Length</param>
        public EnumerableProgressBar(IEnumerable target)
            : base(((IEnumerable<object>)target).Count())
        {
            _target = target;
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return new ProgressBarEnumerator(this, _target);
        }
    }

    /// <summary>
    /// Enumerable progress bar
    /// </summary>
    public class EnumerableProgressBar<T> : ProgressBarBase, IEnumerable<T>
    {
        private readonly IEnumerable<T> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableProgressBar{T}"/> class.
        /// </summary>
        /// <param name="enumerable">Length</param>
        public EnumerableProgressBar(IEnumerable<T> enumerable)
            : base(enumerable.Count())
        {
            _collection = enumerable;
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
    }
}