using System.IO;
using ByteLibLoader.PlatformLoaders.PE;
using CoreResourceManager;
using Xunit;
using static AssertNet.Xunit.Assertions;

namespace ByteLibLoader.Tests.PE
{
    /// <summary>
    /// Test class for the <see cref="OptionalHeader"/> class.
    /// </summary>
    public static class OptionalHeaderTests
    {
        /// <summary>
        /// Checks that we can correctly read a <see cref="OptionalHeader"/> from a stream.
        /// </summary>
        [Fact]
        public static void FromStreamTest()
        {
            Stream stream = Resource.Get("lib_win_x64");
            stream.Position = 0x0110;
            OptionalHeader header = OptionalHeader.FromStream(stream);
            AssertThat(header.Magic).IsEqualTo(0x020B);
            AssertThat(header.MajorLinkerVersion).IsEqualTo(0x0E);
            AssertThat(header.MinorLinkerVersion).IsEqualTo(0x0F);
            AssertThat(header.SizeOfCode).IsEqualTo(0x0000D200);
            AssertThat(header.SizeOfInitializedData).IsEqualTo(0x0000C400);
            AssertThat(header.SizeOfUninitializedData).IsEqualTo(0x00000000);
            AssertThat(header.AddressOfEntryPoint).IsEqualTo(0x0000133C);
            AssertThat(header.BaseOfCode).IsEqualTo(0x00001000);
            AssertThat(header.ImageBase).IsEqualTo(0x0000000180000000);
            AssertThat(header.SectionAlignment).IsEqualTo(0x00001000);
            AssertThat(header.FileAlignment).IsEqualTo(0x00000200);
            AssertThat(header.MajorOperatingSystemVersion).IsEqualTo(0x0006);
            AssertThat(header.MinorOperatingSystemVersion).IsEqualTo(0x0000);
            AssertThat(header.MajorImageVersion).IsEqualTo(0x0000);
            AssertThat(header.MinorImageVersion).IsEqualTo(0x0000);
            AssertThat(header.MajorSubsystemVersion).IsEqualTo(0x0006);
            AssertThat(header.MinorSubsystemVersion).IsEqualTo(0x0000);
            AssertThat(header.Win32VersionValue).IsEqualTo(0x00000000);
            AssertThat(header.SizeOfImage).IsEqualTo(0x0001C000);
            AssertThat(header.SizeOfHeaders).IsEqualTo(0x00000400);
            AssertThat(header.Checksum).IsEqualTo(0x00000000);
            AssertThat(header.Subsystem).IsEqualTo(0x0002);
            AssertThat(header.DllCharacteristics).IsEqualTo(0x0160);
            AssertThat(header.SizeOfStackReserve).IsEqualTo(0x0000000000100000);
            AssertThat(header.SizeOfStackCommit).IsEqualTo(0x0000000000001000);
            AssertThat(header.SizeOfHeapReserve).IsEqualTo(0x0000000000100000);
            AssertThat(header.SizeOfHeapCommit).IsEqualTo(0x0000000000001000);
            AssertThat(header.LoaderFlags).IsEqualTo(0x00000000);
            AssertThat(header.NumberOfRvaAndSizes).IsEqualTo(0x00000010);
        }
    }
}
