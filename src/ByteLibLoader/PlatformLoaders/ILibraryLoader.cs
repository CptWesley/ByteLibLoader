using System;

namespace ByteLibLoader.PlatformLoaders
{
    /// <summary>
    /// Interface for library loaders.
    /// </summary>
    public interface ILibraryLoader
    {
        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="library">The bytes containing the library to load.</param>
        /// <returns>True if successful, false otherwise.</returns>
        IntPtr Load(byte[] library);

        /// <summary>
        /// Finds a pointer to the symbol with a certain name.
        /// </summary>
        /// <param name="library">Pointer to the library to look in.</param>
        /// <param name="symbol">The name of the symbol.</param>
        /// <returns>A pointer to a symbol.</returns>
        IntPtr GetSymbol(IntPtr library, string symbol);
    }
}
