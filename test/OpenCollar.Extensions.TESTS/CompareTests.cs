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

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
    // [TestClass]
    public class CompareTests
    {
        [Fact]
        public void TestCompareAny()
        {
            Assert.True(Compare.CompareAny(1, 1) == 0, @"Integers can be compared. (0).");
            Assert.True(Compare.CompareAny(1, 2) < 0, @"Integers can be compared. (1).");
            Assert.True(Compare.CompareAny(2, 1) > 0, @"Integers can be compared. (2).");

            Assert.True(Compare.CompareAny("1", null) > 0, @"Nulls can be compared. (0).");
            Assert.True(Compare.CompareAny(null, "1") < 0, @"Nulls can be compared. (1).");
            Assert.True(Compare.CompareAny(null, null) == 0, @"Nulls can be compared. (2).");

            Assert.True(Compare.CompareAny(new IncomparableTestObject("1"), new ComparableTestObject("1")) == 0, @"Comparables and incomparables can be compared. (0).");
            Assert.True(Compare.CompareAny(new IncomparableTestObject("1"), new ComparableTestObject("2")) < 0, @"Comparables and incomparables can be compared. (1).");
            Assert.True(Compare.CompareAny(new IncomparableTestObject("2"), new ComparableTestObject("1")) > 0, @"Comparables and incomparables can be compared. (2).");

            Assert.True(Compare.CompareAny(new IncomparableTestObject("1"), new IncomparableTestObject("1")) == 0, @"Incomparables can be compared. (0).");
            Assert.True(Compare.CompareAny(new IncomparableTestObject("1"), new IncomparableTestObject("2")) < 0, @"Incomparables can be compared. (1).");
            Assert.True(Compare.CompareAny(new IncomparableTestObject("2"), new IncomparableTestObject("1")) > 0, @"Incomparables can be compared. (2).");
        }
    }
}