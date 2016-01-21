// <copyright file="XmlSerializer.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Services.Serializers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using Fluffy.Common.Interfaces;

    /// <summary>
    /// Implements universal serializer with support for multiple sources.
    /// </summary>
    public class XmlSerializer : IXmlSerializer
    {
        #region Methods

        /// <summary>
        /// Serializes the specified object into a string.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="data">The object.</param>
        /// <returns>The string representing the serialized object, or null in case of a failure while serializing.</returns>
        public string Serialize<T>(T data)
        {
            try
            {
                using (var sr = new MemoryStream())
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(sr, data);

                    sr.Seek(0, SeekOrigin.Begin);
                    var result = new StreamReader(sr).ReadToEnd();
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Serializes the specified object into the specified stream.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="stream">The stream to serialize to.</param>
        /// <param name="data">The object.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
        public bool Serialize<T>(Stream stream, T data)
        {
            try
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(stream, data);

                return true;
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
            }

            return false;
        }

        /// <summary>
        /// Deserializes an object from the specified string.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="data">The string to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        public T Deserialize<T>(string data)
        {
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return default(T);
            }
        }

        /// <summary>
        /// Deserializes an object from the specified stream.
        /// </summary>
        /// <typeparam name="T">The type or the object.</typeparam>
        /// <param name="stream">The stream to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        public T Deserialize<T>(Stream stream)
        {
            try
            {
                var serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return default(T);
            }
        }

        #endregion Methods
    }
}
