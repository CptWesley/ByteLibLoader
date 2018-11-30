using System;
using System.IO;
using System.Runtime.InteropServices;
using CoreResourceManager;
using Xunit;
using static AssertNet.Xunit.Assertions;

namespace ByteLibLoader.Tests
{
    /// <summary>
    /// Test class for the <see cref="LibraryLoader"/> class.
    /// </summary>
    public class LibraryLoaderTests
    {
        private byte[] _library;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryLoaderTests"/> class.
        /// </summary>
        public LibraryLoaderTests()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                string osPart = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "win" : "unix";
                string bitPart = Environment.Is64BitProcess ? "x64" : "x86";
                Stream stream = Resource.Get($"lib_{osPart}_{bitPart}");
                stream.CopyTo(ms);
                stream.Dispose();
                _library = ms.ToArray();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int Doubler(int value);

        /// <summary>
        /// Checks that loading the library gives a non-null pointer.
        /// </summary>
        [Fact]
        public void LoadTest()
        {
            IntPtr ptr = LibraryLoader.Load(_library);
            AssertThat((long)ptr).IsGreaterThan(0);
        }

        /// <summary>
        /// Checks that we can correctly call a function.
        /// </summary>
        [Fact]
        public void FunctionCallTest()
        {
            IntPtr libPtr = LibraryLoader.Load(_library);
            IntPtr funcPtr = LibraryLoader.GetSymbol(libPtr, "doubler");
            AssertThat((long)funcPtr).IsGreaterThan(0);
            Doubler doubler = (Doubler)Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(Doubler));
            AssertThat(doubler(42)).IsEqualTo(84);
        }
    }
}
