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

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
    public sealed class ExceptionManagerTest
    {
        [Fact]
        public void CheckCalls()
        {
            var ex = new Exception();
            var handlerCount = 0;

            // There should be no impact calling the exception manager with no handlers.
            ExceptionManager.OnUnhandledException(ex);
            UnhandledExceptionEventArgs args = null;
            ExceptionManager.UnhandledException += (src, a) =>
            {
                args = a;
                ++handlerCount;
            };
            ExceptionManager.OnUnhandledException(ex);
            Assert.NotNull(args);
            Assert.NotNull(args.Exception);
            Assert.Same(ex, args.Exception);

            // Exceptions thrown by handlers are ignored.
            ExceptionManager.UnhandledException += (src, a) =>
            {
                ++handlerCount;
                throw new Exception();
            };
            handlerCount = 0;
            ExceptionManager.OnUnhandledException(ex);

            // One of the handlers will throw an execption, but we won't hear about it and all other handlers are called.
            Assert.Equal(2, handlerCount);

            // Add some more handlers.
            ExceptionManager.UnhandledException += (src, a) =>
            {
                ++handlerCount;
                a.Handled = true;
            };
            ExceptionManager.UnhandledException += (src, a) => { ++handlerCount; };
            handlerCount = 0;
            ExceptionManager.OnUnhandledException(ex);
            Assert.Equal(3, handlerCount); // The last handler to be added isn't called because the previous one flags handled.
        }
    }
}