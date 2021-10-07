using System.Collections;
using System.Collections.Generic;

namespace SharpBar
{
    /// <summary>
    /// Class with extensions for <see cref="IEnumerable"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Wraps collection with progress bar reporting
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EnumerableProgressBar<T> WithProgress<T>(this IEnumerable<T> target)
        {
            return new EnumerableProgressBar<T>(target);
        }

        /// <summary>
        /// Wraps collection with progress bar reporting
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static EnumerableProgressBar WithProgress(this IEnumerable target)
        {
            return new EnumerableProgressBar(target);
        }
    }
}