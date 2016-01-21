// <copyright file="ActivityReportingServiceStatusChangedEventArgs.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common
{
    /// <summary>
    /// Represents arguments of StatusChanged event of the ActivityReportingService.
    /// </summary>
    public class ActivityReportingServiceStatusChangedEventArgs
    {
        #region Fields

        /// <summary>
        /// A value indicating whether there is a UI blocking operation in progress.
        /// </summary>
        private readonly bool isBlockingOperationInProgress;

        /// <summary>
        /// A value indicating whether there is a non UI blocking operation in progress.
        /// </summary>
        private readonly bool isNonBlockingOperationInProgress;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ActivityReportingServiceStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="isBlockingOperationInProgress">A value indicating whether there is a UI blocking operation in progress.</param>
        /// <param name="isNonBlockingOperationInProgress">A value indicating whether there is a non UI blocking operation in progress.</param>
        public ActivityReportingServiceStatusChangedEventArgs(bool isBlockingOperationInProgress, bool isNonBlockingOperationInProgress)
        {
            this.isBlockingOperationInProgress = isBlockingOperationInProgress;
            this.isNonBlockingOperationInProgress = isNonBlockingOperationInProgress;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether there is a UI blocking operation in progress.
        /// </summary>
        /// <value><c>true</c> if there is a UI blocking operation in progress; otherwise, <c>false</c>.</value>
        public bool IsBlockingOperationInProgress
        {
            get
            {
                return this.isBlockingOperationInProgress;
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
                return this.isNonBlockingOperationInProgress;
            }
        }

        #endregion Properties
    }
}
