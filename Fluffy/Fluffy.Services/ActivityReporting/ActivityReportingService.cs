// <copyright file="ActivityReportingService.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Services.ActivityReporting
{
    using System;
    using System.Threading;
    using Fluffy.Common;
    using Fluffy.Common.Interfaces;

    /// <summary>
    /// Implementation of the activity reporting service.
    /// </summary>
    public class ActivityReportingService : IActivityReportingService
    {
        #region Fields

        /// <summary>
        /// Number of blocking operations in progress.
        /// </summary>
        private int blockingOperations;

        /// <summary>
        /// The number of non-blocking operations in progress.
        /// </summary>
        private int nonBlockingOperations;

        #endregion Fields

        #region Events

        /// <summary>
        /// Occurs when the data is loaded.
        /// </summary>
        public event EventHandler<ActivityReportingServiceStatusChangedEventArgs> StatusChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a value indicating whether there is a UI blocking operation in progress.
        /// </summary>
        /// <value><c>true</c> if there is a UI blocking operation in progress; otherwise, <c>false</c>.</value>
        public bool IsBlockingOperationInProgress
        {
            get
            {
                return this.blockingOperations > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is a non UI blocking operation in progress.
        /// </summary>
        /// <value><c>true</c> if there is a non UI blocking operation in progress; otherwise, <c>false</c>.</value>
        public bool IsNonBlockingOperationInProgress
        {
            get
            {
                return this.nonBlockingOperations > 0;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reports the beginning of a UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        public void StartedBlockingOperation(string operationId)
        {
            //Logger.Current.LogInformation(string.Format("AO: Started a UI blocking operation: {0}", operationId));
            Interlocked.Increment(ref this.blockingOperations);
            this.OnStatusChanged();
        }

        /// <summary>
        /// Reports the end of a UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        public void FinishedBlockingOperation(string operationId)
        {
            //Logger.Current.LogInformation(string.Format("AO: Finished a UI blocking operation: {0}", operationId));

            if (this.blockingOperations > 0)
            {
                Interlocked.Decrement(ref this.blockingOperations);
            }

            this.OnStatusChanged();
        }

        /// <summary>
        /// Reports the beginning of a non UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        public void StartedNonBlockingOperation(string operationId)
        {
            //Logger.Current.LogInformation(string.Format("AO: Started a non UI blocking operation: {0}", operationId));
            Interlocked.Increment(ref this.nonBlockingOperations);
            this.OnStatusChanged();
        }

        /// <summary>
        /// Reports the end of a non UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        public void FinishedNonBlockingOperation(string operationId)
        {
            //Logger.Current.LogInformation(string.Format("AO: Finished a non UI blocking operation: {0}", operationId));

            if (this.nonBlockingOperations > 0)
            {
                Interlocked.Decrement(ref this.nonBlockingOperations);
            }

            this.OnStatusChanged();
        }

        /// <summary>
        /// Raises the <see cref="E:StatusChanged" /> event.
        /// </summary>
        private void OnStatusChanged()
        {
            var args = new ActivityReportingServiceStatusChangedEventArgs(
                this.IsBlockingOperationInProgress,
                this.IsNonBlockingOperationInProgress);

            var handler = this.StatusChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion Methods
    }
}
