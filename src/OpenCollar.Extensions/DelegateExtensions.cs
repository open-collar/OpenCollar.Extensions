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

using OpenCollar.Extensions.Validation;

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     A delegate used to generate event args for a safe call to a delegate.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the event args that will be returned.
    /// </typeparam>
    /// <returns>
    ///     A instance of the event args to pass to the delegate when invoked.
    /// </returns>
    public delegate T EventArgsFactory<out T>() where T : EventArgs;

    /// <summary>
    ///     Extensions to the delegate class.
    /// </summary>
    public static class DelegateExtensions
    {
        /// <summary>
        ///     Gets the delegate description.
        /// </summary>
        /// <param name="delegate">
        ///     The delegate to describe.
        /// </param>
        /// <returns>
        ///     A description of the type and method specified by the delegate.
        /// </returns>
        [NotNull]
        public static string GetDescription([CanBeNull] this Delegate @delegate)
        {
            if(@delegate == null)
            {
                return @"[null]";
            }

            if(@delegate.Method.DeclaringType == null)
            {
                return $@"[no type].{@delegate.Method}";
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            return $@"[{@delegate.Method.DeclaringType.FullName}].{@delegate.Method}";
        }

        /// <summary>
        ///     Invokes the delegate given (if not <see langword="null" />) with protection against exceptions thrown by
        ///     the invoked methods.
        /// </summary>
        /// <param name="handler">
        ///     The delegate to call.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event being raised.
        /// </param>
        /// <param name="sender">
        ///     The object to pass as the sender.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if at least one delegate was successfully invoked, <see langword="false" /> otherwise.
        /// </returns>
        public static bool SafeInvoke([CanBeNull] this Delegate handler, [NotNull] string eventName, [CanBeNull] object sender)
        {
            return SafeInvoke(handler, eventName, sender, () => EventArgs.Empty, ArgsUsageKind.Reuse);
        }

        /// <summary>
        ///     Invokes the delegate given (if not <see langword="null" />) with protection against exceptions thrown by
        ///     the invoked methods.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the event args to pass.
        /// </typeparam>
        /// <param name="handler">
        ///     The delegate to call.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event being raised.
        /// </param>
        /// <param name="sender">
        ///     The object to pass as the sender.
        /// </param>
        /// <param name="eventArgFactory">
        ///     A factory for generating event args.
        /// </param>
        /// <param name="usage">
        ///     The way in which to use the factory to generate args.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if at least one delegate was successfully invoked, <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Invalid value passed in the <paramref name="usage" /> argument.
        /// </exception>
        public static bool SafeInvoke<T>([CanBeNull] this Delegate handler, [NotNull] string eventName, [CanBeNull] object sender,
            EventArgsFactory<T> eventArgFactory, ArgsUsageKind usage) where T : EventArgs
        {
            eventName.Validate(nameof(eventName), StringIs.NotNullEmptyOrWhiteSpace);
            eventArgFactory.Validate(nameof(eventArgFactory), ObjectIs.NotNull);
            if(ReferenceEquals(handler, null))
            {
                return false;
            }

            var delegates = handler.GetInvocationList();

            object[]? args = null;
            bool generateArgs;
            switch(usage)
            {
                case ArgsUsageKind.Reuse:
                    args = new[] { sender, eventArgFactory() };
                    generateArgs = false;
                    break;

                case ArgsUsageKind.UniqueInstance:
                    generateArgs = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(usage), usage, string.Format(Resources.Exceptions.DelegateExtensions_InvalidUsage, nameof(usage), System.Globalization.CultureInfo.CurrentCulture));
            }

            var raised = false;
            foreach(var callback in delegates)
            {
                try
                {
                    if(generateArgs)
                    {
                        args = new[] { sender, eventArgFactory() };
                    }

                    callback.DynamicInvoke(args);
                    raised = true;
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch(Exception ex)
                {
                    ex.Data.Add("CallingDelegate", GetDescription(callback));
                    ExceptionManager.OnUnhandledException(ex);
                }
#pragma warning restore CA1031 // Do not catch general exception types
            }

            return raised;
        }
    }
}