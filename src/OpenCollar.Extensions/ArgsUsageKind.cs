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

namespace OpenCollar.Extensions
{
    /// <summary>
    ///     Defines the ways in which an event args factory should be used.
    /// </summary>
    public enum ArgsUsageKind
    {
        /// <summary>
        ///     Usage is undefined (this will cause an error if used).
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     The factory will be called once and the same instance of the event args will be used for all delegates.
        /// </summary>
        Reuse,

        /// <summary>
        ///     The factory will be called separately for each delegate.
        /// </summary>
        UniqueInstance
    }
}