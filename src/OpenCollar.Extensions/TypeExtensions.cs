/*
 * This file is part of OpenCollar.Extensions.
 *
 * OpenCollar.Extensions is free software: you can redistribute it
 * and/or modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or (at your
 * option) any later version.
 *
 * OpenCollar.Extensions is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public
 * License for more details.
 *
 * You should have received a copy of the GNU General Public License along with
 * OpenCollar.Extensions.  If not, see <https://www.gnu.org/licenses/>.
 *
 * Copyright © 2019-2020 Jonathan Evans (jevans@open-collar.org.uk).
 */

using System;
using System.Collections.Generic;
using System.IO;

using JetBrains.Annotations;

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     Methods that extend the <see cref="Type" /> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets the full path to directory in which resides the assembly in which the type is defined.
        /// </summary>
        /// <param name="type">
        ///     The type for which to return the assembly directory path.
        /// </param>
        /// <returns>
        ///     The full path to the directory in which resides the assembly in which the type is defined, or if the
        ///     assembly is dynamic or <paramref name="type" /> is <see langword="null" /> then an empty string is returned.
        /// </returns>
        [ContractAnnotation("null=>null;notnull=>notnull")]
        public static string GetAssemblyDirectoryPath([CanBeNull] this Type type)
        {
            if(ReferenceEquals(type, null) || type.Assembly.IsDynamic)
            {
                return string.Empty;
            }

            return Path.GetDirectoryName((new Uri(type.Assembly.CodeBase)).LocalPath);
        }

        /// <summary>
        ///     Gets the full path to the assembly in which the type is defined.
        /// </summary>
        /// <param name="type">
        ///     The type for which to return the assembly path.
        /// </param>
        /// <returns>
        ///     The full path to the assembly in which the type is defined, or if the assembly is dynamic or
        ///     <paramref name="type" /> is <see langword="null" /> then an empty string is returned.
        /// </returns>
        [NotNull]
        public static string GetAssemblyPath([CanBeNull] this Type type)
        {
            if(ReferenceEquals(type, null) || type.Assembly.IsDynamic)
            {
                return string.Empty;
            }

            return (new Uri(type.Assembly.CodeBase)).LocalPath;
        }

        /// <summary>
        ///     Gets the full path to a file or directory relative to the directory in which resides the assembly in
        ///     which the type is defined.
        /// </summary>
        /// <param name="type">
        ///     The type for which to return the assembly directory path. If <see langword="null" /> then
        ///     <see langword="null" /> is returned
        /// </param>
        /// <param name="pathFragments">
        ///     The fragments of the path to append to root directory in which the assembly resides.
        /// </param>
        /// <returns>
        ///     The full path to a file or directory relative to the directory in which resides the assembly in which
        ///     the type is defined, or if the assembly is dynamic or <paramref name="type" /> is
        ///     <see langword="null" /> then an the relative version of the path given is returned.
        /// </returns>
        [ContractAnnotation("type:null=>null;type:notnull=>notnull")]
        public static string GetAssemblyRelativePath([CanBeNull] this Type type, [CanBeNull] params string[] pathFragments)
        {
            if(ReferenceEquals(type, null) || type.Assembly.IsDynamic)
            {
                if(ReferenceEquals(pathFragments, null) || (pathFragments.Length <= 0))
                {
                    return string.Empty;
                }

                return Path.Combine(pathFragments);
            }

            var directoryPath = Path.GetDirectoryName((new Uri(type.Assembly.CodeBase)).LocalPath);

            if(ReferenceEquals(pathFragments, null) || (pathFragments.Length <= 0))
            {
                return directoryPath;
            }

            var combine = new List<string>
            {
                directoryPath
            };
            combine.AddRange(pathFragments);

            return Path.Combine(combine.ToArray());
        }
    }
}