// <copyright file="StringExtensions.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains common string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether the specified number is a valid phone number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns><c>true</c> if the specified number is a valid phone number; otherwise, <c>false</c>.</returns>
        public static bool IsValidPhoneNumber(this string number)
        {
            return number.TrimStart(' ').StartsWith("+44");
        }

        /// <summary>
        /// Determines whether the specified URL is valid.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns><c>true</c> if the specified URL is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidUrl(this string url)
        {
            return Regex.Match(url, @"(http(s)?://)").Success;
        }

        /// <summary>
        /// Determines whether the specified URL has a voucher code.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns><c>true</c> if the specified URL has a voucher code; otherwise, <c>false</c>.</returns>
        public static bool HasUrlGotVoucherCode(this string url)
        {
            return url.Contains("?code=");
        }

        /// <summary>
        /// Gets the code from query string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>the code from within the query string</returns>
        public static string GetCodeFromQueryString(this string url)
        {
            return url.HasUrlGotVoucherCode()
                       ? url.Substring(url.IndexOf("?code=", StringComparison.OrdinalIgnoreCase) + 6)
                       : null;
        }

        /// <summary>
        /// Parses the query string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The query string as a keyed dictionary.</returns>
        public static IDictionary<string, string> ParseQueryString(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            var queryStartPosition = url.IndexOf('?');

            if (queryStartPosition < 0)
            {
                return null;
            }

            var query = url.Substring(queryStartPosition + 1);

            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            var queryElements = query.Split('&');
            var queryString = new Dictionary<string, string>();
            foreach (var element in queryElements)
            {
                var splitPosition = element.IndexOf('=');
                if (splitPosition <= 0)
                {
                    continue;
                }

                var key = element.Substring(0, splitPosition).Trim();
                var value = element.Length > splitPosition + 1 ? element.Substring(splitPosition + 1) : string.Empty;
                if (!string.IsNullOrEmpty(key) && !queryString.ContainsKey(key))
                {
                    queryString.Add(key, value);
                }
            }

            return queryString.Count > 0 ? queryString : null;
        }

        /// <summary>
        /// Converts the string to the key/value pair.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>The key/value pair.</returns>
        public static KeyValuePair<string, string>? ToKeyValuePair(this string input, char separator)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var separatorPosition = input.IndexOf(separator);
            if (separatorPosition < 1)
            {
                // The line does not contain the separator nor a valid key
                return null;
            }

            var key = input.Substring(0, separatorPosition).Trim();
            var value = input.Substring(separatorPosition + 1).Trim();

            return new KeyValuePair<string, string>(key, value);
        }

        /// <summary>
        /// Determines whether the string contains the specified value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns><c>true</c> if the string contains the specified values, <c>false</c> otherwise.</returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Determines whether the string contains all specified values.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="values">The values.</param>
        /// <returns><c>true</c> if the string contains all specified values; otherwise, <c>false</c>.</returns>
        public static bool ContainsAll(this string source, IEnumerable<string> values)
        {
            return values != null && values.All(value => source.Contains(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
