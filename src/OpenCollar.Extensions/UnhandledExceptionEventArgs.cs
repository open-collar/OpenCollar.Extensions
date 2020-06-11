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
using System.ComponentModel;

using JetBrains.Annotations;

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     A class representing the argument supplied when an unhandled exception is reported. Use the
    ///     <see cref="HandledEventArgs.Handled" /> property to indicate that the event has been handled and no further
    ///     callbacks should be raised.
    /// </summary>
    public sealed class UnhandledExceptionEventArgs : HandledEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnhandledExceptionEventArgs" /> class.
        /// </summary>
        /// <param name="exception">
        ///     The exception that will be reported. Must not be <see langword="null" />.
        /// </param>
        internal UnhandledExceptionEventArgs([NotNull] Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        ///     Gets the exception that could not be handled.
        /// </summary>
        /// <value>
        ///     The exception that could not be handled. Will never be <see langword="null" />.
        /// </value>
        [NotNull]
        public Exception Exception { get; }
    }
}