// <copyright file="CommunicationService.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Services.Communication
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Common;
    using Common.Interfaces;

    /// <summary>
    /// Implementation of the communication service.
    /// </summary>
    public partial class CommunicationService : ICommunicationService, IDisposable
    {
        #region Constants

        /// <summary>
        /// The activity operation ID template.
        /// </summary>
        private const string ActivityOperationIdTemplate = "Network request ID: {0}";

        /// <summary>
        /// The application/json value.
        /// </summary>
        private const string ApplicationJsonValue = "application/json";

        /// <summary>
        /// The value of UTF8 character set.
        /// </summary>
        private const string CharSetUtf8Value = "utf-8";

        #endregion Constants

        #region Fields

        /// <summary>
        /// Gets the content type of JSON encoded as UTF8.
        /// </summary>
        /// <returns>the content type header value.</returns>
        private static readonly MediaTypeHeaderValue ContentTypeJsonUtf8Header =
            new MediaTypeHeaderValue(ApplicationJsonValue) { CharSet = CharSetUtf8Value };

        /// <summary>
        /// The activity reporting service.
        /// </summary>
        protected readonly IActivityReportingService activityReportingService;

        /// <summary>
        /// The JSON serializer.
        /// </summary>
        protected readonly IJsonSerializer jsonSerializer;

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The user agent.
        /// </summary>
        private readonly string userAgent;

        /// <summary>
        /// The request ID lock.
        /// </summary>
        private readonly object requestIdLock = new object();

        /// <summary>
        /// The request ID.
        /// </summary>
        private long requestId;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="CommunicationService" /> class.
        /// </summary>
        /// <param name="activityReportingService">The activity reporting service.</param>
        /// <param name="jsonSerializer">The JSON serializer.</param>
        public CommunicationService(
            IActivityReportingService activityReportingService,
            IJsonSerializer jsonSerializer)
        {
            this.activityReportingService = activityReportingService;
            this.jsonSerializer = jsonSerializer;

            this.httpClient = new HttpClient();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
            }
        }

        /// <summary>
        /// Gets the network response asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="request">The request.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="showActivity">if set to <c>true</c> show activity.</param>
        /// <returns>The task returning the network response.</returns>
        protected async Task<NetworkResponse<TResponse>> GetNetworkResponseAsync<TResponse>(
            Uri requestUri,
            HttpRequestMessage request,
            ISerializer serializer,
            bool showActivity) where TResponse : class
        {
            NetworkResponse<TResponse> networkResponse = null;
            var activityId = this.RegisterActivity(requestUri, showActivity);

            try
            {
                var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);

                if (response == null)
                {
                    return null;
                }

                if (typeof(TResponse) == typeof(string))
                {
                    string responseString = null;
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }

                    networkResponse = response.ToStringNetworkResponse<TResponse>(responseString);
                }
                else
                {
                    Stream responseStream = null;
                    if (response.IsSuccessStatusCode)
                    {
                        responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    }

                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (typeof(TResponse) == typeof(Stream))
                    {
                        networkResponse = response.ToStreamNetworkResponse<TResponse>(responseStream);
                    }
                    else
                    {
                        networkResponse = response.ToDeserializedNetworkResponse<TResponse>(responseStream, serializer);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Current.LogWarning(
                //    string.Format(
                //        "Error getting network response from {0}. Error: {1}",
                //        requestUri.OriginalString,
                //        ex.Message));

                return null;
            }
            finally
            {
                var statusCode = networkResponse == null ? null : networkResponse.StatusCode.ToString();
                this.UnregisterActivity(activityId, showActivity, statusCode);
            }

            return networkResponse;
        }

        /// <summary>
        /// Gets the network response asynchronously.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="request">The request.</param>
        /// <param name="showActivity">if set to <c>true</c> show activity.</param>
        /// <returns>The task returning the network response.</returns>
        protected async Task<NetworkResponse> GetNetworkResponseAsync(
            Uri requestUri,
            HttpRequestMessage request,
            bool showActivity)
        {
            HttpResponseMessage response = null;
            var activityId = this.RegisterActivity(requestUri, showActivity);

            try
            {
                response = await this.httpClient.SendAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //Logger.Current.LogWarning(
                //    string.Format(
                //        "Error getting network response from {0}. Error: {1}",
                //        requestUri.OriginalString,
                //        ex.Message));

                return null;
            }
            finally
            {
                var statusCode = response == null ? null : response.StatusCode.ToString();
                this.UnregisterActivity(activityId, showActivity, statusCode);
            }

            return response.ToNetworkResponse();
        }

        /// <summary>
        /// Gets the successful network response with the specified result.
        /// </summary>
        /// <typeparam name="T">The type of the response</typeparam>
        /// <param name="response">The response.</param>
        /// <returns>The successful network response with the specified result.</returns>
        protected NetworkResponse<T> GetSuccessfulNetworkResponse<T>(T response) where T : class
        {
            return new NetworkResponse<T>(true, HttpStatusCode.OK, NetworkResponseStatus.Success, "OK", response);
        }

        /// <summary>
        /// Registers the service activity.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="showActivity">if set to <c>true</c> show activity.</param>
        /// <returns>The activity ID.</returns>
        protected long RegisterActivity(Uri requestUri, bool showActivity)
        {
            lock (this.requestIdLock)
            {
                this.requestId++;

                //Logger.Current.LogInformation(
                //    string.Format("CS: Network request {0} to {1}", this.requestId, requestUri.OriginalString));

                if (showActivity && this.activityReportingService != null)
                {
                    this.activityReportingService.StartedNonBlockingOperation(
                        string.Format(ActivityOperationIdTemplate, this.requestId));
                }

                return this.requestId;
            }
        }

        /// <summary>
        /// Unregisters the service activity.
        /// </summary>
        /// <param name="activityId">The activity ID.</param>
        /// <param name="showActivity">if set to <c>true</c> show activity.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        protected void UnregisterActivity(long activityId, bool showActivity, string statusCode)
        {
            //Logger.Current.LogInformation(
            //    string.Format("Network request {0} completed with status code {1}", activityId, statusCode));

            if (showActivity && this.activityReportingService != null)
            {
                this.activityReportingService.FinishedNonBlockingOperation(
                    string.Format(ActivityOperationIdTemplate, activityId));
            }
        }

        #endregion Methods
    }
}
