// <copyright file="IStorageNamingService.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Interfaces
{
    /// <summary>
    /// Definition of the storage naming service.
    /// </summary>
    public interface IStorageNamingService
    {
        #region Methods

        /// <summary>
        /// Gets the folder name for the specified data store.
        /// </summary>
        /// <param name="storageFolder">The storage folder.</param>
        /// <returns>The folder name.</returns>
        string GetFolderName(StorageFolder storageFolder);

        /// <summary>
        /// Gets the file name for the specified data file.
        /// </summary>
        /// <param name="storageFile">The storage file.</param>
        /// <returns>The file name.</returns>
        string GetFileName(StorageFile storageFile);

        /// <summary>
        /// Gets the path to the specified file.
        /// </summary>
        /// <param name="storageFolder">The storage folder.</param>
        /// <param name="storageFile">The storage file.</param>
        /// <returns>The path.</returns>
        string GetPath(StorageFolder storageFolder, StorageFile storageFile);

        /// <summary>
        /// Gets the absolute path to the specified file.
        /// </summary>
        /// <param name="storageFolder">The storage folder.</param>
        /// <param name="storageFile">The storage file.</param>
        /// <returns>The absolute path.</returns>
        string GetAbsolutePath(StorageFolder storageFolder, StorageFile storageFile);
        
        #endregion Methods
    }
}
