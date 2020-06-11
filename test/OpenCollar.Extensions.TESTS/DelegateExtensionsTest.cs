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
using System.Text;

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
#pragma warning disable S3264 // Events should be invoked

    public sealed class TestEventArgs : EventArgs
    {
        public string Id { get; set; }
    }

    public sealed class DelegateExtensionsTest
    {
        private event EventHandler<TestEventArgs> TestEvent1;

        [Fact]
        public void CheckNormalUsage()
        {
            // TestEvent1

            var handled = false;
            var id = Guid.NewGuid().ToString("D");
            var sender = new object();

            EventHandler<TestEventArgs> handler = (x, args) => { handled = true; Assert.Equal(id, args.Id); Assert.Same(sender, x); };

            TestEvent1.SafeInvoke(nameof(TestEvent1), sender, () => new TestEventArgs()
            {
                Id = id
            }, ArgsUsageKind.Reuse);

            Assert.False(handled);

            TestEvent1 += handler;

            TestEvent1.SafeInvoke(nameof(TestEvent1), sender, () => new TestEventArgs()
            {
                Id = id
            }, ArgsUsageKind.Reuse);

            Assert.True(handled);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                TestEvent1.SafeInvoke(nameof(TestEvent1), sender, () => new TestEventArgs()
                {
                    Id = id
                }, ArgsUsageKind.Unknown);
            });

            TestEvent1 -= handler;

            handled = false;

            TestEvent1.SafeInvoke(nameof(TestEvent1), sender, () => new TestEventArgs()
            {
                Id = id
            }, ArgsUsageKind.Reuse);

            Assert.False(handled);
        }

        private event EventHandler<TestEventArgs> TestEvent2;

        [Fact]
        public void CheckSingleUseArgs()
        {
            // TestEvent2

            var handled = false;
            var sender = new object();

            EventHandler<TestEventArgs> handler = (x, args) => { handled = true; };

            var argCount = 0;

            TestEvent2.SafeInvoke(nameof(TestEvent2), sender, () =>
            {
                ++argCount;
                return new TestEventArgs();
            }, ArgsUsageKind.UniqueInstance);

            Assert.Equal(0, argCount);

            TestEvent2 += handler;

            TestEvent2.SafeInvoke(nameof(TestEvent2), sender, () =>
          {
              ++argCount;
              return new TestEventArgs();
          }, ArgsUsageKind.UniqueInstance);

            Assert.True(handled);

            handled = false;

            TestEvent2.SafeInvoke(nameof(TestEvent2), sender, () =>
            {
                ++argCount;
                return new TestEventArgs();
            }, ArgsUsageKind.UniqueInstance);

            TestEvent2 -= handler;

            Assert.Equal(2, argCount);
        }

        private event EventHandler<TestEventArgs> TestEvent3;

        [Fact]
        public void CheckNoErrors()
        {
            // TestEvent3

            var sender = new object();
            var handled = false;

            EventHandler<TestEventArgs> handler1 = (x, args) => { throw new Exception(); };
            EventHandler<TestEventArgs> handler2 = (x, args) => { handled = true; };

            var argCount = 0;

            TestEvent3 += handler1;
            TestEvent3 += handler2;

            TestEvent3.SafeInvoke(nameof(TestEvent3), sender, () =>
            {
                ++argCount;
                return new TestEventArgs();
            }, ArgsUsageKind.UniqueInstance);

            Assert.True(handled);
        }

        private event EventHandler TestEvent4;

        [Fact]
        public void CheckNoArgs()
        {
            // TestEvent34

            var sender = new object();
            var handled = false;

            EventHandler handler1 = (x, eventArgs) =>
            {
                Assert.Equal(EventArgs.Empty, eventArgs);
                handled = true;
            };

            TestEvent4 += handler1;

            TestEvent4.SafeInvoke(nameof(TestEvent4), sender);

            Assert.True(handled);
        }

        private event EventHandler<TestEventArgs> TestEvent5;

        [Fact]
        public void CheckGetDescription()
        {
            TestEvent5 += OnCheckGetDescription;

            Assert.Equal(@"[OpenCollar.Extensions.TESTS.DelegateExtensionsTest].Void OnCheckGetDescription(System.Object, OpenCollar.Extensions.TESTS.TestEventArgs)", TestEvent5.GetDescription());
            Assert.Equal(@"[null]", ((Delegate)null).GetDescription());
        }

        private void OnCheckGetDescription(object sender, TestEventArgs e)
        {
            // Nothing required.
        }
    }
}