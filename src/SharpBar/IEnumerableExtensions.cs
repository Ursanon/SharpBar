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
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EnumerableProgressBar<T> WithProgress<T>(this IEnumerable<T> collection)
        {
            return new EnumerableProgressBar<T>(collection);
        }

        /// <summary>
        /// Wraps collection with progress bar reporting
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static EnumerableProgressBar WithProgress(this IEnumerable collection)
        {
            return new EnumerableProgressBar(collection);
        }
    }
}