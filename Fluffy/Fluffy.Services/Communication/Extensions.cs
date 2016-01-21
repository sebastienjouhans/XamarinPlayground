// <copyright file="Extensions.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Services.Communication
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using Common;
    using Common.Interfaces;

    /// <summary>
    /// Contains extension methods of the .Net component.
    /// </summary>
    internal static class Extensions
    {
        #region Constants

        /// <summary>
        /// The response stream is null error.
        /// </summary>
        private const string TheResponseStreamIsNullError = "The response stream is null";

        #endregion Constants

        #region Methods

        /// <summary>
        /// Converts the HTTP response message to the network response returning the stream.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns>The network response with the stream return value.</returns>
        public static NetworkResponse ToNetworkResponse(this HttpResponseMessage httpResponseMessage)
        {
            return new NetworkResponse(httpResponseMessage.IsSuccessStatusCode, httpResponseMessage.StatusCode, httpResponseMessage.StatusCode.ToNetworkResponseStatus(), httpResponseMessage.ReasonPhrase);
        }

        /// <summary>
        /// Converts the HTTP response message to the network response returning the string.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <param name="responseString">The response string.</param>
        /// <returns>The network response with the string return value.</returns>
        public static NetworkResponse<TResponse> ToStringNetworkResponse<TResponse>(this HttpResponseMessage httpResponseMessage, string responseString) where TResponse : class
        {
            return new NetworkResponse<TResponse>(httpResponseMessage.IsSuccessStatusCode, httpResponseMessage.StatusCode, httpResponseMessage.StatusCode.ToNetworkResponseStatus(), httpResponseMessage.ReasonPhrase, responseString as TResponse);
        }

        /// <summary>
        /// Converts the HTTP response message to the network response returning the stream.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <param name="responseStream">The response stream.</param>
        /// <returns>The network response with the stream return value.</returns>
        public static NetworkResponse<TResponse> ToStreamNetworkResponse<TResponse>(this HttpResponseMessage httpResponseMessage, Stream responseStream) where TResponse : class
        {
            return new NetworkResponse<TResponse>(httpResponseMessage.IsSuccessStatusCode, httpResponseMessage.StatusCode, httpResponseMessage.StatusCode.ToNetworkResponseStatus(), httpResponseMessage.ReasonPhrase, responseStream as TResponse);
        }

        /// <summary>
        /// Converts the HTTP response message to the deserialised object of specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <param name="responseStream">The response stream.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The deserialised object.</returns>
        /// <exception cref="System.ArgumentNullException">serializer;A valid serializer object must be supplied</exception>
        public static NetworkResponse<TResponse> ToDeserializedNetworkResponse<TResponse>(this HttpResponseMessage httpResponseMessage, Stream responseStream, ISerializer serializer) where TResponse : class
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer", "A valid serializer object must be supplied");
            }

            var isSuccessful = httpResponseMessage.IsSuccessStatusCode;
            var statusCode = httpResponseMessage.StatusCode;
            var status = httpResponseMessage.StatusCode.ToNetworkResponseStatus();
            var reasonPhrase = httpResponseMessage.ReasonPhrase;
            TResponse response = null;

            using (httpResponseMessage)
            {
                using (responseStream)
                {
                    if (isSuccessful)
                    {
                        if (responseStream == null)
                        {
                            isSuccessful = false;
                            status = NetworkResponseStatus.CommunicationError;
                            reasonPhrase = TheResponseStreamIsNullError;
                        }
                        else
                        {
                            response = serializer.Deserialize<TResponse>(responseStream);
                            if (response == null)
                            {
                                //Logger.Current.LogError(string.Format("Deserialization error occurred while deserializing network response from {0}", httpResponseMessage.RequestMessage.RequestUri));
                                
                                isSuccessful = false;
                                status = NetworkResponseStatus.DeserializationError;
                                reasonPhrase = string.Empty;
                            }
                        }
                    }
                }
            }

            return new NetworkResponse<TResponse>(isSuccessful, statusCode, status, reasonPhrase, response);
        }

        /// <summary>
        /// Converts the HTTP status code to the network response status.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <returns>The network response status.</returns>
        public static NetworkResponseStatus ToNetworkResponseStatus(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                // 100-199
                case HttpStatusCode.Continue: // 100
                case HttpStatusCode.SwitchingProtocols: // 101
                    return NetworkResponseStatus.CommunicationError;

                // 200-299
                case HttpStatusCode.OK: // 200
                case HttpStatusCode.Created: // 201
                case HttpStatusCode.Accepted: // 202
                case HttpStatusCode.NonAuthoritativeInformation: // 203
                case HttpStatusCode.NoContent: // 204
                case HttpStatusCode.ResetContent: // 205
                case HttpStatusCode.PartialContent: // 206
                    return NetworkResponseStatus.Success;

                // 300-399
                case HttpStatusCode.Ambiguous: // 300; equvivalent to HttpStatusCode.MultipleChoices
                case HttpStatusCode.Moved: // 301; HttpStatusCode.MovedPermanently:
                case HttpStatusCode.Redirect: // 302; HttpStatusCode.Found
                case HttpStatusCode.RedirectMethod: // 303; HttpStatusCode.SeeOther
                case HttpStatusCode.NotModified: // 304
                case HttpStatusCode.UseProxy: // 305
                case HttpStatusCode.Unused: // 306
                case HttpStatusCode.TemporaryRedirect: // 307; HttpStatusCode.RedirectKeepVerb
                    return NetworkResponseStatus.CommunicationError;

                // 400-499
                case HttpStatusCode.Unauthorized: // 401
                    return NetworkResponseStatus.AuthenticationError;

                case HttpStatusCode.BadRequest: // 400
                case HttpStatusCode.PaymentRequired: // 402
                case HttpStatusCode.Forbidden: // 403
                case HttpStatusCode.NotFound: // 404
                case HttpStatusCode.MethodNotAllowed: // 405
                case HttpStatusCode.NotAcceptable: // 406
                case HttpStatusCode.ProxyAuthenticationRequired: // 407
                case HttpStatusCode.RequestTimeout: // 408
                case HttpStatusCode.Conflict: // 409
                case HttpStatusCode.Gone: // 410
                case HttpStatusCode.LengthRequired: // 411
                case HttpStatusCode.PreconditionFailed: // 412
                case HttpStatusCode.RequestEntityTooLarge: // 413
                case HttpStatusCode.RequestUriTooLong: // 414
                case HttpStatusCode.UnsupportedMediaType: // 415
                case HttpStatusCode.RequestedRangeNotSatisfiable: // 416
                case HttpStatusCode.ExpectationFailed: // 417
                    return NetworkResponseStatus.CommunicationError;

                // 500-599
                case HttpStatusCode.InternalServerError: // 500
                case HttpStatusCode.NotImplemented: // 501
                case HttpStatusCode.BadGateway: // 502
                case HttpStatusCode.ServiceUnavailable: // 503
                case HttpStatusCode.GatewayTimeout: // 504
                case HttpStatusCode.HttpVersionNotSupported: // 505
                    return NetworkResponseStatus.DataServiceError;

                default:
                    return NetworkResponseStatus.CommunicationError;
            }
        }

        #endregion Methods
    }
}
