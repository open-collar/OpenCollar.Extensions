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
using System.Threading;

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
#pragma warning disable S3966 // Objects should not be disposed more than once

    public class DisposableTest
    {
        [Fact]
        public void CheckDisposeThreadSafety()
        {
            var d = new DisposableMock
            {
                FailOnDispose = true
            };
            Assert.False(d.IsDisposed);
            Assert.Equal(0, d.CallCount);
            d.LockEvent.Reset();
            for(var n = 0; n < 10; ++n)
            {
                var t = new Thread(() => { d.Dispose(); })
                {
                    Name = $@"Dispose thread {n}.",
                    IsBackground = true
                };
                t.Start();
            }

            // Allow one second for all the threads to block.
            Thread.Sleep(1000);
            d.LockEvent.Set();
            Assert.True(d.IsDisposed);
            Assert.Equal(1, d.CallCount);
        }

        [Fact]
        public void CheckNotDisposedThrows()
        {
            var d = new DisposableMock();
            Assert.False(d.IsDisposed);
            d.PerformCheck();
            d.Dispose();
            Assert.True(d.IsDisposed);
            Assert.Throws<ObjectDisposedException>(() => d.PerformCheck());
        }

        [Fact]
        public void DisposableIsDisposedOf()
        {
            var d = new DisposableMock();
            Assert.False(d.IsDisposed);
            d.Dispose();
            Assert.True(d.IsDisposed);
            Assert.True(d.Disposing);
        }

        [Fact]
        public void DisposableOnlyCalledOnce()
        {
            var d = new DisposableMock();
            Assert.False(d.IsDisposed);
            Assert.Equal(0, d.CallCount);
            d.Dispose();
            Assert.True(d.IsDisposed);
            Assert.Equal(1, d.CallCount);
            d.Dispose();
            Assert.True(d.IsDisposed);
            Assert.Equal(1, d.CallCount);
        }

        [Fact]
        public void DisposeNeverThrowsExceptions()
        {
            var d = new DisposableMock
            {
                FailOnDispose = true
            };
            d.Dispose();
            Assert.True(d.IsDisposed);
            Assert.Equal(1, d.CallCount);
        }
    }
}