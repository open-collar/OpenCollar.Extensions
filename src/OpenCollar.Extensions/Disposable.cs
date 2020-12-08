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
using System.Threading;

namespace OpenCollar.Extensions
{
#pragma warning disable S3881 // Fix this implementation of 'IDisposable' to conform to the dispose pattern.

    /// <summary>
    ///     A base class for objects that implements <see cref="IDisposable" /> and the
    ///     <see href="https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose#implement-the-dispose-pattern">Dispose Pattern</see>
    ///     in a thread-safe way.
    /// </summary>
    /// <remarks>
    ///     No finalizer is implemented - this is left to the few consumers that will actually need it; but, the finalizer
    ///     is suppressed if the <see cref="IDisposable.Dispose()"/> method is called.  If your class implements a finalizer
    ///     it should call the <see cref="Dispose(bool)"/> method with the <c>disposing</c> argument set to <see langword="false"/>.
    /// </remarks>
    [Serializable]
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        ///     The value of the <see cref="_isDisposed" /> field if the object has been disposed of.
        /// </summary>
        [Browsable(false)]
        [Category(@"__internal")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized]
        private const int Disposed = 1;

        /// <summary>
        ///     The value of the <see cref="_isDisposed" /> field if the object has NOT been disposed of.
        /// </summary>
        [Browsable(false)]
        [Category(@"__internal")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized]
        private const int NotDisposed = 0;

        /// <summary>
        ///     A field used to track whether or this instance has been disposed of (see <see cref="Disposed" /> and
        ///     <see cref="NotDisposed" /> for potential values). Access should be made thread-safe by using the
        ///     <see cref="System.Threading.Interlocked" /> class to control access.
        /// </summary>
        [Browsable(false)]
        [Category(@"__internal")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized]
        private int _isDisposed;

        /// <summary>
        ///     Gets a value indicating whether this instance has been disposed of.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance has been disposed of; otherwise, <see langword="false" />.
        /// </value>
        [Browsable(false)]
        [Category(@"__internal")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("Gets a value indicating whether this instance has been disposed of.")]
        public bool IsDisposed => _isDisposed == Disposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// Any exceptions thrown by the implementation of the <see cref="Dispose(bool)"/> method will be caught and
        /// not re-thrown (see 
        /// <see href="https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1065?view=vs-2019">CA1065: Do not raise exceptions in unexpected locations</see>
        /// for more details).
        /// </remarks>
#pragma warning disable CA1063 // Implement IDisposable Correctly
        public void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
            var wasDisposed = Interlocked.CompareExchange(ref _isDisposed, Disposed, NotDisposed);
            if(wasDisposed == Disposed)
            {
                return;
            }

            try
            {
                Dispose(true);
            }

            // ReSharper disable once EmptyGeneralCatchClause
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                // Dispose must not throw exceptions.
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Checks that the object has not been disposed of, and throws an exception if it has.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        ///     Object cannot be accessed after it has been disposed of.
        /// </exception>
        public void CheckNotDisposed()
        {
            if(_isDisposed == Disposed)
            {
                throw new ObjectDisposedException(Resources.Exceptions.Disposable_CannotUseDisposedObject);
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to
        ///     release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}