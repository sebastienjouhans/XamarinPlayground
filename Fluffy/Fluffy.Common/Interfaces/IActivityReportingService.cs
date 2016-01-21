// <copyright file="IActivityReportingService.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Interfaces
{
    using System;

    /// <summary>
    /// Definition of the activity reporting service.
    /// </summary>
    public interface IActivityReportingService
    {
        #region Events

        /// <summary>
        /// Occurs when the data is loaded.
        /// </summary>
        event EventHandler<ActivityReportingServiceStatusChangedEventArgs> StatusChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a value indicating whether a UI blocking operation in progress.
        /// </summary>
        /// <value><c>true</c> if a UI blocking operation in progress; otherwise, <c>false</c>.</value>
        bool IsBlockingOperationInProgress
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether a non UI blocking operation in progress.
        /// </summary>
        /// <value><c>true</c> if a non UI blocking operation in progress; otherwise, <c>false</c>.</value>
        bool IsNonBlockingOperationInProgress
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reports the beginning of a UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        void StartedBlockingOperation(string operationId);

        /// <summary>
        /// Reports the end of a UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        void FinishedBlockingOperation(string operationId);

        /// <summary>
        /// Reports the beginning of a non UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        void StartedNonBlockingOperation(string operationId);

        /// <summary>
        /// Reports the end of a non UI blocking operation.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        void FinishedNonBlockingOperation(string operationId);

        #endregion Methods
    }
}
