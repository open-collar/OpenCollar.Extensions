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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

using JetBrains.Annotations;

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
    public sealed class TypeExtensionsTest
    {
        [Fact]
        public void TestGetAssemblyDirectoryPath()
        {
            Assert.Equal(string.Empty, TypeExtensions.GetAssemblyDirectoryPath(null));

            Assert.Equal(string.Empty, ((Type)null).GetAssemblyDirectoryPath());

            var dynamicType = GetDynamicType();

            Assert.Equal(string.Empty, dynamicType.GetAssemblyDirectoryPath());
            var coreAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "OpenCollar.Extensions");
            Assert.Equal(Environment.CurrentDirectory, coreAssemblies.First().GetTypes()[0].GetAssemblyDirectoryPath());
        }

        [Fact]
        public void TestGetAssemblyPath()
        {
            var path = GetType().GetAssemblyPath();

            Assert.NotNull(path);

            var assembly = GetType().Assembly;

            Assert.True(path.Contains(assembly.GetName().Name, StringComparison.OrdinalIgnoreCase));
            Assert.True(assembly.IsDynamic || System.IO.File.Exists(path));

            Assert.Equal(string.Empty, ((Type)null).GetAssemblyPath());

            var dynamicType = GetDynamicType();

            Assert.Equal(string.Empty, dynamicType.GetAssemblyPath());
        }

        [Fact]
        public void TestGetAssemblyRelativePath()
        {
            var path = GetType().GetAssemblyRelativePath("one", "two", "three");

            Assert.NotNull(path);

            string onetwothree = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? @"one/two/three" : @"one\two\three";
            string onetwothree_leadingslash = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? @"/one/two/three" : @"\one\two\three";

            Assert.EndsWith(onetwothree_leadingslash, path, StringComparison.OrdinalIgnoreCase);

            path = GetType().GetAssemblyRelativePath();

            Assert.NotNull(path);
            Assert.True(path.Length > 0);

            path = GetType().GetAssemblyRelativePath((string[])null);

            Assert.NotNull(path);
            Assert.True(path.Length > 0);

            var assembly = GetType().Assembly;

            Assert.True(path.Contains(assembly.GetName().Name, StringComparison.OrdinalIgnoreCase));

            Assert.Equal(string.Empty, ((Type)null).GetAssemblyRelativePath());
            Assert.Equal(onetwothree, ((Type)null).GetAssemblyRelativePath("one", "two", "three"));

            var dynamicType = GetDynamicType();

            Assert.Equal(string.Empty, dynamicType.GetAssemblyRelativePath());
            Assert.Equal(string.Empty, dynamicType.GetAssemblyRelativePath((string[])null));
            Assert.Equal(onetwothree, dynamicType.GetAssemblyRelativePath("one", "two", "three"));
        }

        [NotNull]
        private static Type GetDynamicType()
        {
            // This will force a dynamic serialization assembly to be created and loaded.
            var serializer = new XmlSerializer(typeof(DisposableMock));
            using(var stream = new MemoryStream())
            {
                serializer.Serialize(stream, new DisposableMock());
            }

            var dynamicAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDynamic && (a.GetTypes().Length > 0));
            var dynamicType = dynamicAssemblies.First().GetTypes().First();
            return dynamicType;
        }
    }
}