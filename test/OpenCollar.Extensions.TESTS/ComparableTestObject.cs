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

namespace OpenCollar.Extensions.TESTS
{
    internal class ComparableTestObject : IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ComparableTestObject(string value)
        {
            Value = value;
        }

        public string Value { get; }

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates
        ///     whether the current instance precedes, follows, or occurs in the same position in the sort order as the
        ///     other object.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these
        ///     meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order.
        ///     Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater
        ///     than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <param name="obj">
        ///     An object to compare with this instance.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj" /> is not the same type as this instance.
        /// </exception>
        public int CompareTo(object obj)
        {
            if(ReferenceEquals(obj, null))
                return 1;

            if(ReferenceEquals(obj, this))
                return 0;

            var x = obj as ComparableTestObject;
            if(!ReferenceEquals(x, null))
                return string.Compare(Value, x.Value, StringComparison.Ordinal);

            var y = obj as IncomparableTestObject;
            if(!ReferenceEquals(y, null))
                return string.Compare(Value, y.Value, StringComparison.Ordinal);

            return -1;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}