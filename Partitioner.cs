using System;
using System.Collections.Generic;

namespace Max.Business.Components.Utils
{
    /// <summary>
    /// A helper class that provides functionality for partitioning collections based on predicates.
    ///
    /// Author: Trevor Stanfield
    /// </summary>
    public static class Partitioner
    {
        /// <summary>
        /// Partitions a dictionary into multiple dictionaries based on the provided value predicates.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The dictionary to partition.</param>
        /// <param name="predicates">An array of predicates to partition the dictionary by values.</param>
        /// <returns>
        /// A list of dictionaries where each dictionary contains items that satisfy the corresponding
        /// predicate plus one which contains all items which satisfy none of the supplied predicates if such items exist.
        /// </returns>
        public static List<Dictionary<TKey, TValue>> PartitionBy<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            params Func<TValue, bool>[] predicates)
        {
            // Initialize the partitions and the remaining dictionary.
            var partitions = new List<Dictionary<TKey, TValue>>();
            var remaining = new Dictionary<TKey, TValue>();

            // Create partitions for each predicate.
            for (int i = 0; i < predicates.Length; i++)
            {
                partitions.Add(new Dictionary<TKey, TValue>());
            }

            // Iterate through the source dictionary.
            foreach (var kv in source)
            {
                bool isMatched = false;

                // Check each predicate to see if it matches the current value.
                for (int i = 0; i < predicates.Length; i++)
                {
                    if (predicates[i](kv.Value))
                    {
                        partitions[i].Add(kv.Key, kv.Value);
                        isMatched = true;
                        break;
                    }
                }

                // If no predicate matched, add the item to the remaining dictionary.
                if (!isMatched)
                {
                    remaining.Add(kv.Key, kv.Value);
                }
            }

            // Add the remaining items as a final partition if any.
            if (remaining.Count > 0)
            {
                partitions.Add(remaining);
            }

            return partitions;
        }

        /// <summary>
        /// Partitions a dictionary into multiple dictionaries based on the provided key predicates.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The dictionary to partition.</param>
        /// <param name="predicates">An array of predicates to partition the dictionary by keys.</param>
        /// <returns>
        /// A list of dictionaries where each dictionary contains items that satisfy the corresponding
        /// predicate plus one which contains all items which satisfy none of the supplied predicates if such items exist.
        /// </returns>
        public static List<Dictionary<TKey, TValue>> PartitionBy<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            params Func<TKey, bool>[] keyPredicates)
        {
            // Initialize the partitions and the remaining dictionary.
            var partitions = new List<Dictionary<TKey, TValue>>();
            var remaining = new Dictionary<TKey, TValue>();

            // Create partitions for each predicate
            for (int i = 0; i < keyPredicates.Length; i++)
            {
                partitions.Add(new Dictionary<TKey, TValue>());
            }

            // Iterate through the source dictionary.
            foreach (var kv in source)
            {
                bool isMatched = false;

                // Check each predicate to see if it matches the current key.
                for (int i = 0; i < keyPredicates.Length; i++)
                {
                    if (keyPredicates[i](kv.Key))
                    {
                        partitions[i].Add(kv.Key, kv.Value);
                        isMatched = true;
                        break;
                    }
                }

                // If no predicate matched, add the item to the remaining dictionary.
                if (!isMatched)
                {
                    remaining.Add(kv.Key, kv.Value);
                }
            }

            // Add the remaining items as a final partition if any.
            if (remaining.Count > 0)
            {
                partitions.Add(remaining);
            }

            return partitions;
        }

        /// <summary>
        /// Partitions an enumerable into multiple lists based on the provided predicates.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
        /// <param name="source">The enumerable to partition.</param>
        /// <param name="predicates">An array of predicates to partition the enumerable by elements.</param>
        /// <returns>A list of lists where each list contains items that satisfy the corresponding predicate.</returns>
        /// <returns>
        /// A list of lists where each dictionary contains items that satisfy the corresponding
        /// predicate plus one which contains all items which satisfy none of the supplied predicates if such items exist.
        /// </returns>
        public static List<List<T>> PartitionBy<T>(
            this IEnumerable<T> source,
            params Func<T, bool>[] predicates)
        {
            // Initialize the partitions and the remaining list.
            var partitions = new List<List<T>>();
            var remaining = new List<T>();

            // Create partitions for each predicate.
            for (int i = 0; i < predicates.Length; i++)
            {
                partitions.Add(new List<T>());
            }

            // Iterate through the source enumerable.
            foreach (var item in source)
            {
                bool isMatched = false;

                // Check each predicate to see if it matches the current element.
                for (int i = 0; i < predicates.Length; i++)
                {
                    if (predicates[i](item))
                    {
                        partitions[i].Add(item);
                        isMatched = true;
                        break;
                    }
                }

                // If no predicate matched, add the item to the remaining list.
                if (!isMatched)
                {
                    remaining.Add(item);
                }
            }

            // Add the remaining items as a final partition if any.
            if (remaining.Count > 0)
            {
                partitions.Add(remaining);
            }

            return partitions;
        }

        /// <summary>
        /// Partitions a dictionary into multiple dictionaries based on the provided value predicates.
        /// This method is recommended when the key and value types of the dictionary are the same.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the source dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the source dictionary.</typeparam>
        /// <param name="source">The source dictionary to partition.</param>
        /// <param name="predicates">A params array of predicates to match the dictionary values against.</param>
        /// <returns>
        /// A list of dictionaries, where each dictionary contains the items from the source that match the corresponding predicate.
        /// An additional dictionary is added to the list containing the items that did not match any of the provided value predicates if such items exist.
        /// </returns>
        public static List<Dictionary<TKey, TValue>> PartitionByValues<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            params Func<TValue, bool>[] valuePredicates)
        {
            return source.PartitionBy(valuePredicates);
        }

        /// <summary>
        /// Partitions a dictionary into multiple dictionaries based on the provided key predicates.
        /// This method is recommended when the key and value types of the dictionary are the same.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the source dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the source dictionary.</typeparam>
        /// <param name="source">The source dictionary to partition.</param>
        /// <param name="keyPredicates">A params array of predicates to match the dictionary keys against.</param>
        /// <returns>
        /// A list of dictionaries, where each dictionary contains the items from the source that match the corresponding key predicate.
        /// An additional dictionary is added to the list containing the items that did not match any of the provided key predicates if such items exist.
        /// </returns>
        public static List<Dictionary<TKey, TValue>> PartitionByKeys<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            params Func<TKey, bool>[] keyPredicates)
        {
            return source.PartitionBy(keyPredicates);
        }
    }
}