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

using JetBrains.Annotations;

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     The central exception manager used to report and receive all unhandled exceptions in the application.
    /// </summary>
    public static class ExceptionManager
    {
        /// <summary>
        ///     An event that is raised every time an unhandled exception is reported.
        /// </summary>
        public static event EventHandler<UnhandledExceptionEventArgs>? UnhandledException;

        /// <summary>
        ///     Determines whether there are any registered exception handlers.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if [has registered handlers]; otherwise, <see langword="false" />.
        /// </returns>
        public static bool HasRegisteredHandlers()
        {
            var eventHandler = UnhandledException;
            if(ReferenceEquals(eventHandler, null))
            {
                return false;
            }

            var callbacks = eventHandler.GetInvocationList();
            return callbacks.Length > 0;
        }

        /// <summary>
        ///     Called when an exception cannot be handled.
        /// </summary>
        /// <param name="exception">
        ///     The exception that cannot be handled.
        /// </param>
        public static void OnUnhandledException([CanBeNull] Exception exception)
        {
            // Ignore calls when there are no exception details to report.
            if(ReferenceEquals(exception, null))
            {
                return;
            }

            // Ignore the call if there is no-one to whom to report the exception.
            var eventHandler = UnhandledException;
            if(ReferenceEquals(eventHandler, null))
            {
                return;
            }

            // And again, ignore the call if there is no-one to whom to report the exception.
            var callbacks = eventHandler.GetInvocationList();
            if(ReferenceEquals(callbacks, null) || (callbacks.Length <= 0))
            {
                return;
            }

            // There is definitely someone listening, so create the arguments to pass in the
            var args = new UnhandledExceptionEventArgs(exception);
            var parameters = new object[] { null, args };
            foreach(var callback in callbacks)
            {
                try
                {
                    callback.DynamicInvoke(parameters);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch
                {
                    // All errors are deliberately sunk in the error manager.
                }
#pragma warning restore CA1031 // Do not catch general exception types

                if(args.Handled)
                {
                    break;
                }
            }
        }
    }
}