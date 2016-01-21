// <copyright file="IStorageService.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Definition of the storage service.
    /// </summary>
    public interface IStorageService
    {
        #region Methods

        /// <summary>
        /// Determines whether the specified folder exists.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns><c>true</c> if the specified folder exists, <c>false</c> otherwise.</returns>
        bool FolderExists(string path);

        /// <summary>
        /// Determines whether the specified folder exists asynchronously.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns>The task returning <c>true</c> if the specified folder exists, <c>false</c> otherwise.</returns>
        Task<bool> FolderExistsAsync(string path);

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns><c>true</c> if the specified file exists, <c>false</c> otherwise.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Determines whether the specified file exists asynchronously.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The task returning <c>true</c> if the specified file exists, <c>false</c> otherwise.</returns>
        Task<bool> FileExistsAsync(string path);

        /// <summary>
        /// Creates the specified folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool CreateFolder(string path);

        /// <summary>
        /// Gets the stream for the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The stream.</returns>
        Stream GetStream(string path);

        /// <summary>
        /// Gets the stream for appending to the the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The stream.</returns>
        Stream GetStreamForAppending(string path);

        /// <summary>
        /// Saves the contents of the stream to the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool SaveStream(string path, Stream stream);

        /// <summary>
        /// Saves the content to the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="content">The content.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool SaveFile(string path, string content);

        /// <summary>
        /// Reads the content of the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The content.</returns>
        string ReadFile(string path);

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool DeleteFolder(string path);

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool DeleteFile(string path);

        /// <summary>
        /// Deletes the files in the folder according to the specified filter.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="filter">The filter.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool DeleteFiles(string folder, string filter);

        /// <summary>
        /// Copies the specified folder to the target path.
        /// </summary>
        /// <param name="sourcePath">The path of the source folder.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool CopyFolder(string sourcePath, string targetPath);

        /// <summary>
        /// Copies the specified file to the target path.
        /// </summary>
        /// <param name="sourcePath">The path of the source file.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool CopyFile(string sourcePath, string targetPath);

        /// <summary>
        /// Moves the specified folder to the target path.
        /// </summary>
        /// <param name="sourcePath">The path of the source folder.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool MoveFolder(string sourcePath, string targetPath);

        /// <summary>
        /// Renames the specified file.
        /// </summary>
        /// <param name="sourcePath">The path of the source file.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool RenameFile(string sourcePath, string targetPath);

        /// <summary>
        /// Moves the specified file to the target path.
        /// </summary>
        /// <param name="sourcePath">The path of the source file.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool MoveFile(string sourcePath, string targetPath);

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The creation time if the file exists, null otherwise.</returns>
        DateTimeOffset? GetFileCreationTime(string path);

        /// <summary>
        /// Gets the last write time of the file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The last write time if the file exists, null otherwise.</returns>
        DateTimeOffset? GetFileLastWriteTime(string path);

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The size of the file.</returns>
        long? GetFileSize(string path);

        /// <summary>
        /// Finds the files according to the specified filter.
        /// </summary>
        /// <param name="parentPath">The parent path.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="getFullPath">if set to <c>true</c> returns the full path of the file.</param>
        /// <returns>The list of found files.</returns>
        IList<string> FindFiles(string parentPath, string filter, bool getFullPath);

        #endregion Methods
    }
}
