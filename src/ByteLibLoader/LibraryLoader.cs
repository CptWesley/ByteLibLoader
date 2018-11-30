using System;
using System.Runtime.InteropServices;
using ByteLibLoader.PlatformLoaders;

namespace ByteLibLoader
{
    /// <summary>
    /// Loads native libraries from memory.
    /// </summary>
    public static class LibraryLoader
    {
        private static ILibraryLoader _loader =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? (ILibraryLoader)new WindowsLibraryLoader() : new UnixLibraryLoader();

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="library">The bytes containing the library to load.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static IntPtr Load(byte[] library)
            => _loader.Load(library);

        /// <summary>
        /// Finds a pointer to the symbol with a certain name.
        /// </summary>
        /// <param name="library">Pointer to the library to look in.</param>
        /// <param name="symbol">The name of the symbol.</param>
        /// <returns>A pointer to a symbol.</returns>
        public static IntPtr GetSymbol(IntPtr library, string symbol)
            => _loader.GetSymbol(library, symbol);
    }
}
