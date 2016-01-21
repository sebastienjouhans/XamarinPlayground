// <copyright file="CollectionExtensions.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Contains common collection extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Gets the value from the dictionary of strings using the string key ignoring the case.
        /// </summary>
        /// <param name="dictionary">The dictionary of strings.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public static string GetValue(this IDictionary<string, string> dictionary, string key)
        {
            if (dictionary == null || !dictionary.Any())
            {
                return null;
            }

            return (from item in dictionary
                    where string.Equals(item.Key, key, StringComparison.OrdinalIgnoreCase)
                    select item.Value).FirstOrDefault();
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Updates the target collection to match the items of the source collection using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">Type of the collection item</typeparam>
        /// <param name="target">The target collection</param>
        /// <param name="source">The source collection</param>
        /// <param name="comparer">The equality comparer</param>
        /// <param name="replaceMatchingItems">if set to <c>true</c> replace matching target items with the source ones.</param>
        /// <returns>The updated collection</returns>
        /// <exception cref="System.ArgumentNullException">target;The target collection must not be null
        /// or
        /// comparer;The comparer cannot be null</exception>
        /// <remarks>The update mechanism works from the top down and supports duplicate items; If null source is supplied the target list is cleared</remarks>
        public static IList<T> Update<T>(this IList<T> target, IList<T> source, IEqualityComparer<T> comparer, bool replaceMatchingItems)
            where T : class
        {
            if (target == null)
            {
                throw new ArgumentNullException("target", "The target collection must not be null");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException("comparer", "The comparer cannot be null");
            }

            if (source == null)
            {
                target.Clear();
                return target;
            }

            // Running the for cycle until the end of the source collection
            for (var sourceIndex = 0; sourceIndex < source.Count; sourceIndex++)
            {
                // Removing each target collection item at the current position that is not found down in the source collection
                while (target.Count > sourceIndex && source.IndexOf(target[sourceIndex], sourceIndex, comparer) < 0)
                {
                    target.RemoveAt(sourceIndex);
                }

                var sourceItem = source[sourceIndex];

                var itemIndex = target.IndexOf(sourceItem, sourceIndex, comparer);
                if (itemIndex < 0)
                {
                    // The source item is not found in the target collection, insert the item at the current position
                    target.Insert(sourceIndex, sourceItem);
                }
                else if (itemIndex > sourceIndex)
                {
                    // The source item is found in the target position, however below the current position
                    // Remove the target item from its original position and insert it at the current position
                    var targetItem = target[itemIndex];
                    target.RemoveAt(itemIndex);
                    target.Insert(sourceIndex, targetItem);
                }
                else if (replaceMatchingItems)
                {
                    // Otherwise assuming that itemIndex == sourceIndex
                    // The matching item is found, updating the object in the current collection with the source
                    // Continue to the next index
                    target[sourceIndex] = sourceItem;
                }
            }

            // Removing redundant items from the target collection
            while (target.Count > source.Count)
            {
                target.RemoveAt(source.Count);
            }

            return target;
        }

        /// <summary>
        /// Determines the index of a specified item in the collection starting from the specified index and using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">the type of the item</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="item">The item</param>
        /// <param name="startIndex">The start index</param>
        /// <param name="comparer">The equality comparer</param>
        /// <returns>The index of item in the collection or -1 if the item is not found</returns>
        /// <exception cref="System.ArgumentNullException">
        /// collection;The collection must not be null
        /// or
        /// item;The item must not be null
        /// or
        /// comparer;The comparer cannot be null
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex;The start index cannot be negative</exception>
        public static int IndexOf<T>(this IList<T> collection, T item, int startIndex, IEqualityComparer<T> comparer)
            where T : class
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "The collection must not be null");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item", "The item must not be null");
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", "The start index cannot be negative");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException("comparer", "The comparer cannot be null");
            }

            for (int index = startIndex; index < collection.Count; index++)
            {
                if (comparer.Equals(collection[index], item))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Updates the target collection to match the items of the source collection using the specified equality comparer.
        /// </summary>
        /// <param name="target">The target collection</param>
        /// <param name="source">The source collection</param>
        /// <returns>The updated collection</returns>
        /// <exception cref="System.ArgumentNullException">target;The target collection must not be null
        /// or
        /// comparer;The comparer cannot be null</exception>
        /// <remarks>The update mechanism works from the top down and supports duplicate items; If null source is supplied the target list is cleared</remarks>
        public static IList Update(this IList target, IList source)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target", "The target collection must not be null");
            }

            if (source == null)
            {
                target.Clear();
                return target;
            }

            // Running the for cycle until the end of the source collection
            for (var sourceIndex = 0; sourceIndex < source.Count; sourceIndex++)
            {
                // Removing each target collection item at the current position that is not found down in the source collection
                while (target.Count > sourceIndex && source.IndexOf(target[sourceIndex], sourceIndex) < 0)
                {
                    target.RemoveAt(sourceIndex);
                }

                var sourceItem = source[sourceIndex];

                var itemIndex = target.IndexOf(sourceItem, sourceIndex);
                if (itemIndex < 0)
                {
                    // The source item is not found in the target collection, insert the item at the current position
                    target.Insert(sourceIndex, sourceItem);
                }
                else if (itemIndex > sourceIndex)
                {
                    // The source item is found in the target position, however below the current position
                    // Remove the target item from its original position and insert it at the current position
                    var targetItem = target[itemIndex];
                    target.RemoveAt(itemIndex);
                    target.Insert(sourceIndex, targetItem);
                }

                // Otherwise assuming that itemIndex == sourceIndex
                // Continue to the next index
            }

            // Removing redundant items from the target collection
            while (target.Count > source.Count)
            {
                target.RemoveAt(source.Count);
            }

            return target;
        }

        /// <summary>
        /// Determines the index of a specified item in the collection starting from the specified index and using the specified equality comparer.
        /// </summary>
        /// <param name="collection">The collection</param>
        /// <param name="item">The item</param>
        /// <param name="startIndex">The start index</param>
        /// <returns>The index of item in the collection or -1 if the item is not found</returns>
        /// <exception cref="System.ArgumentNullException">collection;The collection must not be null
        /// or
        /// item;The item must not be null
        /// or
        /// comparer;The comparer cannot be null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex;The start index cannot be negative</exception>
        public static int IndexOf(this IList collection, object item, int startIndex)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "The collection must not be null");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item", "The item must not be null");
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", "The start index cannot be negative");
            }

            for (var index = startIndex; index < collection.Count; index++)
            {
                if (collection[index].Equals(item))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Interweaves the specified list with another list.
        /// </summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="listA">The list a.</param>
        /// <param name="listB">The list b.</param>
        /// <returns>The interwoven lists.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// listA;The argument must not be null
        /// or
        /// listB;The argument must not be null
        /// </exception>
        public static IEnumerable<T> Interweave<T>(this IEnumerable<T> listA, IEnumerable<T> listB)
            where T : class 
        {
            if (listA == null)
            {
                throw new ArgumentNullException("listA", "The argument must not be null");
            }

            if (listB == null)
            {
                throw new ArgumentNullException("listB", "The argument must not be null");
            }

            var listAEnumerator = listA.GetEnumerator();
            var listBEnumerator = listB.GetEnumerator();

            while (true)
            {
                var hasAValue = listAEnumerator.MoveNext();
                if (hasAValue)
                {
                    yield return listAEnumerator.Current;
                }

                var hasBValue = listBEnumerator.MoveNext();
                if (hasBValue)
                {
                    yield return listBEnumerator.Current;
                }

                if (!hasAValue && !hasBValue)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Removes the items from the list A that are found in the list B.
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="listA">The list A.</param>
        /// <param name="listB">The list B.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>The items without duplicates.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// listA;The argument must not be null
        /// or
        /// listB;The argument must not be null
        /// </exception>
        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> listA, IEnumerable<T> listB, IEqualityComparer<T> comparer)
        {
            if (listA == null)
            {
                throw new ArgumentNullException("listA", "The argument must not be null");
            }

            if (listB == null)
            {
                throw new ArgumentNullException("listB", "The argument must not be null");
            }

            return from item in listA
                   let isDuplicate = listB.Contains(item, comparer)
                   where !isDuplicate
                   select item;
        }

        /// <summary>
        /// Concatenates non-empty members of a string collection, using the specified separator between each member.
        /// </summary>
        /// <param name="values">The array that contains the elements to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>The concatenated string.</returns>
        public static string JoinNonEmpty(this IEnumerable<string> values, string separator)
        {
            var builder = new StringBuilder();

            foreach (var value in values.Where(value => !string.IsNullOrWhiteSpace(value)))
            {
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }

                builder.Append(value.Trim());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the latest activity date time.
        /// </summary>
        /// <param name="dates">The dates.</param>
        /// <returns>The latest activity date time.</returns>
        public static DateTime GetLatestActivityDateTime(this IEnumerable<DateTime> dates)
        {
            return (from date in dates
                    where date < DateTime.UtcNow
                    orderby date descending
                    select date).FirstOrDefault();
        }

        #endregion Methods
    }
}
