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
    ///     Utility methods supporting comparisons between objects and values.
    /// </summary>
    public static class Compare
    {
        /// <summary>
        ///     Compares any two values.
        /// </summary>
        /// <param name="left">
        ///     The first value to compare (against which the result will be relative).
        /// </param>
        /// <param name="right">
        ///     The second value.
        /// </param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has the following
        ///     meanings: Value Meaning Less than zero <paramref name="left" /> object is less than the <paramref name="right" />
        ///     parameter. Zero <paramref name="left" /> object is equal to <paramref name="right" />. Greater than zero
        ///     <paramref name="left" /> object is greater than <paramref name="right" />.
        /// </returns>
        public static int CompareAny([CanBeNull] object left, [CanBeNull] object right)
        {
            if(ReferenceEquals(left, right))
                return 0;

            if(ReferenceEquals(left, null))
                return -1;

            if(ReferenceEquals(right, null))
                return +1;

            var c1 = left as IComparable;
            if(!ReferenceEquals(c1, null))
                return c1.CompareTo(right);

            var c2 = right as IComparable;
            if(!ReferenceEquals(c2, null))
                return -c2.CompareTo(left);

            return string.Compare(left.ToString(), right.ToString(), StringComparison.Ordinal);
        }
    }
}