// <copyright file="ISerializer.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Interfaces
{
    using System.IO;

    /// <summary>
    /// Defines a generic serializer.
    /// </summary>
    public interface ISerializer
    {
        #region Methods

        /// <summary>
        /// Serializes the specified object into a string.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="data">The object.</param>
        /// <returns>The string representing the serialized object, or null in case of a failure while serializing.</returns>
        string Serialize<T>(T data);

        /// <summary>
        /// Serializes the specified object into the specified stream.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="stream">The stream to serialize to.</param>
        /// <param name="data">The object.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
        bool Serialize<T>(Stream stream, T data);

        /// <summary>
        /// Deserializes an object from the specified string.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="data">The string to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize<T>(string data);

        /// <summary>
        /// Deserializes an object from the specified stream.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="stream">The stream to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize<T>(Stream stream);

        #endregion Methods
    }
}
