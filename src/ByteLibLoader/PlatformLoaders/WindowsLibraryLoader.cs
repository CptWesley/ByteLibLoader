using System;

namespace ByteLibLoader.PlatformLoaders
{
    /// <summary>
    /// Class which is able to load libraries from memory on Windows systems.
    /// </summary>
    /// <seealso cref="ILibraryLoader" />
    public class WindowsLibraryLoader : ILibraryLoader
    {
        /// <inheritdoc/>
        public IntPtr Load(byte[] library)
        {
            throw new NotImplementedException("Windows is not implemented yet.");
        }

        /// <inheritdoc/>
        public IntPtr GetSymbol(IntPtr library, string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
