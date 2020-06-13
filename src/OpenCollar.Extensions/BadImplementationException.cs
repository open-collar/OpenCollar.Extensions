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
using System.Diagnostics;
using System.Runtime.Serialization;

using JetBrains.Annotations;

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     Exception thrown when a badly implemented override returns an invalid value.
    /// </summary>
    [Serializable]
    [DebuggerDisplay(@"BadImplementationException: {Message}")]
    public class BadImplementationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BadImplementationException" /> class.
        /// </summary>
        public BadImplementationException()
        { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BadImplementationException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public BadImplementationException([CanBeNull] string message) : base(message)
        { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BadImplementationException" /> class with a specified error
        ///     message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic)
        ///     if no inner exception is specified.
        /// </param>
        public BadImplementationException([CanBeNull] string message, [CanBeNull] Exception innerException) : base(
                                                                                                                   message, innerException)
        { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BadImplementationException" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data
        ///     about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information
        ///     about the source or destination.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="info" /> parameter is null.
        /// </exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        ///     The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).
        /// </exception>
        protected BadImplementationException([NotNull] SerializationInfo info, StreamingContext context) : base(info,
                                                                                                                context)
        { }
    }
}