// <copyright file="NumberExtensions.cs" company="R/GA">
//     Copyright (c) Telefonica. All rights reserved.
// </copyright>
// <summary></summary>
namespace Fluffy.Common.Extensions
{
    using System;

    /// <summary>
    /// Contains common number extensions.
    /// </summary>
    public static class NumberExtensions
    {
        #region Constants

        /// <summary>
        /// The number of meters in mile.
        /// </summary>
        private const double MetersInMile = 1609.344;

        #endregion Constants

        #region Methods

        /// <summary>
        /// Converts meters to miles.
        /// </summary>
        /// <param name="meters">The meters.</param>
        /// <returns>The miles.</returns>
        public static double MetersToMiles(this double meters)
        {
            return meters / MetersInMile;
        }

        /// <summary>
        /// Converts meters to miles.
        /// </summary>
        /// <param name="meters">The meters.</param>
        /// <returns>The miles.</returns>
        public static double MetersToMiles(this int? meters)
        {
            return meters.HasValue ? ((double)meters).MetersToMiles() : 0.0;
        }

        /// <summary>
        /// Converts an angle to a radian.
        /// </summary>
        /// <param name="input">The angle that is to be converted.</param>
        /// <returns>The angle in radians.</returns>
        public static double ToRad(this double input)
        {
            return input * (Math.PI / 180);
        }

        #endregion Methods
    }
}
