// <copyright file="NetworkResponseStatus.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common
{
    /// <summary>
    /// Represents the high level information about the network response status.
    /// </summary>
    public enum NetworkResponseStatus
    {
        /// <summary>
        /// The success.
        /// </summary>
        Success,

        /// <summary>
        /// The authentication error.
        /// </summary>
        AuthenticationError,

        /// <summary>
        /// The communication error.
        /// </summary>
        CommunicationError,

        /// <summary>
        /// The data service error.
        /// </summary>
        DataServiceError,

        /// <summary>
        /// The deserialization error.
        /// </summary>
        DeserializationError,

        /// <summary>
        /// The serialization error.
        /// </summary>
        SerializationError,
    }
}
