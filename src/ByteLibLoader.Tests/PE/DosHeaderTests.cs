using System.IO;
using ByteLibLoader.PlatformLoaders.PE;
using CoreResourceManager;
using Xunit;
using static AssertNet.Xunit.Assertions;

namespace ByteLibLoader.Tests.PE
{
    /// <summary>
    /// Test class for the <see cref="DosHeader"/> class.
    /// </summary>
    public static class DosHeaderTests
    {
        /// <summary>
        /// Checks that we can correctly read a <see cref="DosHeader"/> from a stream.
        /// </summary>
        [Fact]
        public static void FromStreamTest()
        {
            Stream stream = Resource.Get("lib_win_x64");
            DosHeader header = DosHeader.FromStream(stream);
            AssertThat(header.Magic).IsEqualTo(DosHeader.RequiredMagicNumber);
        }
    }
}
