// <copyright file="ILogObserver.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Interfaces
{
    /// <summary>
    /// The interface for a log observer.
    /// </summary>
    public interface ILogObserver
    {
        /// <summary>
        /// Consumes the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        void ConsumeEntry(LogEntry logEntry);
    }
}
