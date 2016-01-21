// <copyright file="JsonSerializer.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Services.Serializers
{
    using System;
    using System.IO;
    using Fluffy.Common.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Implements a generic JSON serializer.
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// The deserialiser settings
        /// </summary>
        private JsonSerializerSettings deserialiserSettings = new JsonSerializerSettings()
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        #region Methods
        
        /// <summary>
        /// Serialises object into a string
        /// </summary>
        /// <typeparam name="T">Type or the serialisable object</typeparam>
        /// <param name="data">Object to serialise</param>
        /// <returns>String representing the serialised object</returns>
        public string Serialize<T>(T data)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(data, this.deserialiserSettings);
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Serialises object into a stream
        /// </summary>
        /// <typeparam name="T">Type or the serialisable object</typeparam>
        /// <param name="stream">Stream to serialise to</param>
        /// <param name="data">Object to serialise</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
        public bool Serialize<T>(Stream stream, T data)
        {
            try
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    // The stream is automatically disposed by the streamWriter.Dispose, hence removing reference to the stream
                    stream = null;

                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    serializer.Serialize(streamWriter, data);
                    if (streamWriter.BaseStream.CanSeek)
                    {
                        streamWriter.BaseStream.SetLength(streamWriter.BaseStream.Position);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            return false;
        }

        /// <summary>
        /// Deserialises object from the string
        /// </summary>
        /// <typeparam name="T">Type or the serialisable object</typeparam>
        /// <param name="data">String representing the serialised object</param>
        /// <returns>Deserialised object</returns>
        public T Deserialize<T>(string data)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data, this.deserialiserSettings);
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return default(T);
            }
        }

        /// <summary>
        /// Deserialises object from a stream
        /// </summary>
        /// <typeparam name="T">Type of the serialisable object</typeparam>
        /// <param name="stream">Stream to serialise from</param>
        /// <returns>Deserialised object</returns>
        public T Deserialize<T>(Stream stream)
        {
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(stream);

                // The stream is automatically disposed by the streamReader.Dispose, hence removing reference to the stream
                stream = null;

                using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                {
                    // The streamReader is automatically disposed by the jsonTextReader.Dispose, hence removing reference to the streamReader
                    streamReader = null;

                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
            catch (Exception ex)
            {
                //Logger.Current.LogError(ex);
                return default(T);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }

                if (streamReader != null)
                {
                    streamReader.Dispose();
                }
            }
        }

        #endregion Methods
    }
}
