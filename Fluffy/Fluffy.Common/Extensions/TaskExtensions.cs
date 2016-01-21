// <copyright file="TaskExtensions.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Extensions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Contains common task extensions.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Forgets the specified task.
        /// </summary>
        /// <param name="task">The task.</param>
        public static void Forget(this Task task)
        {
            // This method suppresses the following warning:
            // Because this call is not awaited, execution of the current method continues before the call is completed.
            // Consider applying the 'await' operator to the result of the call.
            // The solution is explained here:
            // http://stackoverflow.com/questions/22629951/suppressing-warning-cs4014-because-this-call-is-not-awaited-execution-of-the
            //
            // Microsoft also have this implementation in the TplExtensions found here:
            // http://msdn.microsoft.com/en-us/library/microsoft.visualstudio.threading.tplextensions.forget.aspx
        }
    }
}
