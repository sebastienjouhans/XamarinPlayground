// <copyright file="NetworkResponse.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents the network response returning the status only.
    /// </summary>
    public class NetworkResponse
    {
        #region Fields

        /// <summary>
        /// A value indicating whether the response is successful.
        /// </summary>
        private readonly bool isSuccessful;

        /// <summary>
        /// The status code.
        /// </summary>
        private readonly HttpStatusCode statusCode;

        /// <summary>
        /// The status.
        /// </summary>
        private readonly NetworkResponseStatus status;

        /// <summary>
        /// The status phrase.
        /// </summary>
        private readonly string statusPhrase;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="NetworkResponse"/> class.
        /// </summary>
        /// <param name="isSuccessful">if set to <c>true</c> the response is successful.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="status">The status.</param>
        /// <param name="statusPhrase">The status phrase.</param>
        public NetworkResponse(
            bool isSuccessful,
            HttpStatusCode statusCode,
            NetworkResponseStatus status,
            string statusPhrase)
        {
            this.isSuccessful = isSuccessful;
            this.statusCode = statusCode;
            this.status = status;
            this.statusPhrase = statusPhrase;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the response is successful.
        /// </summary>
        /// <value><c>true</c> if the response is successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful
        {
            get
            {
                return this.isSuccessful;
            }
        }

        /// <summary>
        /// Gets the status phrase.
        /// </summary>
        /// <value>The status phrase.</value>
        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public NetworkResponseStatus Status
        {
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Gets the status phrase.
        /// </summary>
        /// <value>The status phrase.</value>
        public string StatusPhrase
        {
            get
            {
                return this.statusPhrase;
            }
        }

        #endregion Properties
    }

    /// <summary>
    /// Represents the network response returning the result of the specified type.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class NetworkResponse<TResponse> : NetworkResponse, IDisposable where TResponse : class
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="NetworkResponse{TResponse}" /> class.
        /// </summary>
        /// <param name="isSuccessful">The is successful.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="status">The status.</param>
        /// <param name="statusPhrase">The status phrase.</param>
        /// <param name="response">The response.</param>
        public NetworkResponse(bool isSuccessful, HttpStatusCode statusCode, NetworkResponseStatus status, string statusPhrase, TResponse response)
            : base(isSuccessful, statusCode, status, statusPhrase)
        {
            this.Response = response;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>The response.</value>
        public TResponse Response { get; private set; }

        #endregion Properties

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
            if (!disposing)
            {
                return;
            }

            if (!(this.Response is IDisposable))
            {
                return;
            }

            (this.Response as IDisposable).Dispose();
            this.Response = null;
        }
        #endregion Methods
    }
}
