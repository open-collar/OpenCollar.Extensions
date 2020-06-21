using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace OpenCollar.Extensions.TESTS
{
    public sealed class BadImplementationExceptionTests
    {
        [Fact]
        public void TestConstructors()
        {
            var ex = new Exception();
            const string message = @"TEST MESSSAGE";

            var x = new BadImplementationException();
            Assert.NotNull(x);

            x = new BadImplementationException(message);
            Assert.NotNull(x);
            Assert.Equal(x.Message, message);

            x = new BadImplementationException(message, ex);
            Assert.NotNull(x);
            Assert.Equal(x.Message, message);
            Assert.Equal(x.InnerException, ex);
        }

        [Fact]
        public void TestSerialization()
        {
            var ex = new Exception(@"INNER EXCEPTION");
            const string message = @"TEST MESSSAGE";

            var x = new BadImplementationException(message, ex);

            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            byte[] data;
            using(var stream = new System.IO.MemoryStream())
            {
                formatter.Serialize(stream, x);
                data = stream.ToArray();
            }

            using(var stream = new System.IO.MemoryStream(data))
            {
                var y = (BadImplementationException)formatter.Deserialize(stream);

                Assert.NotNull(y);
                Assert.NotSame(x, y);
                Assert.Equal(x.Message, y.Message);
                Assert.NotNull(y.InnerException);
                Assert.Equal(x.InnerException.Message, y.InnerException.Message);
            }
        }
    }
}